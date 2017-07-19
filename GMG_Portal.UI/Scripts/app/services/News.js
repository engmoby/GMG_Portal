provide.service('NewsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/News/GetAllWithDeleted');
    }

    this.Save = function (news) {
        return $http({
            url: apiUrl + '/SystemParameters/News/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(news)
        });
    }
});