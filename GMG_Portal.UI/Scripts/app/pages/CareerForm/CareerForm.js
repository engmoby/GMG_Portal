controllerProvider.register('CareerFormsController', ['$scope', 'CareerFormsApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CareerFormsController]);
function CareerFormsController($scope, CareerFormsApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CareerFormsApi.GetAll().then(function (response) {
        debugger;
        $scope.CareerForms = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (CareerForm) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = CareerForm == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (CareerForm == null) CareerForm = {};
        $scope.CareerForm = angular.copy(CareerForm);
        if ($scope.CareerForm.Image)
            $scope.countFiles = $scope.CareerForm.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.downloadFile = function (name) {
        CareerFormsApi.Download(name).then(function (data, status, headers) {
            headers = headers();

                var filename = headers['x-filename'];
                var contentType = headers['content-type'];

                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    console.log(ex);
                }
                $rootScope.ViewLoading = false;
                $scope.back();
            },
            function (data) {
                debugger; 
            });
         
    };
    $scope.openImage = function (CareerForm) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = CareerForm == null ? 'add' : 'edit';
        if (CareerForm == null) CareerForm = {};
        $scope.CareerForm = angular.copy(CareerForm);
        //if ($scope.CareerForm.Image)
        //    $scope.countFiles = $scope.CareerForm.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
    $scope.downloadFileImage = function (downloadPath) {
        window.open(downloadPath.Attach, '_blank', '');
    }
    $scope.Restore = function (CareerForm) {
        debugger;
        $scope.CareerForm = angular.copy(CareerForm);
        $scope.CareerForm.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.Seen = function (CareerForm) {
        debugger;
        $scope.CareerForm = angular.copy(CareerForm);
        if (CareerForm.Seen === true) {
        $scope.CareerForm.Seen = false;
            
        } else {
            
            $scope.CareerForm.Seen = true;
        }
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.CareerForm.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        CareerFormsApi.Save($scope.CareerForm).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.CareerForms.indexOf($filter('filter')($scope.CareerForms, { 'ID': $scope.CareerForm.ID }, true)[0]);
                           // $scope.CareerForms[index] = angular.copy(response.data);
                            CareerFormsApi.GetAll().then(function (response) {
                                $scope.CareerForms = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.CareerForms.indexOf($filter('filter')($scope.CareerForms, { 'ID': $scope.CareerForm.ID }, true)[0]);
                           // $scope.CareerForms[index] = angular.copy(response.data);
                            CareerFormsApi.GetAll().then(function (response) {
                                $scope.CareerForms = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            CareerFormsApi.GetAll().then(function (response) {
                                $scope.CareerForms = response.data;
                            });
                            // $scope.CareerForms.push(angular.copy(response.data));
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

    $scope.Delete = function (CareerForm) {
        $scope.action = 'delete';
        $scope.CareerForm = CareerForm;
        $scope.CareerForm.IsDeleted = true;
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






