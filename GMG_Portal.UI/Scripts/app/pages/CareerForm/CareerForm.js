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
         
    }
   
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
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
                            index = $scope.CareerForms.indexOf($filter('filter')($scope.CareerForms, { 'Id': $scope.CareerForm.Id }, true)[0]);
                            $scope.CareerForms[index] = angular.copy(response.data);
                            //CareerFormsApi.GetAll().then(function (response) {
                            //    $scope.CareerForms = response.data; 
                            //}); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.CareerForms.indexOf($filter('filter')($scope.CareerForms, { 'Id': $scope.CareerForm.Id }, true)[0]);
                            $scope.CareerForms[index] = angular.copy(response.data);
                            //CareerFormsApi.GetAll().then(function (response) {
                            //    $scope.CareerForms = response.data;
                            //});
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
                    //CareerFormsApi.GetAll().then(function (response) {
                    //    $scope.CareerForms = response.data; 
                    //}); 
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

   

}






