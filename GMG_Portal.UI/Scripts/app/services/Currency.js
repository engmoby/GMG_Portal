provide.service('CurrencyApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Currency/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (news) {
        return $http({
            url: apiUrl + '/SystemParameters/Currency/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(news)
        });
    }
});