controllerProvider.register('AccountTypesController', ['$scope', 'AccountTypesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', AccountTypesController]);


function AccountTypesController($scope, AccountTypesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    AccountTypesApi.GetAll().then(function (response) {
        $scope.AccountTypes = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (AccountType) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = AccountType == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (AccountType == null) AccountType = { 'NameAr': '', 'NameEn': '' };
        $scope.AccountType = angular.copy(AccountType);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 500);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (AccountType)
    {
        debugger;
        $scope.AccountType = angular.copy(AccountType);
        $scope.AccountType.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        debugger;
        AccountTypesApi.Save($scope.AccountType).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.AccountTypes.indexOf($filter('filter')($scope.AccountTypes, { 'ID': $scope.AccountType.ID }, true)[0]);
                            $scope.AccountTypes[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.AccountTypes.indexOf($filter('filter')($scope.AccountTypes, { 'ID': $scope.AccountType.ID }, true)[0]);
                            $scope.AccountTypes[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.AccountTypes.push(angular.copy(response.data));
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

    $scope.Delete = function (AccountType) {
        $scope.action = 'delete';
        $scope.AccountType = AccountType;
        $scope.AccountType.IsDeleted = true;
        $scope.save();
    }
}





