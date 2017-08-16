provide.service('AboutApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;

    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/About/GetAllWithDeleted?LangId=' + lang);
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