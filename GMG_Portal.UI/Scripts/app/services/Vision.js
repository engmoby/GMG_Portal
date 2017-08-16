provide.service('VisionsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Visions/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (vision) {
        return $http({
            url: apiUrl + '/SystemParameters/Visions/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(vision)
        });
    }
});