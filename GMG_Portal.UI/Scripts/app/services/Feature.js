provide.service('FeaturesApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Features/GetAllWithDeleted?langId=' + lang);
    }

    this.Save = function (feature) {
        return $http({
            url: apiUrl + '/SystemParameters/Features/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(feature)
        });
    }
});