provide.service('CareersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Career/GetAllWithDeleted');
    }

    this.Save = function (career) {
        return $http({
            url: apiUrl + '/SystemParameters/Career/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(career)
        });
    }
});