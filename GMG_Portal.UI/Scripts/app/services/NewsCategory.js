provide.service('NewsCategoryApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/NewsCategory/GetAllWithDeleted');
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