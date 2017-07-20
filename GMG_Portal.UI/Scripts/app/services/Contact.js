provide.service('ContactApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/ContactUs/GetAllWithDeleted');
    }

    this.Save = function (career) {
        return $http({
            url: apiUrl + '/SystemParameters/ContactUs/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(career)
        });
    }
});