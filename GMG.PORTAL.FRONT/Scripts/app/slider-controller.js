var app = angular.module('slider', []);

app.controller('sliderctrl', function ($scope, $http) {

    $http.get("https://whispering-woodland-9020.herokuapp.com/getAllBooks")
        .then(function (response) {
            $scope.data = response.data;
        });
});