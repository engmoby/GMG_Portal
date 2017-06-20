controllerProvider.register('FrequencyController', ['$scope', 'FrequenciesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', FrequencyController]);
function FrequencyController($scope, FrequenciesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    FrequenciesApi.GetAll().then(function (response) {
        $scope.Frequencies = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Frequency) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Frequency == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Frequency == null) Frequency = { 'NameAr': '', 'NameEn': '' };
        $scope.Frequency = angular.copy(Frequency);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Frequency) {
        debugger;
        $scope.Frequency = angular.copy(Frequency);
        $scope.Frequency.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        FrequenciesApi.Save($scope.Frequency).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.Frequency.ID }, true)[0]);
                            $scope.Frequencies[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.Frequency.ID }, true)[0]);
                            $scope.Frequencies[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Frequencies.push(angular.copy(response.data));
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

    $scope.Delete = function (Frequency) {
        $scope.action = 'delete';
        $scope.Frequency = Frequency;
        $scope.Frequency.IsDeleted = true;
        $scope.save();
    }

}





