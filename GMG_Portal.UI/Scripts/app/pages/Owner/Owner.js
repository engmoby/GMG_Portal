controllerProvider.register('OwnersController', ['$scope', 'OwnersApi', 'uploadOwnersService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OwnersController]);
function OwnersController($scope, OwnersApi, uploadOwnersService, $rootScope, $timeout, $filter, $uibModal, toastr) {

    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;
        OwnersApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Owners = response.data;
            $rootScope.ViewLoading = false;
        });
    });
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 1MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    OwnersApi.GetAll(CurrentLanguage).then(function (response) {
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
        $scope.back();
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Owner.Image = $scope.Image;
            $scope.Image = "";
        } 
        debugger;
        $scope.Owner.LangId = CurrentLanguage;
        OwnersApi.Save($scope.Owner).then(function (response) { 
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'Id': $scope.Owner.Id }, true)[0]);
                            $scope.Owners[index] = angular.copy(response.data); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'Id': $scope.Owner.Id }, true)[0]);
                            $scope.Owners[index] = angular.copy(response.data);
                             toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                             $scope.Owners.push(angular.copy(response.data));
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
        if (fileLength > maxFileSize) {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageSizeValidaiton);
            return;
        }
        if (extn !== "jpg" && extn !== "jpeg" && extn !== "png") {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageFormatValidaiton);
            return;
        }
        uploadOwnersService.uploadFiles($scope)
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






