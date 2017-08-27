var app = angular.module("Login", []);
angular.module("Login").controller("LoginController", LoginController);
var apiUrl = "http://localhost:2798/";

app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('login', {
            url: '/login',
            templateUrl: 'login.html',
            controller: 'LoginController'
        })
        .state('home', {
            url: '/home',
            templateUrl: 'home.html',
            controller: 'HomeController'
        });
}]);

function LoginController($scope, $http, LoginService, $state) {
    $scope.userObj = {};

    $scope.Login = function () {

        loginCall($scope.userObj).then(function (response) {
            debugger;
            if (response.data != null) {
                alert("Succeded");
                $state.transitionTo('home');
                window.location.pathname = 'Admin/Index';
                //  $location.path('/HomeSlider');
                //    $window.location.href = 'http://localhost:19056/#!/HomePage';
                alert("sssds");

            } else {
                alert("null");

            }
        },
          function (response) {
              debugger;
              ss = response;
          });
        function loginCall(userObj) {
            return $http({
                url: apiUrl + '/SystemParameters/Admin/Login',
                method: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(userObj)
            });
        };
    }


}
 
app.factory('LoginService', function () {
    var admin = 'admin';
    var pass = 'pass';
    var isAuthenticated = false;

    return {
        login: function (username, password) {
            isAuthenticated = username === admin && password === pass;
            return isAuthenticated;
        },
        isAuthenticated: function () {
            return isAuthenticated;
        }
    };

});