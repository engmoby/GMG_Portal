provide.service('UsersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Admin/GetAllWithDeleted');
    }

    this.Save = function (user) {
        return $http({
            url: apiUrl + '/SystemParameters/Admin/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(user)
        });
    }
});