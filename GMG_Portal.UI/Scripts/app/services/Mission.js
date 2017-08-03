provide.service('MissionsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value; 
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Missions/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (mission) {
        return $http({
            url: apiUrl + '/SystemParameters/Missions/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(mission)
        });
    }
});