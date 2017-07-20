controllerProvider.register('CareersController', ['$scope', 'CareersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CareersController]);
function CareersController($scope, CareersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CareersApi.GetAll().then(function (response) {
        $scope.Careers = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Career) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Career == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Career == null) Career = {};
        $scope.Career = angular.copy(Career);
        if ($scope.Career.Image)
            $scope.countFiles = $scope.Career.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (career) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Career == null ? 'add' : 'edit';
        if (career == null) career = {};
        $scope.Career = angular.copy(career);
        //if ($scope.Career.Image)
        //    $scope.countFiles = $scope.Career.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Career) {
        debugger;
        $scope.Career = angular.copy(Career);
        $scope.Career.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        CareersApi.Save($scope.Career).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Careers.indexOf($filter('filter')($scope.Careers, { 'ID': $scope.Career.ID }, true)[0]);
                           // $scope.Careers[index] = angular.copy(response.data);
                            CareersApi.GetAll().then(function (response) {
                                $scope.Careers = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Careers.indexOf($filter('filter')($scope.Careers, { 'ID': $scope.Career.ID }, true)[0]);
                           // $scope.Careers[index] = angular.copy(response.data);
                            CareersApi.GetAll().then(function (response) {
                                $scope.Careers = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            CareersApi.GetAll().then(function (response) {
                                $scope.Careers = response.data;
                            });
                            // $scope.Careers.push(angular.copy(response.data));
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

           
            $scope.back();
            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (Career) {
        $scope.action = 'delete';
        $scope.Career = Career;
        $scope.Career.IsDeleted = true;
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






