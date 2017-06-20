controllerProvider.register('OrderStatusesController', ['$scope', 'OrderStatusesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OrderStatusesController]);
function OrderStatusesController($scope, OrderStatusesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    OrderStatusesApi.GetAll().then(function (response) {
        $scope.OrderStatuses = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (OrderStatus) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = OrderStatus == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (OrderStatus == null) OrderStatus = { 'NameAr': '', 'NameEn': '' };
        $scope.OrderStatus = angular.copy(OrderStatus);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        OrderStatusesApi.Save($scope.OrderStatus).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.OrderStatuses.indexOf($filter('filter')($scope.OrderStatuses, { 'ID': $scope.OrderStatus.ID }, true)[0]);
                            $scope.OrderStatuses[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.OrderStatuses.indexOf($filter('filter')($scope.OrderStatuses, { 'ID': $scope.OrderStatus.ID }, true)[0]);
                            $scope.OrderStatuses.splice(index, 1);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.OrderStatuses.push(angular.copy(response.data));
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
    $scope.Delete = function (OrderStatus) {
        $scope.action = 'delete';
        $scope.OrderStatus = OrderStatus;
        $scope.OrderStatus.IsDeleted = true;
        $scope.save();
    }
}





