


var app = angular.module('portalFront', []);
app.service('HomeSliders', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/HomeSliders/GetAll');
    }


});