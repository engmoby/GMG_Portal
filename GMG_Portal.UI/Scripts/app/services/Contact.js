provide.service('ContactApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;



    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/ContactUs/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (Contact) {
        return $http({
            url: apiUrl + '/SystemParameters/ContactUs/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Contact)
        });
    }
});