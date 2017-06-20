provide.service('CouriersApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Couriers/GetAllWithDeleted');
    }

    
    this.GetAllProductSizes = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductSizes/GetAll');
    }

   
    this.GetCitiesByCountryId = function (countyId) {
        return $http.get(ApiURL + '/SystemParameters/Cities/GetByCountryId?countryId=' + countyId);
    }

    this.GetAllShipmentTypes = function (countyId) {
        return $http.get(ApiURL + '/SystemParameters/ShipmentTypes/GetAll');
    }

    this.SaveCourierDetails = function (CourierDetail) {
        return $http({
            url: ApiURL + '/SystemParameters/CourierDetails/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(CourierDetail)
        });
    }

    this.Save = function (Courier) {
        return $http({
            url: ApiURL + '/SystemParameters/Couriers/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Courier)
        });
    }


});