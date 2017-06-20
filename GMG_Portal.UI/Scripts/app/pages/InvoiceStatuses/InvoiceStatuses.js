controllerProvider.register('InvoiceStatusesController', ['$scope', 'InvoiceStatusesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', InvoiceStatusesController]);
function InvoiceStatusesController($scope, InvoiceStatusesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $rootScope.ViewLoading = true;
    InvoiceStatusesApi.GetAll().then(function (response) {
        $scope.InvoiceStatuses = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (InvoiceStatus) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = InvoiceStatus == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (InvoiceStatus == null) InvoiceStatus = { 'NameAr': '', 'NameEn': '' };
        $scope.InvoiceStatus = angular.copy(InvoiceStatus);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (InvoiceStatus) {
        debugger;
        $scope.InvoiceStatus = angular.copy(InvoiceStatus);
        $scope.InvoiceStatus.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        InvoiceStatusesApi.Save($scope.InvoiceStatus).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.InvoiceStatuses.indexOf($filter('filter')($scope.InvoiceStatuses, { 'ID': $scope.InvoiceStatus.ID }, true)[0]);
                            $scope.InvoiceStatuses[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.InvoiceStatuses.indexOf($filter('filter')($scope.InvoiceStatuses, { 'ID': $scope.InvoiceStatus.ID }, true)[0]);
                            $scope.InvoiceStatuses[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.InvoiceStatuses.push(angular.copy(response.data));
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

    $scope.Delete = function (InvoiceStatus) {
        $scope.action = 'delete';
        $scope.InvoiceStatus = InvoiceStatus;
        $scope.InvoiceStatus.IsDeleted = true;
        $scope.save();
    }


}





