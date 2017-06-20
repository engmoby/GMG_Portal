provide.service('MaintenanceContractsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/MaintenanceContracts/GetAll');
    }

    this.Save = function (AccountType) {
        return $http({
            url: ApiURL + '/SystemParameters/MaintenanceContracts/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(AccountType)
        });
    }


    this.GetAllCustomer = function () {
        return $http.get(ApiURL + '/SystemParameters/MaintenanceContracts/GetAllCustomer');
    }

    this.GetAllEmployee = function () {
        return $http.get(ApiURL + '/SystemParameters/MaintenanceContracts/GetAllEmployee');
    }
    this.GetCustomerContract = function (customerId) {
        return $http.get(ApiURL + '/SystemParameters/MaintenanceContracts/GetCustomerContract?customerId=' + customerId);
    }

    this.GetAllFrequencies = function () {
        return $http.get(ApiURL + '/SystemParameters/Frequencies/GetAll');
    }

    this.GetAllProducts = function () {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAll');
    }

    this.GetAllProductCategories = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductCategory/GetAllLeafsRelatedWithProducts');
    }
    this.GetProductsByProductCategoryId = function (ProductCategoryId) {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAll?ProductCategoryID=' + ProductCategoryId);
    }

    this.GetAllSparePartsByProductId = function (ProductId) {
        return $http.get(ApiURL + '/SystemParameters/SpareParts/GetAll?ProductId=' + ProductId);
    }

});