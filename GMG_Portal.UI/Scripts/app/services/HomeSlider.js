provide.service('HomeSlidersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
    this.GetAll = function (lang) {
        debugger;
        return $http.get(apiUrl + '/SystemParameters/HomeSliders/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (homeSlider) {
        return $http({
            url: apiUrl + '/SystemParameters/HomeSliders/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(homeSlider)
        });
    }
});