controllerProvider.register('CountriesController', ['$scope', 'CountriesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CountriesController]);
function CountriesController($scope, CountriesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    CountriesApi.GetAll().then(function (response) {
        $scope.Countries = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Country) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Country == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Country == null) Country = { 'NameAr': '', 'NameEn': '' };
        $scope.Country = angular.copy(Country);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Country) {
        debugger;
        $scope.Country = angular.copy(Country);
        $scope.Country.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        CountriesApi.Save($scope.Country).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Countries.indexOf($filter('filter')($scope.Countries, { 'ID': $scope.Country.ID }, true)[0]);
                            $scope.Countries[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Countries.indexOf($filter('filter')($scope.Countries, { 'ID': $scope.Country.ID }, true)[0]);
                            $scope.Countries[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Countries.push(angular.copy(response.data));
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

    $scope.Delete = function (Country) {
        $scope.action = 'delete';
        $scope.Country = Country;
        $scope.Country.IsDeleted = true;
        $scope.save();
    }

}





