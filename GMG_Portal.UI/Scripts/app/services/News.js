provide.service('NewsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    
   
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/News/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (New) {
        return $http({
            url: apiUrl + '/SystemParameters/News/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(New)
        });
    }

    this.GetAllCategories = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/NewsCategory/GetAllWithDeleted?LangId=' + lang);
    }
});