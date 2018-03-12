var app = angular.module("MyApp", ["ngResource", "ui.router"]);

app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider

        .state("list", {
            url: "/list",
            templateUrl: "/ngView/list.html",
            controller: "listCtrl"
        })
        .state("list.detail", {
            url: "/detail/{id}",
            templateUrl: "/ngView/detail.html",
            controller: "detailCtrl"
        })
        .state("list.detail.form", {
            url: "/form",
            templateUrl: "/ngView/form.html",
            controller: "formCtrl"
        });

    // For any unmatched url, redirect to /list
    $urlRouterProvider.otherwise("/list");
});

app.controller("listCtrl", function ($scope, APIservice) {
    APIservice.query(function (Data) {
        $scope.dataList = Data;
    });

    $scope.edit = function (id) {
        $location.path("/form/" + (id));
    }
});

app.controller("detailCtrl", function ($scope, $stateParams, APIservice) {
    $scope.dataDetail = APIservice.get({ id: $stateParams.id });
});

app.controller("formCtrl", function ($scope, $state, $stateParams, APIservice) {
    if ($stateParams.id)
        $scope.user = APIservice.get({ id: $stateParams.id });

    $scope.submit = function (user) {
        APIservice.update({ id: $stateParams.id }, user, function () {
            $state.go("list", {}, { reload: true });
        });
    }
});

app.service("APIservice", function ($resource) {
    return $resource("/api/Students/:id", null, { "update": { method: "PUT" } });
});

app.controller("HomeslidersCtrl", function ($scope, HomeSlidersApi) {
    $scope.title = "hamada";
    HomeSlidersApi.GetAll().then(function (response) {
        $scope.homeSliders = response.data;
    });
});

app.service('HomeSlidersApi', function ($http) {
   // var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get('http://localhost:2798/SystemParameters/HomeSliders/GetAll');
    }
});