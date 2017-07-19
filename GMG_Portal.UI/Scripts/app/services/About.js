provide.service('AboutApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/About/GetAllWithDeleted');
    }

    this.Save = function (vision) {
        return $http({
            url: apiUrl + '/SystemParameters/About/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(vision)
        });
    }
});