provide.service('MissionsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Missions/GetAllWithDeleted');
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