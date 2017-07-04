﻿provide.service('HomeSlidersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/HomeSliders/GetAllWithDeleted');
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