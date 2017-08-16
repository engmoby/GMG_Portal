provide.service('CoreValuesApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;

    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/CoreValues/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (CoreValues) {
        return $http({
            url: apiUrl + '/SystemParameters/CoreValues/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(CoreValues)
        });
    }
});