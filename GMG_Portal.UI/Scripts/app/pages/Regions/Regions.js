controllerProvider.register('RegionsController', ['$scope', 'RegionsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', RegionsController]);
function RegionsController($scope, RegionsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    //get countries
    RegionsApi.GetAllCountries().then(function (response) {
        $scope.Countries = response.data;
        for (var i = 0; i < $scope.Countries.length; i++) {
            if ($scope.Countries[i].IsCurrent == true) {
                $scope.selectedCountry = $scope.Countries[i];
                break;
            }
        }

        //get cities of selected country
        RegionsApi.GetCitiesByCountryId($scope.selectedCountry.ID).then(function (response) {
            $scope.cities = response.data;
            $scope.selectedCity = $scope.cities[0];

            RegionsApi.GetAllRegionsByCityId($scope.selectedCity.ID).then(function (response) {
                $scope.Regions = response.data;
            });
        });
        $rootScope.ViewLoading = false;

    });


    //get cities of selected country when selected country changed
    $scope.selectedCountryChanged = function (selectedItem) {
        $scope.Regions = null;
        if (selectedItem != null) {
            //get cities of selected country
            $rootScope.ViewLoading = true;
            $scope.selectedCity = null;
            RegionsApi.GetCitiesByCountryId(selectedItem.ID).then(function (response) {
                debugger;
                $scope.cities = response.data;
                $scope.selectedCity = $scope.cities[0];
                $rootScope.ViewLoading = false;
            });
        }
    };

    //when selected city changed
    $scope.selectedCityChanged = function (selectedItem) {
        debugger;
        $scope.Regions = null;
        if (selectedItem != null)
        {
            RegionsApi.GetAllRegionsByCityId(selectedItem.ID).then(function (response) {
                $scope.Regions = response.data;
            });
        }
        $scope.selectedCity = selectedItem;
        //alert(selectedItem.ID);
    };


    $scope.open = function (Region) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Region == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Region == null) Region = { 'NameAr': '', 'NameEn': '' };
        $scope.Region = angular.copy(Region);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }


    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Region) {
        debugger;
        $scope.Region = angular.copy(Region);
        $scope.Region.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.Region.CityID = $scope.selectedCity.ID;
        RegionsApi.Save($scope.Region).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Regions.indexOf($filter('filter')($scope.Regions, { 'ID': $scope.Region.ID }, true)[0]);
                            $scope.Regions[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Regions.indexOf($filter('filter')($scope.Regions, { 'ID': $scope.Region.ID }, true)[0]);
                            $scope.Regions[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Regions.push(angular.copy(response.data));
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


    $scope.openModifyCitiesModal = function ()
    {
        $('#ModelModfiyCities').modal('show');
    }

    $scope.Delete = function (Region) {
        $scope.action = 'delete';
        $scope.Region = Region;
        $scope.Region.IsDeleted = true;
        $scope.save();
    }
}





