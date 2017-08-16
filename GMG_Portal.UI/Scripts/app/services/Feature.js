provide.service('FeaturesApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Features/GetAllWithDeleted?LangId=' + lang);
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