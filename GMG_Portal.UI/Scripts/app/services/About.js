provide.service('AboutApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 

    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/About/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (about) {
        return $http({
            url: apiUrl + '/SystemParameters/About/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(about)
        });
    }
});