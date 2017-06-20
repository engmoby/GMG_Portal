provide.service('FrequenciesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Frequencies/GetAllWithDeleted');
    }

    this.Save = function (Frequency) {
        return $http({
            url: ApiURL + '/SystemParameters/Frequencies/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Frequency)
        });
    }
});