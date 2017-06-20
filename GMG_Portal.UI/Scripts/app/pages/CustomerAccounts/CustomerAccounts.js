controllerProvider.register('CustomerAccountsController', ['$scope', 'CustomerAccountsApi', '$rootScope', '$timeout', '$uibModal', '$filter', 'fileReader', 'toastr', '$compile', CustomerAccountsController]);


function CustomerAccountsController($scope, CustomerAccountsApi, $rootScope, $timeout, $uibModal, $filter, fileReader, toastr, $compile) {
    debugger;
    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;
    $rootScope.ViewLoading = true;
    var servicesBackCount = 0;
    CustomerAccountsApi.GetAll().then(function (response) {
        debugger;
        $scope.Customers = response.data;
        servicesBackCount++;
        if (servicesBackCount > 4)
            $rootScope.ViewLoading = false;
    });

    CustomerAccountsApi.GetAllAccountTypes().then(function (response) {
        $scope.AccountTypes = response.data;
        servicesBackCount++;
        if (servicesBackCount > 4)
            $rootScope.ViewLoading = false;
    });

    CustomerAccountsApi.GetAllCountries().then(function (response) {
        $scope.Countries = response.data;
        for (var i = 0; i < $scope.Countries.length; i++) {
            if ($scope.Countries[i].IsCurrent == true) {
                $scope.selectedCountry = $scope.Countries[i];
                break;
            }
            servicesBackCount++;
            if (servicesBackCount > 4)
                $rootScope.ViewLoading = false;
        }
        CustomerAccountsApi.GetCitiesByCountryId($scope.selectedCountry.ID).then(function (response) {
            debugger;
            $scope.cities = response.data;
            servicesBackCount++;
            if (servicesBackCount > 4)
                $rootScope.ViewLoading = false;
        });
    });

    CustomerAccountsApi.GetAllPaymentTypes().then(function (response) {

        $scope.PaymentTypes = response.data;
        servicesBackCount++;
        if (servicesBackCount > 4)
            $rootScope.ViewLoading = false;
    });


    $scope.selectedAccountTypesChanged = function (SelectedAccountType) {
        debugger;
        if (SelectedAccountType)
            $scope.SelectedAccountType = SelectedAccountType;

        $scope.Customer.AccountTypeNameAr = $scope.SelectedAccountType.NameAr;
        $scope.Customer.AccountTypeNameEn = $scope.SelectedAccountType.NameEn;
    }

    //get cities of selected country when selected country changed
    $scope.selectedCountryChanged = function (selectedCountry) {
        debugger;

        if (selectedCountry)
            $scope.selectedCountry = selectedCountry;

        $scope.Customer.CountryNameAr = $scope.selectedCountry.NameAr;
        $scope.Customer.CountryNameEn = $scope.selectedCountry.NameEn;
        $scope.Customer.CountryID = $scope.selectedCountry.ID;

        $scope.Regions = null;
        $scope.cities = null;
        if ($scope.selectedCountry != null) {
            //get cities of selected country
            $rootScope.ViewLoading = true;
            CustomerAccountsApi.GetCitiesByCountryId($scope.selectedCountry.ID).then(function (response) {
                debugger;
                $scope.cities = response.data;
                $rootScope.ViewLoading = false;
            });
        }
    };

    //when selected city changed
    $scope.selectedCityChanged = function (selectedItem) {
        debugger;
        if (selectedItem) {
            $scope.selectedCity = selectedItem;
        }
        $scope.Customer.CityNameAr = $scope.selectedCity.NameAr;
        $scope.Customer.CityNameEn = $scope.selectedCity.NameEn;
        $scope.Regions = null;
        if ($scope.selectedCity != null) {
            debugger;
            CustomerAccountsApi.GetAllRegionsByCityId($scope.selectedCity.ID).then(function (response) {
                $scope.Regions = response.data;
            });
        }

    };

    $scope.selectedRegionChanged = function (selectedRegion) {
        debugger;
        if (selectedRegion) {
            $scope.selectedRegion = selectedRegion;
        }
        $scope.Customer.RegionNameAr = $scope.selectedRegion.NameAr;
        $scope.Customer.RegionNameEn = $scope.selectedRegion.NameEn;

    }

    $scope.selectedPaymentTypesChanged = function (SelectedPaymentType) {
        debugger;
        if (SelectedPaymentType) {
            $scope.SelectedPaymentType = SelectedPaymentType;
        }
        $scope.Customer.PaymentTypeNameAr = $scope.SelectedPaymentType.NameAr;
        $scope.Customer.PaymentTypeNameEn = $scope.SelectedPaymentType.NameEn;
    }

    $scope.open = function (Customer) {
        debugger;
        $('custom-Uploader').each(function () {
            var el = angular.element($(this));
            $compile(el)($scope);
        });
        $scope.action = Customer == null ? 'add' : 'edit';
        //$scope.FrmAddUpdate.$setPristine();
        //$scope.FrmAddUpdate.$setUntouched();
        $scope.isFrmRowformInvalid = false;
        $scope.isSecondFrmRowformInvalid = false;
        if (Customer == null) {
            Customer = { ContractImages: [], CustomerInstallments: [] };
            $scope.Customer = angular.copy(Customer);
        }
        else {
            var AccountTypeIndex = $scope.AccountTypes.indexOf($filter('filter')($scope.AccountTypes, { 'ID': Customer.AccountTypeID }, true)[0]);
            $scope.SelectedAccountType = $scope.AccountTypes[AccountTypeIndex];

            var PaymentTypeIndex = $scope.PaymentTypes.indexOf($filter('filter')($scope.PaymentTypes, { 'ID': Customer.PaymentTypeID }, true)[0]);
            $scope.SelectedPaymentType = $scope.PaymentTypes[PaymentTypeIndex];

            var CountrieIndex = $scope.Countries.indexOf($filter('filter')($scope.Countries, { 'ID': Customer.CountryID }, true)[0]);
            $scope.selectedCountry = $scope.Countries[CountrieIndex];



            //get cities of selected country
            CustomerAccountsApi.GetCitiesByCountryId(Customer.CountryID).then(function (response) {
                debugger;
                $scope.cities = response.data;
                var CityIndex = $scope.cities.indexOf($filter('filter')($scope.cities, { 'ID': Customer.CityID }, true)[0]);
                $scope.selectedCity = $scope.cities[CityIndex];

            });

            //get region of selected city
            CustomerAccountsApi.GetAllRegionsByCityId(Customer.CityID).then(function (response) {
                debugger;
                $scope.Regions = response.data;
                var regionIndex = $scope.Regions.indexOf($filter('filter')($scope.Regions, { 'ID': Customer.RegionID }, true)[0]);
                $scope.selectedRegion = $scope.Regions[regionIndex];

            });

            CustomerAccountsApi.GetACustomerInstallments(Customer.ID).then(function (response) {
                if (response.data.length != 0)
                $scope.Customer.CustomerInstallments = response.data;

                //$scope.Customer = angular.copy(Customer);

            });
            CustomerAccountsApi.GetContractImages(Customer.ID).then(function (response) {
                debugger;
                $rootScope.ViewLoading = true;
                if (response.data.length != 0 )
                   $scope.Customer.ContractImages = response.data;
                //$scope.Customer = angular.copy(Customer);
                $rootScope.ViewLoading = false;

            });

            //$scope.Customer.RegionID = $scope.selectedRegion.ID;
        }
        $scope.invalidupdateAddFrm = true;
        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;


        $timeout(function () {
            document.querySelector('select[name="LstAccountType"]').focus();
        }, 500);


    }

    $scope.Restore = function (Customer) {
        debugger;
        $scope.Customer = angular.copy(Customer);
        $scope.Customer.IsDeleted = false;
        $scope.action = 'restore';
        $scope.save();
    }


    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        debugger;
        var c = $scope.Customer;
        if ($scope.action != 'delete' && $scope.action != 'restore') {
            $scope.Customer.AccountTypeID = $scope.SelectedAccountType.ID;
            $scope.Customer.CountryID = $scope.selectedCountry.ID;
            $scope.Customer.CityID = $scope.selectedCity.ID;
            $scope.Customer.RegionID = $scope.selectedRegion.ID;
            $scope.Customer.PaymentTypeID = $scope.SelectedPaymentType.ID;


        }
        CustomerAccountsApi.Save($scope.Customer).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {

                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Customers.indexOf($filter('filter')($scope.Customers, { 'ID': $scope.Customer.ID }, true)[0]);
                            $scope.Customers[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.Customers.indexOf($filter('filter')($scope.Customers, { 'ID': $scope.Customer.ID }, true)[0]);
                            $scope.Customers[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Customers.indexOf($filter('filter')($scope.Customers, { 'ID': $scope.Customer.ID }, true)[0]);
                            $scope.Customers[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Customers.push(angular.copy(response.data));
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
            $rootScope.ViewLoading = false;
            ss = response;
        });
    }



    $scope.back = function () {
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowTableData = true;
    }

    $scope.Delete = function (Customer) {
        debugger;
        $scope.action = 'delete';
        $scope.Customer = Customer;
        $scope.Customer.IsDeleted = true;
        $scope.save();
    }






    $scope.removeCustomerInstallment = function (CustomerInstallment) {
        debugger;
        if (CustomerInstallment.ID == null) {
            var index = $scope.Customer.CustomerInstallments.indexOf($filter('filter')($scope.Customer.CustomerInstallments, { '$$hashKey': CustomerInstallment.$$hashKey }, true)[0]);
            $scope.Customer.CustomerInstallments.splice(index, 1);
        }
        else {
            var index = $scope.Customer.CustomerInstallments.indexOf($filter('filter')($scope.Customer.CustomerInstallments, { 'ID': CustomerInstallment.ID }, true)[0]);
            CustomerInstallment.IsDeleted = true;
            $scope.Customer.CustomerInstallments[index] = angular.copy(CustomerInstallment);
        }
        //$scope.$apply();
    };

    $scope.addCustomerInstallment = function () {
        debugger;
        var c = /^([a-z0-9]{5,})$/.test('abc1');
        $scope.inserted = {
            Percentage: null,
        };
        $scope.Customer.CustomerInstallments.push($scope.inserted);
        $scope.isFrmRowformInvalid = true;
        $scope.isSecondFrmRowformInvalid = true;
    };

    $scope.cancelRowform = function (rowform, CustomerInstallment) {
        debugger;
        !$scope.isFrmRowformInvalid ? rowform.$cancel() : $scope.removeCustomerInstallment(CustomerInstallment)
        $scope.isSecondFrmRowformInvalid = false;
        $scope.isFrmRowformInvalid = false;

    }

    $scope.cancelSecondRowform = function (rowform, CustomerInstallment) {
        debugger;
        !$scope.isSecondFrmRowformInvalid ? rowform.$cancel() : $scope.removeCustomerInstallment(CustomerInstallment)
        $scope.isSecondFrmRowformInvalid = false;
        $scope.isFrmRowformInvalid = false;
    }


    $scope.validateCity = function ($data) {
        $scope.isFrmRowformInvalid = false;
        if ($data == null) {
            $scope.isFrmRowformInvalid = true;
            return "Please Choose a City";
        }
    }



    $scope.validateInstallmentPercentage = function ($data) {
        $scope.isFrmRowformInvalid = false;
        debugger;
        if ($data == null) {
            $scope.isFrmRowformInvalid = true;
            return "Please enter the Installment Percentage";
        }

        if (!/^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$/.test($data)) {
            $scope.isFrmRowformInvalid = true;
            return "Installment Percentage Fieled shouldn't be less than zero";
        }

    }




    $scope.openDatePicker = open;
    $scope.opened = false;
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[3];
    $scope.options = {
        showWeeks: false
    };

    function open() {
        $scope.opened = true;
    }

    //$scope.$watch($scope.Customer.InstallementNos, function (newValue, oldValue) {
    //    debugger;
    //    alert(newValue);
    //}, true);
    $scope.InstalmentsNumberChanged = function () {
        debugger;
        $scope.Customer.CustomerInstallments = [];
        if ($scope.Customer.InstallementNos) {
            for (var i = 0; i < $scope.Customer.InstallementNos; i++) {
                $scope.addCustomerInstallment();
            }
        }

    }



}