controllerProvider.register('NewsCategorysController', ['$scope', 'NewsCategoryApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', NewsCategorysController]);
function NewsCategorysController($scope, NewsCategoryApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        NewsCategoryApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.NewsCategorys = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    NewsCategoryApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.NewsCategorys = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (NewsCategory) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = NewsCategory == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (NewsCategory == null) NewsCategory = {};
        $scope.NewsCategory = angular.copy(NewsCategory);
        if ($scope.NewsCategory.Image)
            $scope.countFiles = $scope.NewsCategory.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (NewsCategory) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = NewsCategory == null ? 'add' : 'edit';
        if (NewsCategory == null) NewsCategory = {};
        $scope.NewsCategory = angular.copy(NewsCategory);
        //if ($scope.NewsCategory.Image)
        //    $scope.countFiles = $scope.NewsCategory.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (NewsCategory) {
        debugger;
        $scope.NewsCategory = angular.copy(NewsCategory);
        $scope.NewsCategory.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;

        $scope.NewsCategory.langId = CurrentLanguage;

        debugger;
        NewsCategoryApi.Save($scope.NewsCategory).then(function (response) {

                switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                    case 'edit':
                        index = $scope.NewsCategorys.indexOf($filter('filter')($scope.NewsCategorys, { 'Id': $scope.NewsCategory.Id }, true)[0]);
                         $scope.NewsCategorys[index] = angular.copy(response.data);
                        //NewsCategoryApi.GetAll().then(function (response) {
                        //    $scope.NewsCategorys = response.data;
                        //});
                        toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                        break;
                    case 'delete':
                        index = $scope.NewsCategorys.indexOf($filter('filter')($scope.NewsCategorys, { 'Id': $scope.NewsCategory.Id }, true)[0]);
                         $scope.NewsCategorys[index] = angular.copy(response.data);
                        //NewsCategoryApi.GetAll().then(function (response) {
                        //    $scope.NewsCategorys = response.data;
                        //});
                        toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                        break;
                    case 'add':
                        //NewsCategoryApi.GetAll().then(function (response) {
                        //    $scope.NewsCategorys = response.data;
                        //});
                        $scope.NewsCategorys.push(angular.copy(response.data));
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
                    NewsCategoryApi.GetAll().then(function (response) {
                        $scope.NewsCategorys = response.data;
                    });
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

    $scope.Delete = function (NewsCategory) {
        $scope.action = 'delete';
        $scope.NewsCategory = NewsCategory;
        $scope.NewsCategory.IsDeleted = true;
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






