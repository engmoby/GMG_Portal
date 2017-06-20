controllerProvider.register('ComplaintTypesController', ['$scope', 'ComplaintTypesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ComplaintTypesController]);
function ComplaintTypesController($scope, ComplaintTypesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $rootScope.ViewLoading = true;
    ComplaintTypesApi.GetAll().then(function (response) {
        $scope.ComplaintTypes = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (ComplaintType) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = ComplaintType == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (ComplaintType == null) ComplaintType = { 'NameAr': '', 'NameEn': '' };
        $scope.ComplaintType = angular.copy(ComplaintType);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (ComplaintType) {
        debugger;
        $scope.ComplaintType = angular.copy(ComplaintType);
        $scope.ComplaintType.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        ComplaintTypesApi.Save($scope.ComplaintType).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.ComplaintTypes.indexOf($filter('filter')($scope.ComplaintTypes, { 'ID': $scope.ComplaintType.ID }, true)[0]);
                            $scope.ComplaintTypes[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.ComplaintTypes.indexOf($filter('filter')($scope.ComplaintTypes, { 'ID': $scope.ComplaintType.ID }, true)[0]);
                            $scope.ComplaintTypes[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.ComplaintTypes.push(angular.copy(response.data));
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

    $scope.Delete = function (ComplaintType) {
        $scope.action = 'delete';
        $scope.ComplaintType = ComplaintType;
        $scope.ComplaintType.IsDeleted = true;
        $scope.save();
    }


}





