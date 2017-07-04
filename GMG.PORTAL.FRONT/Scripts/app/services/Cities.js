provide.service('CitiesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;

    this.GetAllCountries = function () {
        return $http.get(ApiURL + '/SystemParameters/Countries/GetAll');
    }

    this.GetCitiesByCountryId = function (countyId) {
        return $http.get(ApiURL + '/SystemParameters/Cities/GetByCountryIdWithDeleted?countryId=' + countyId);
    }

    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Cities/GetAllWithDeleted');
    }

    this.Save = function (City) {
        return $http({
            url: ApiURL + '/SystemParameters/Cities/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(City)
        });
    }
});