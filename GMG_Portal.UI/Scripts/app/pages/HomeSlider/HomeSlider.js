controllerProvider.register('HomeSlidersController', ['$scope', 'HomeSlidersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomeSlidersController]);
function HomeSlidersController($scope, HomeSlidersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $rootScope.ViewLoading = true;
    HomeSlidersApi.GetAll().then(function (response) {
        $scope.HomeSliders = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (homeSlider) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = homeSlider == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (homeSlider == null) homeSlider = { 'NameAr': '', 'NameEn': '' };
        $scope.HomeSlider = angular.copy(homeSlider);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (homeSlider) {
        debugger;
        $scope.HomeSlider = angular.copy(homeSlider);
        $scope.HomeSlider.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        if ($scope.Image) {
            $scope.HomeSlider.Image = $scope.Image;
            $scope.Image = ""; 
        }
        //  uploadService.uploadFiles();
        debugger;
        $rootScope.ViewLoading = true;
        HomeSlidersApi.Save($scope.HomeSlider).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HomeSliders.indexOf($filter('filter')($scope.HomeSliders, { 'ID': $scope.HomeSlider.ID }, true)[0]);
                            $scope.HomeSliders[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.HomeSliders.indexOf($filter('filter')($scope.HomeSliders, { 'ID': $scope.HomeSlider.ID }, true)[0]);
                            $scope.HomeSliders[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.HomeSliders.push(angular.copy(response.data));
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

    $scope.Delete = function (homeSlider) {
        $scope.action = 'delete';
        $scope.HomeSlider = homeSlider;
        $scope.HomeSlider.IsDeleted = true;
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
      //  $scope.save();
        $scope.uploading = true;
        uploadService.uploadFiles($scope)
            // then() called when uploadFiles gets back
            .then(function (data) {
                // promise fulfilled
                debugger;
                $scope.uploading = false;
                if (data === '') {
                   // console.log(data);
                    //   $scope.Image=data.
                    alert("Done!!!")
                    $scope.formdata = new FormData();
                    $scope.data = [];
                    $scope.countFiles = '';
                    $scope.$apply;
                } else {
                   // console.log(data);
                    //Server Error
                    alert("Shit, What happended up there!!! " + data);
                }
            }, function (error) {
                $scope.uploading = false;
                //Server Error
                alert("Shit, What happended up there!!! " + error);
            }

            );
    };

}






