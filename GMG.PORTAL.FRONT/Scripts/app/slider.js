var app = angular.module('slider', []);

app.controller('sliderctrl', function ($scope, $http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    $http.get(ApiURL + '/SystemParameters/HomeSliders/GetAll')
        .then(function (response) {
            $scope.data = response.data;
        });
});