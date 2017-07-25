provide.service('FeaturesApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Features/GetAllWithDeleted');
    }

    this.Save = function (Feature) {
        return $http({
            url: apiUrl + '/SystemParameters/Features/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Feature)
        });
    }
});