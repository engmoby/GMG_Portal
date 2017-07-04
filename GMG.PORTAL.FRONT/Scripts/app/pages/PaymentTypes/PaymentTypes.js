controllerProvider.register('PaymentTypesController', ['$scope', 'PaymentTypesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', PaymentTypesController]);

function PaymentTypesController($scope, PaymentTypesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {


    $rootScope.ViewLoading = true;
    PaymentTypesApi.GetAll().then(function (response) {
        $scope.PaymentTypes = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (PaymentType) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = PaymentType == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (PaymentType == null) PaymentType = { 'NameAr': '', 'NameEn': '' };
        $scope.PaymentType = angular.copy(PaymentType);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (PaymentType) {
        debugger;
        $scope.PaymentType = angular.copy(PaymentType);
        $scope.PaymentType.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        PaymentTypesApi.Save($scope.PaymentType).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.PaymentTypes.indexOf($filter('filter')($scope.PaymentTypes, { 'ID': $scope.PaymentType.ID }, true)[0]);
                            $scope.PaymentTypes[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.PaymentTypes.indexOf($filter('filter')($scope.PaymentTypes, { 'ID': $scope.PaymentType.ID }, true)[0]);
                            $scope.PaymentTypes[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.PaymentTypes.push(angular.copy(response.data));
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

    $scope.Delete = function (PaymentType) {
        $scope.action = 'delete';
        $scope.PaymentType = PaymentType;
        $scope.PaymentType.IsDeleted = true;
        $scope.save();
    }

}





