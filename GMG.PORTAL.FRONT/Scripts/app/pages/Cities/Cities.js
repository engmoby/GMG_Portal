controllerProvider.register('CitiesController', ['$scope', 'CitiesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CitiesController]);
function CitiesController($scope, CitiesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    CitiesApi.GetAllCountries().then(function (response) {
        debugger;
        $scope.selectedCountry = null;
        $scope.Countries = response.data;
        for (var i = 0; i < $scope.Countries.length; i++) {
            if ($scope.Countries[i].IsCurrent == true) {
                $scope.selectedCountry = $scope.Countries[i];
                break;
            }
        }

        //get cities of selected country
        if ($scope.selectedCountry != null) {
            CitiesApi.GetCitiesByCountryId($scope.selectedCountry.ID).then(function (response) {
                $scope.Cities = response.data;
            })
        };
        $rootScope.ViewLoading = false;

       });

    $scope.selectedCountryChanged = function (selectedItem) {
        debugger;
        $scope.selectedCountry = selectedItem;
        $scope.Cities = null;
        if (selectedItem != null) {
            //get cities of selected country
            $rootScope.ViewLoading = true;
            CitiesApi.GetCitiesByCountryId(selectedItem.ID).then(function (response) {
                debugger;
                $scope.Cities = response.data;
                $rootScope.ViewLoading = false;
            });
        }
    };

    $scope.open = function (City) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = City == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (City == null) City = { 'NameAr': '', 'NameEn': '' };
        $scope.City = angular.copy(City);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (City) {
        debugger;
        $scope.City = angular.copy(City);
        $scope.City.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }
    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.City.CountryID = $scope.selectedCountry.ID;
        CitiesApi.Save($scope.City).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Cities.indexOf($filter('filter')($scope.Cities, { 'ID': $scope.City.ID }, true)[0]);
                            $scope.Cities[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Cities.indexOf($filter('filter')($scope.Cities, { 'ID': $scope.City.ID }, true)[0]);
                            $scope.Cities[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Cities.push(angular.copy(response.data));
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
        });
    }

    $scope.Delete = function (City) {
        $scope.action = 'delete';
        $scope.City = City;
        $scope.City.IsDeleted = true;
        $scope.save();
    }


}





