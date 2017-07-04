provide.service('LanguagesApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Languages/GetAllWithDeleted');
    }

    this.Save = function (language) {
        return $http({
            url: apiUrl + '/SystemParameters/Languages/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(language)
        });
    }
});