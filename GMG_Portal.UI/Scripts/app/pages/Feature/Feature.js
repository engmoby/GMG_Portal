controllerProvider.register('FeaturesController', ['$scope', 'FeaturesApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', FeaturesController]);
function FeaturesController($scope, FeaturesApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        FeaturesApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Features = response.data;
            $rootScope.ViewLoading = false;
        });
    });
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    FeaturesApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.Features = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Feature) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Feature == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Feature == null) Feature = {};
        $scope.Feature = angular.copy(Feature);
        if ($scope.Feature.Image)
            $scope.countFiles = $scope.Feature.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (Feature) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Feature == null ? 'add' : 'edit';
        if (Feature == null) Feature = {};
        $scope.Feature = angular.copy(Feature);
        //if ($scope.Feature.Image)
        //    $scope.countFiles = $scope.Feature.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Feature) {
        debugger;
        $scope.Feature = angular.copy(Feature);
        $scope.Feature.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Feature.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        $scope.Feature.LangId = CurrentLanguage;

        FeaturesApi.Save($scope.Feature).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Features.indexOf($filter('filter')($scope.Features, { 'ID': $scope.Feature.ID }, true)[0]);
                           // $scope.Features[index] = angular.copy(response.data);
                            FeaturesApi.GetAll().then(function (response) {
                                $scope.Features = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Features.indexOf($filter('filter')($scope.Features, { 'ID': $scope.Feature.ID }, true)[0]);
                           // $scope.Features[index] = angular.copy(response.data);
                            FeaturesApi.GetAll().then(function (response) {
                                $scope.Features = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            FeaturesApi.GetAll().then(function (response) {
                                $scope.Features = response.data;
                            });
                            // $scope.Features.push(angular.copy(response.data));
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

    $scope.Delete = function (Feature) {
        $scope.action = 'delete';
        $scope.Feature = Feature;
        $scope.Feature.IsDeleted = true;
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






