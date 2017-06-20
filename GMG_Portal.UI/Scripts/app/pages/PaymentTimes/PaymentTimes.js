controllerProvider.register('PaymentTimesController', ['$scope', 'PaymentTimesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', PaymentTimesController]);
function PaymentTimesController($scope, PaymentTimesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    PaymentTimesApi.GetAll().then(function (response) {
        $scope.PaymentTimes = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (PaymentTime) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = PaymentTime == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (PaymentTime == null) PaymentTime = { 'NameAr': '', 'NameEn': '' };
        $scope.PaymentTime = angular.copy(PaymentTime);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        PaymentTimesApi.Save($scope.PaymentTime).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.PaymentTimes.indexOf($filter('filter')($scope.PaymentTimes, { 'ID': $scope.PaymentTime.ID }, true)[0]);
                            $scope.PaymentTimes[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.PaymentTimes.indexOf($filter('filter')($scope.PaymentTimes, { 'ID': $scope.PaymentTime.ID }, true)[0]);
                            $scope.PaymentTimes.splice(index, 1);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.PaymentTimes.push(angular.copy(response.data));
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

    $scope.Delete = function (PaymentTime) {
        $scope.action = 'delete';
        $scope.PaymentTime = PaymentTime;
        $scope.PaymentTime.IsDeleted = true;
        $scope.save();
    }
}





