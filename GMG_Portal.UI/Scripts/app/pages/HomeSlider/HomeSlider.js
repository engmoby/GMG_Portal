controllerProvider.register('HomeSlidersController', ['$scope', 'HomeSlidersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomeSlidersController]);
function HomeSlidersController($scope, HomeSlidersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    debugger;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        HomeSlidersApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.HomeSliders = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.Image = "";
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    HomeSlidersApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.HomeSliders = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (homeSlider) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = homeSlider == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (homeSlider == null) homeSlider = {};
        $scope.HomeSlider = angular.copy(homeSlider);
        if ($scope.HomeSlider.Image)
            $scope.countFiles = $scope.HomeSlider.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (homeSlider) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = homeSlider == null ? 'add' : 'edit';
        if (homeSlider == null) homeSlider = {};
        $scope.homeSlider = angular.copy(homeSlider);
        //if ($scope.homeSlider.Image)
        //    $scope.countFiles = $scope.homeSlider.Image;

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
        $scope.back();
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.HomeSlider.Image = $scope.Image;
            $scope.Image = "";
        }
        $scope.HomeSlider.LangId = CurrentLanguage;

        HomeSlidersApi.Save($scope.HomeSlider).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HomeSliders.indexOf($filter('filter')($scope.HomeSliders, { 'Id': $scope.HomeSlider.Id }, true)[0]);
                            $scope.HomeSliders[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.HomeSliders.indexOf($filter('filter')($scope.HomeSliders, { 'Id': $scope.HomeSlider.Id }, true)[0]);
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





