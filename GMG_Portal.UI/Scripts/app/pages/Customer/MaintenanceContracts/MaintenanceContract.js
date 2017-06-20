controllerProvider.register('MaintenanceContractsController', ['$scope', 'MaintenanceContractsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', MaintenanceContractsController]);


function MaintenanceContractsController($scope, MaintenanceContractsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;

    $scope.ProductsList = [];
    $scope.customerObject = {
        selectedCustomer: [{}]
    };
    $scope.ProductCategories = [];
    $scope.productObject = {
        selectedproduct: [{}]
    };

    $scope.productCategoryObject = {
        selectedproductCategory: [{}]
    };
    $scope.MaintenanceContractProductsSelected = [];
    $rootScope.ViewLoading = true;
    $scope.selectedCustomer = null;
    $scope.CustomerContractDetails = null;
    $scope.selectedMaintenanceFrequency = null;
    $scope.selectedPaymentFrequency = null;
    $scope.Frequencies = null;
    MaintenanceContractsApi.GetAll().then(function (response) {
        $scope.MaintenanceContracts = response.data;
        $rootScope.ViewLoading = false;
    });

    $scope.open = function (MaintenanceContract) {
        debugger;
        //MaintenanceContractsApi.GetAllProducts().then(function (response) {
        //    //  $scope.MaintenanceContract.MaintenanceContractProducts = response.data;
        //    $scope.ProductsList = response.data;
        //    //console.log($scope.ProductsList);
        //});

        $scope.ProductsList = [];

        //get ProductCategories
        MaintenanceContractsApi.GetAllProductCategories().then(function (response) {
            $scope.ProductCategories = response.data;

            if ($scope.ProductCategories.length > 0) {
                $scope.selectedProductCategory = $scope.ProductCategories[0];

                MaintenanceContractsApi.GetProductsByProductCategoryId($scope.selectedProductCategory.ID).then(function (response) {
                    $scope.Products = response.data;
                    if ($scope.Products.length > 0) {
                        $scope.selectedProduct = $scope.Products[0];

                        MaintenanceContractsApi.GetAllSparePartsByProductId($scope.selectedProduct.ID).then(function (response) {
                            $scope.SpareParts = response.data;
                        });
                    }
                });
            }
            $rootScope.ViewLoading = false;

        });

        $scope.invalidupdateAddFrm = true;
        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;
        $scope.action = MaintenanceContract == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (MaintenanceContract == null) {
            $scope.selectedPaymentFrequency = null;
            $scope.selectedMaintenanceFrequency = null;
            $scope.selectedCustomer = null;
            MaintenanceContract = {};
        }
        else {
            var paymentFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.PaymentFrequencyID }, true)[0]);
            $scope.selectedPaymentFrequency = $scope.Frequencies[paymentFrequencyIndex];

            var maintenanceFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.MaintenanceFrequencyID }, true)[0]);
            $scope.selectedMaintenanceFrequency = $scope.Frequencies[maintenanceFrequencyIndex];

            var customerIndex = $scope.Customers.indexOf($filter('filter')($scope.Customers, { 'ID': MaintenanceContract.CustomerID }, true)[0]);
            $scope.selectedCustomer = $scope.Customers[customerIndex];
         //   $scope.ProductsList = MaintenanceContract.MaintenanceContractProducts;

            for (var i = 0; i < MaintenanceContract.MaintenanceContractProducts.length; i++) {
               
                $scope.productObj = $filter('filter')($scope.Products, { ID: MaintenanceContract.MaintenanceContractProducts[i].ProductID })[0];
                 
                $scope.ProductsList.push($scope.productObj);
            }

        }

        $scope.MaintenanceContract = angular.copy(MaintenanceContract);
        $timeout(function () {
            //document.querySelector('input[name="AutoSearchTxt"]').focus();
        }, 500);
    }

    $scope.back = function () {
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowTableData = true;
        //$('#ModelAddUpdate').modal('hide');
    }

    $scope.save = function () {

        $rootScope.ViewLoading = true;
        debugger;

        //   $scope.MaintenanceContracts.MaintenanceContractProducts = [];

        for (var i = 0; i < $scope.ProductsList.length; i++) {
            $scope.MaintenanceContract.MaintenanceContractProducts.push($scope.ProductsList[i]);
            $scope.MaintenanceContract.MaintenanceContractProducts[i].ProductID = $scope.ProductsList[i].ID;
            // $scope.ProductsList.push($scope.MaintenanceContractProductsSelected[i]);
        }
        MaintenanceContractsApi.Save($scope.MaintenanceContract).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.MaintenanceContracts.indexOf($filter('filter')($scope.MaintenanceContracts, { 'ID': $scope.MaintenanceContract.ID }, true)[0]);
                            $scope.MaintenanceContracts[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.MaintenanceContracts.indexOf($filter('filter')($scope.MaintenanceContracts, { 'ID': $scope.MaintenanceContract.ID }, true)[0]);
                            $scope.MaintenanceContracts.splice(index, 1);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.MaintenanceContracts.push(angular.copy(response.data));
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
                    break;
                case "CustomerContractAlreadyExist":
                    toastr.error($('#HCustomerContractAlreadyExist').val(), 'Error');
                default:

            }
            MaintenanceContractsApi.GetAll().then(function (response) {
                $scope.MaintenanceContracts = response.data;
                $rootScope.ViewLoading = false;
            });
            $rootScope.ViewLoading = false;
            $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }



    $scope.Delete = function (MaintenanceContract) {
        $scope.action = 'delete';
        $scope.MaintenanceContract = MaintenanceContract;
        $scope.MaintenanceContract.IsDeleted = true;
        $scope.save();
    }

    $scope.showCustomer = function (MaintenanceContract) {
        var selected = [];
        if (MaintenanceContract.CustomerID) {
            selected = $filter('filter')($scope.Customers, { ID: MaintenanceContract.CustomerID });
        }
        return selected != null ? selected[0].FirstNameEn : 'Not set';
    };

    MaintenanceContractsApi.GetAllCustomer().then(function (response) {

        $rootScope.ViewLoading = true;
        $scope.Customers = response.data;
        $rootScope.ViewLoading = false;

    });

    MaintenanceContractsApi.GetAllFrequencies().then(function (response) {
        $scope.Frequencies = response.data;
        for (var i = 0; i < $scope.Frequencies.length; i++) {
            if ($scope.Frequencies[i].IsCurrent == true) {
                $scope.selectedMaintenanceFrequency = $scope.Frequencies[i];
                $scope.selectedPaymentFrequency = $scope.Frequencies[i];
                break;
            }
        }
        $rootScope.ViewLoading = false;
    });

    //$scope.selectedCustomer = function (selected) {
    //    if (selected) {
    //        $scope.selectedCustomer = selected.originalObject;
    //        MaintenanceContractsApi.GetCustomerContract(selected.originalObject.ID).then(function (response) {

    //            $rootScope.ViewLoading = true;
    //            $scope.MaintenanceContract = response.data;
    //            //console.log($scope.MaintenanceContract);
    //            $rootScope.ViewLoading = false;

    //        });
    //    }
    //}


    $scope.addProduct = function () {
        for (var h = 0; h < $scope.productObject.selectedproduct.length; h++) {

            if ($scope.containsObject($scope.productObject.selectedproduct[h], $scope.ProductsList)) {

            } else
                $scope.ProductsList.push($scope.productObject.selectedproduct[h]);
        }
    };
    $scope.deleteProduct = function (index) {
        $scope.ProductsList.splice(index, 1);
    }
    $scope.containsObject = function (obj, list) {
        var i;
        for (i = 0; i < list.length; i++) {
            if (angular.equals(list[i], obj)) {
                return true;
            }
        }

        return false;
    };

    $scope.selectedMaintenanceFrequencyChanged = function (selectedItem) {
        if (selectedItem != null) {
            $scope.selectedMaintenanceFrequency = selectedItem;
        }

    };

    $scope.selectedPaymentFrequencyChanged = function (selectedItem) {
        if (selectedItem != null) {
            $scope.selectedPaymentFrequency = selectedItem;
        }

    };



    $scope.selectedCustomerChanged = function (selectedItem) {
        if (selectedItem != null) {
            MaintenanceContractsApi.GetCustomerContract(selectedItem.ID).then(function (response) {
                debugger;
                $scope.selectedCustomer = selectedItem.ID;
                $scope.MaintenanceContract = response.data;
                $scope.selectedPaymentFrequency = null;
                $scope.selectedMaintenanceFrequency = null;
                var paymentFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.MaintenanceContract.PaymentFrequencyID }, true)[0]);
                $scope.selectedPaymentFrequency = $scope.Frequencies[paymentFrequencyIndex];
                var maintenanceFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.MaintenanceContract.MaintenanceFrequencyID }, true)[0]);
                $scope.selectedMaintenanceFrequency = $scope.Frequencies[maintenanceFrequencyIndex];

            });
        }
    };
    $scope.startDateChanged = function () {
        console.log('the start date changed!');
        console.log($scope.startDate);
        //make AJAX call, which only happens once
    }
    $scope.$watch('customerObject.selectedCustomer', function () {
        for (var i = 0; i < $scope.customerObject.selectedCustomer.length; i++) {
            if ($scope.customerObject.selectedCustomer[i].ID != null) {
                MaintenanceContractsApi.GetCustomerContract($scope.customerObject.selectedCustomer[i].ID).then(function (response) {
                    debugger;
                    $scope.MaintenanceContract = response.data;
                    var paymentFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.PaymentFrequencyID }, true)[0]);
                    $scope.selectedPaymentFrequency = $scope.Frequencies[paymentFrequencyIndex];
                    var maintenanceFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.MaintenanceFrequencyID }, true)[0]);
                    $scope.selectedMaintenanceFrequency = $scope.Frequencies[maintenanceFrequencyIndex];

                });
            }
        }
    });
    $scope.$watch('productCategoryObject.selectedproductCategory', function () {
        for (var i = 0; i < $scope.productCategoryObject.selectedproductCategory.length; i++) {
            if ($scope.productCategoryObject.selectedproductCategory[i].ID != null) {
                MaintenanceContractsApi.GetProductsByProductCategoryId($scope.productCategoryObject.selectedproductCategory[i].ID).then(function (response) {
                    debugger;
                    for (var k = 0; k < response.data.length; k++) {
                        if ($scope.containsObject(response.data[k], $scope.Products)) {

                        } else
                            $scope.Products.push(response.data[k]);
                    }
                    if ($scope.Products.length > 0)
                        $scope.selectedProduct = $scope.Products[0];
                    $rootScope.ViewLoading = false;
                });
            }
        }
    });
    $scope.$watch('[$scope.MaintenanceContract.ContractDate,$scope.MaintenanceContract.EndDate]', function (newValue, oldValue) {
        console.log(newValue);
        console.log(oldValue);
    }, true);
    $scope.$watch('$scope.MaintenanceContract.ContractDate', function () {
        for (var i = 0; i < $scope.customerObject.selectedCustomer.length; i++) {
            if ($scope.customerObject.selectedCustomer[i].ID != null) {
                MaintenanceContractsApi.GetCustomerContract($scope.customerObject.selectedCustomer[i].ID).then(function (response) {
                    debugger;
                    $scope.MaintenanceContract = response.data;
                    var paymentFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.PaymentFrequencyID }, true)[0]);
                    $scope.selectedPaymentFrequency = $scope.Frequencies[paymentFrequencyIndex];
                    var maintenanceFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': MaintenanceContract.MaintenanceFrequencyID }, true)[0]);
                    $scope.selectedMaintenanceFrequency = $scope.Frequencies[maintenanceFrequencyIndex];

                });
            }
        }
    });
    $scope.selectedCustomerChanged = function (selectedItem) {
        if (selectedItem != null) {
            MaintenanceContractsApi.GetCustomerContract(selectedItem.ID).then(function (response) {
                debugger;
                $scope.selectedCustomer = selectedItem.ID;
                $scope.MaintenanceContract = response.data;
                $scope.selectedPaymentFrequency = null;
                $scope.selectedMaintenanceFrequency = null;
                var paymentFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.MaintenanceContract.PaymentFrequencyID }, true)[0]);
                $scope.selectedPaymentFrequency = $scope.Frequencies[paymentFrequencyIndex];
                var maintenanceFrequencyIndex = $scope.Frequencies.indexOf($filter('filter')($scope.Frequencies, { 'ID': $scope.MaintenanceContract.MaintenanceFrequencyID }, true)[0]);
                $scope.selectedMaintenanceFrequency = $scope.Frequencies[maintenanceFrequencyIndex];

            });
        }
    };

    $scope.openProduct = function () {
        debugger;

        $scope.invalidupdateAddFrm = true;

        $('#ModelAddUpdate').modal('show');

    }

    $scope.backProduct = function () {
        $('#ModelAddUpdate').modal('hide');
    }
    $scope.openStartCalender = openStartCalender;
    $scope.openEndCalender = openEndCalender;
    $scope.openDueCalender = openDueCalender;
    $scope.openedStartCalender = false;
    $scope.openedEndCalender = false;
    $scope.openedDueCalender = false;
    $scope.formats = ['fullDate', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.options = {
        showWeeks: false
    };

    function openStartCalender() {
        $scope.openedStartCalender = true;
    }
    function openEndCalender() {
        $scope.openedEndCalender = true;
    }
    function openDueCalender() {
        $scope.openedDueCalender = true;
    }

}





