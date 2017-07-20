controllerProvider.register('NewsCategoryController', ['$scope', 'NewsCategoryApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', NewsCategoryController]);
function NewsCategoryController($scope, NewsCategoryApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    NewsCategoryApi.GetAll().then(function (response) {
        $scope.newscategory = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (newscategory) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = news == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (newscategory == null) newscategory = {};
        $scope.newscategory = angular.copy(news);
        if ($scope.newscategory.Image)
            $scope.countFiles = $scope.newscategory.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (newscategory) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = homeSlider == null ? 'add' : 'edit';
        if (newscategory == null) newscategory = {};
        $scope.newscategory = angular.copy(newscategory);
        //if ($scope.homeSlider.Image)
        //    $scope.countFiles = $scope.homeSlider.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (newscategory) {
        debugger;
        $scope.newscategory = angular.copy(newscategory);
        $scope.newscategory.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.newscategory.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        NewsCategoryApi.Save($scope.newscategory).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.newscategory.indexOf($filter('filter')($scope.newscategory, { 'ID': $scope.newscategory.ID }, true)[0]);
                           // $scope.newscategory[index] = angular.copy(response.data);
                            NewsCategoryApi.GetAll().then(function (response) {
                                $scope.newscategory = response.data;
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.newscategory.indexOf($filter('filter')($scope.newscategory, { 'ID': $scope.newscategory.ID }, true)[0]);
                           // $scope.newscategory[index] = angular.copy(response.data);
                            NewsCategoryApi.GetAll().then(function (response) {
                                $scope.newscategory = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            NewsCategoryApi.GetAll().then(function (response) {
                                $scope.newscategory = response.data;
                            });
                            // $scope.newscategory.push(angular.copy(response.data));
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

    $scope.Delete = function (newscategory) {
        $scope.action = 'delete';
        $scope.newscategory = newscategory;
        $scope.newscategory.IsDeleted = true;
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






