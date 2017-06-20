provide.service('RegionsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;

    this.GetAllCountries = function () {
        return $http.get(ApiURL + '/SystemParameters/Countries/GetAll');
    }

    this.GetCitiesByCountryId = function (countyId) {
        return $http.get(ApiURL + '/SystemParameters/Cities/GetByCountryId?countryId=' + countyId);
    }

    this.GetAllRegionsByCityId = function (cityId) {
        return $http.get(ApiURL + '/SystemParameters/Regions/GetAllWithDeleted?cityId=' + cityId);
    }

    this.Save = function (Region) {
        return $http({
            url: ApiURL + '/SystemParameters/Regions/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Region)
        });
    }
});