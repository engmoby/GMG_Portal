﻿controllerProvider.register('NewsController', ['$scope', 'appCONSTANTS', 'NewsApi', 'uploadNewsService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', NewsController]);
function NewsController($scope, appCONSTANTS, NewsApi, uploadNewsService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.language = appCONSTANTS.supportedLanguage;

    $rootScope.ViewLoading = true;
    var langId = document.querySelector('#HCurrentLang').value;
    $scope.CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        $scope.CurrentLanguage = selectedValue;

        NewsApi.GetAll($scope.CurrentLanguage).then(function (response) {
            $scope.News = response.data;
            $rootScope.ViewLoading = false;
        });

        NewsApi.GetAllCategories($scope.CurrentLanguage).then(function (response) {
            debugger;
            $scope.Categorys = response.data;
        });
    });
    $scope.SelectedCategory = null;

    $scope.Image = "";
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024

    $scope.letterLimit = 20;
    NewsApi.GetAll($scope.CurrentLanguage).then(function (response) {
        $scope.News = response.data;
        $rootScope.ViewLoading = false;
    });
    //get Categories

    NewsApi.GetAllCategories($scope.CurrentLanguage).then(function (response) {
        debugger;
        $scope.Categorys = response.data;
    });
    $scope.open = function (New) {
    
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = New == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (New == null) New = {}

        else {
          //  $scope.SelectedCategory = null;

            console.log($scope.SelectedCategory);
    debugger;
            var categoryIndex = $scope.Categorys.indexOf($filter('filter')($scope.Categorys, { 'Id': New.CategoryId }, true)[0]);
            $scope.SelectedCategory = $scope.Categorys[categoryIndex];

        };
        $scope.New = angular.copy(New);
        if ($scope.New.Image)
            $scope.countFiles = $scope.New.Image;

    }

    $scope.openImage = function (New) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = New == null ? 'add' : 'edit';
        if (New == null) New = {};
        $scope.New = angular.copy(New);
        //if ($scope.New.Image)
        //    $scope.countFiles = $scope.New.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (New) {
        debugger;
        $scope.New = angular.copy(New);
        $scope.New.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }
    $scope.Delete = function (New) {
        $scope.action = 'delete';
        $scope.New = New;
        $scope.New.IsDeleted = true;
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.New.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadNewsService.uploadFiles();
        debugger;
        if ($scope.SelectedCategory != null)
            $scope.New.CategoryId = $scope.SelectedCategory.Id;
        $scope.New.langId = $scope.CurrentLanguage;

        NewsApi.Save($scope.New).then(function (response) {
                $scope.New.Image = "";
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.News.indexOf($filter('filter')($scope.News, { 'Id': $scope.New.Id }, true)[0]);
                            $scope.News[index] = angular.copy(response.data);
                            //NewsApi.GetAll(CurrentLanguage).then(function (response) {
                            //    $scope.News = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.News.indexOf($filter('filter')($scope.News, { 'Id': $scope.New.Id }, true)[0]);
                            $scope.News[index] = angular.copy(response.data);
                            //NewsApi.GetAll(CurrentLanguage).then(function (response) {
                            //    $scope.News = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //NewsApi.GetAll(CurrentLanguage).then(function (response) {
                            //    $scope.News = response.data;
                            //});
                            $scope.News.push(angular.copy(response.data));
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
        if (extn !== "jpg" && extn !== "png") {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null); 
            alert($scope.ImageFormatValidaiton);
            return;
        } 
        uploadNewsService.uploadFiles($scope)
            // then() called when uploadFiles gets back
            .then(function (data) {
                // promise fulfilled
                $scope.uploading = false;
                if (data === '') {
                    // console.log(data);
                    //   $scope.Image=data.
                    $scope.save();
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



    $scope.selectedCategorysChanged = function (selectedItem) {
        if (selectedItem != null) {
            $scope.SelectedCategory = selectedItem;
        }

    }

}






