controllerProvider.register('CoreValuesController', ['$scope', 'CoreValuesApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CoreValuesController]);
function CoreValuesController($scope, CoreValuesApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CoreValuesApi.GetAll().then(function (response) {
        $scope.CoreValues = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Values) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Values == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Values == null) Values = {};
        $scope.Values = angular.copy(Values);
        if ($scope.Values.Image)
            $scope.countFiles = $scope.Values.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (Values) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Values == null ? 'add' : 'edit';
        if (Values == null) Values = {};
        $scope.Values = angular.copy(Values);
        //if ($scope.Values.Image)
        //    $scope.countFiles = $scope.Values.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Values) {
        debugger;
        $scope.Values = angular.copy(Values);
        $scope.Values.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Values.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        CoreValuesApi.Save($scope.Values).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.CoreValues.indexOf($filter('filter')($scope.CoreValues, { 'ID': $scope.Values.ID }, true)[0]);
                           // $scope.CoreValues[index] = angular.copy(response.data);
                            CoreValuesApi.GetAll().then(function (response) {
                                $scope.CoreValues = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.CoreValues.indexOf($filter('filter')($scope.CoreValues, { 'ID': $scope.Values.ID }, true)[0]);
                           // $scope.CoreValues[index] = angular.copy(response.data);
                            CoreValuesApi.GetAll().then(function (response) {
                                $scope.CoreValues = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            CoreValuesApi.GetAll().then(function (response) {
                                $scope.CoreValues = response.data;
                            });
                            // $scope.CoreValues.push(angular.copy(response.data));
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

    $scope.Delete = function (Values) {
        $scope.action = 'delete';
        $scope.Values = Values;
        $scope.Values.IsDeleted = true;
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






