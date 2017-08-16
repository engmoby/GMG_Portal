provide.service('OwnersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Owners/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (Owner) {
        return $http({
            url: apiUrl + '/SystemParameters/Owners/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Owner)
        });
    }
});