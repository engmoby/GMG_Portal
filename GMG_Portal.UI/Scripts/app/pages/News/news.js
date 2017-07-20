controllerProvider.register('NewsController', ['$scope', 'NewsApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', NewsController]);
function NewsController($scope, NewsApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    NewsApi.GetAll().then(function (response) {
        $scope.news = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (news) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = news == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (news == null) news = {};
        $scope.news = angular.copy(news);
        if ($scope.news.Image)
            $scope.countFiles = $scope.news.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (news) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = homeSlider == null ? 'add' : 'edit';
        if (news == null) news = {};
        $scope.news = angular.copy(news);
        //if ($scope.homeSlider.Image)
        //    $scope.countFiles = $scope.homeSlider.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (news) {
        debugger;
        $scope.news = angular.copy(news);
        $scope.news.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.news.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        NewsApi.Save($scope.news).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.news.indexOf($filter('filter')($scope.news, { 'ID': $scope.news.ID }, true)[0]);
                           // $scope.news[index] = angular.copy(response.data);
                            NewsApi.GetAll().then(function (response) {
                                $scope.news = response.data;
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.news.indexOf($filter('filter')($scope.news, { 'ID': $scope.news.ID }, true)[0]);
                           // $scope.news[index] = angular.copy(response.data);
                            NewsApi.GetAll().then(function (response) {
                                $scope.news = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            NewsApi.GetAll().then(function (response) {
                                $scope.news = response.data;
                            });
                            // $scope.news.push(angular.copy(response.data));
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






