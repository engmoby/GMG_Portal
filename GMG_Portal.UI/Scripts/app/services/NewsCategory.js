provide.service('NewsCategoryApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/NewsCategory/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (news) {
        return $http({
            url: apiUrl + '/SystemParameters/NewsCategory/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(news)
        });
    }
});