controllerProvider.register('OwnersController', ['$scope', 'OwnersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OwnersController]);
function OwnersController($scope, OwnersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $scope.ImageFormatValidaiton = "Please upload jpg or jpeg ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 1MB";
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    OwnersApi.GetAll().then(function (response) {
        $scope.Owners = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Owner) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Owner == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Owner == null) Owner = {};
        $scope.Owner = angular.copy(Owner);
        if ($scope.Owner.Image)
            $scope.countFiles = $scope.Owner.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (Owner) {
        debugger; 
        $('#ModelImage').modal('show');
        $scope.action = Owner == null ? 'add' : 'edit'; 
        if (Owner == null) Owner = {};
        $scope.Owner = angular.copy(Owner);
        if ($scope.Owner.Image)
            $scope.countFiles = $scope.Owner.Image;
         
    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Owner) {
        debugger;
        $scope.Owner = angular.copy(Owner);
        $scope.Owner.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Owner.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        OwnersApi.Save($scope.Owner).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'ID': $scope.Owner.ID }, true)[0]);
                           // $scope.Owners[index] = angular.copy(response.data);
                            OwnersApi.GetAll().then(function (response) {
                                $scope.Owners = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'ID': $scope.Owner.ID }, true)[0]);
                           // $scope.Owners[index] = angular.copy(response.data);
                            OwnersApi.GetAll().then(function (response) {
                                $scope.Owners = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            OwnersApi.GetAll().then(function (response) {
                                $scope.Owners = response.data;
                            });
                            // $scope.Owners.push(angular.copy(response.data));
                            toastr.success($('#HSaveSuccessMessage').val(), 'Success');
                            break;
                    }
                    break;
                case "NameEnMustBeUnique":
                    toastr.error($('#HEnglishNameUnique').val(), 'Error');
                    break;
                case "NameArMustBeUnique":
                    toastr.error($('#HArabicNameUnique').val(), 'Error');
                    break;
                case "HasRelationship":
                    toastr.error($('#HCannotDeleted').val(), 'Error');
                    break;
                default:

            }

            $rootScope.ViewLoading = false;
            $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (Owner) {
        $scope.action = 'delete';
        $scope.Owner = Owner;
        $scope.Owner.IsDeleted = true;
        $scope.save();
    }
    $scope.uploading = false;
    $scope.countFiles = '';
    $scope.data = []; //For displaying file name on browser
    $scope.formdata = new FormData();
    $scope.getFiles = function (file) {
        angular.forEach(file, function (value, key) {
            $scope.formdata.append(key, value);
            $scope.data.push({ FileName: value.name, FileLength: value.size });
            $scope.Image = value.name;
            // console.log($scope.Image);
        });
        //This line is just show you there is possible to
        //send in extra parameter to server.


        $scope.countFiles = $scope.data.length == 0 ? '' : $scope.data.length + ' files selected';
        $scope.$apply();
        $scope.formdata.append('countFiles', $scope.countFiles);
        // console.log($scope.data);
    };

    $scope.uploadFiles = function () {
        debugger;
        if ($scope.data.length === 0) {
            $scope.save();
            return;
        }
        var extn = $scope.Image.split(".").pop();
        var fileLength = $scope.data[0].FileLength;
        if (fileLength > 52166) {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageSizeValidaiton);
            return;
        }
        if (extn !== "jpg") {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageFormatValidaiton);
            return;
        }
        uploadService.uploadFiles($scope)
            // then() called when uploadFiles gets back
            .then(function (data) {
                // promise fulfilled
                $scope.uploading = false;
                if (data === '') {
                    // console.log(data);
                    //   $scope.Image=data.
                    $scope.save();
                    $scope.data = [];
                    alert("Done!!!");
                    $scope.formdata = new FormData();
                    $scope.data = [];
                    $scope.countFiles = '';
                    $scope.$apply;
                } else {
                    // console.log(data);
                    //Server Error
                    $scope.data = [];
                    alert("Shit, What happended up there!!! " + data);
                }
            }, function (error) {
                $scope.uploading = false;
                $scope.data = [];
                //Server Error
                alert("Shit, What happended up there!!! " + error);
            }

            );
    };

}






