﻿controllerProvider.register('OffersController', ['$scope', 'OffersApi', 'uploadNewsService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OffersController]);
function OffersController($scope, OffersApi, uploadNewsService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = ""; 
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    OffersApi.GetAll().then(function (response) {
        $scope.Offers = response.data;
        $rootScope.ViewLoading = false;
    });

    //get Categories

    OffersApi.GetAllCategories().then(function (response) {
        debugger;
        $scope.Categorys = response.data;
    });
    $scope.open = function (Offer) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Offer == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Offer == null) Offer = {}
        else {
            var categoryIndex = $scope.Categorys.indexOf($filter('filter')($scope.Categorys, { 'Id': Offer.CategoryId }, true)[0]);
            $scope.SelectedCategory = $scope.Categorys[categoryIndex];
        };
        $scope.Offer = angular.copy(Offer);
        if ($scope.Offer.Image)
            $scope.countFiles = $scope.Offer.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (Offer) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Offer == null ? 'add' : 'edit';
        if (Offer == null) Offer = {};
        $scope.Offer = angular.copy(Offer);
        //if ($scope.Offer.Image)
        //    $scope.countFiles = $scope.Offer.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Offer) {
        debugger;
        $scope.Offer = angular.copy(Offer);
        $scope.Offer.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Offer.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadNewsService.uploadFiles();
        debugger;
        $scope.Offer.CategoryId = $scope.SelectedCategory.Id;
        OffersApi.Save($scope.Offer).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Offers.indexOf($filter('filter')($scope.Offers, { 'ID': $scope.Offer.ID }, true)[0]);
                            // $scope.Offers[index] = angular.copy(response.data);
                            OffersApi.GetAll().then(function (response) {
                                $scope.Offers = response.data;
                            });
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Offers.indexOf($filter('filter')($scope.Offers, { 'ID': $scope.Offer.ID }, true)[0]);
                            // $scope.Offers[index] = angular.copy(response.data);
                            OffersApi.GetAll().then(function (response) {
                                $scope.Offers = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            OffersApi.GetAll().then(function (response) {
                                $scope.Offers = response.data;
                            });
                            // $scope.Offers.push(angular.copy(response.data));
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

    $scope.Delete = function (Offer) {
        $scope.action = 'delete';
        $scope.Offer = Offer;
        $scope.Offer.IsDeleted = true;
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
        if (fileLength > 152166) {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageSizeValidaiton);
            return;
        }
        if (extn !== "jpg" && extn !== "png") {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            //  alert("neeed jpg");
            alert($scope.ImageFormatValidaiton);
            return;
        }
        //if (extn !== "png") {
        //    $scope.countFiles = null;
        //    angular.element("input[type='file']").val(null);
        //    alert("neeed jpeg");
        //    // alert($scope.ImageFormatValidaiton);
        //    return;
        //}
        uploadNewsService.uploadFiles($scope)
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

    $scope.CategoryObject = {
        selectedCategory: [{}]
    };

    $scope.selectedCategorysChanged = function (selectedCategory) {
        debugger;
        $scope.SelectedCategory = selectedCategory;
    }

}






