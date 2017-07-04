provide.service('CountriesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Countries/GetAllWithDeleted');
    }

    this.Save = function (Country) {
        return $http({
            url: ApiURL + '/SystemParameters/Countries/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Country)
        });
    }
});