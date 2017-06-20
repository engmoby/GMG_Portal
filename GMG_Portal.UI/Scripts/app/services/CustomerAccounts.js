provide.service('CustomerAccountsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Customers/GetAllWithDeleted');
    }


    this.GetAllAccountTypes = function () {
        return $http.get(ApiURL + '/SystemParameters/AccountTypes/GetAll');
    }


    this.GetAllCountries = function () {
        return $http.get(ApiURL + '/SystemParameters/Countries/GetAll');
    }

    this.GetCitiesByCountryId = function (countyId) {
        return $http.get(ApiURL + '/SystemParameters/Cities/GetByCountryId?countryId=' + countyId);
    }

    this.GetAllRegionsByCityId = function (cityId) {
        return $http.get(ApiURL + '/SystemParameters/Regions/GetAll?cityId=' + cityId);
    }

    this.GetAllPaymentTypes = function () {
        return $http.get(ApiURL + '/SystemParameters/PaymentTypes/GetAll');
    }

    this.GetACustomerInstallments = function (CustomerID) {
        return $http.get(ApiURL + '/SystemParameters/CustomerInstallments/GetAll?CustomerId=' + CustomerID);
    }

    this.GetContractImages = function (CustomerID) {
        return $http.get(ApiURL + '/SystemParameters/Customers/GetContractImages?CustomerID=' + CustomerID);
    }

    this.SaveCourierDetails = function (CourierDetail) {
        return $http({
            url: ApiURL + '/SystemParameters/CourierDetails/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(CourierDetail)
        });
    }

    this.Save = function (Customer) {
        return $http({
            url: ApiURL + '/SystemParameters/Customers/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Customer)
        });
    }


});