 
function config($stateProvider, $urlRouterProvider) { 
    $stateProvider 
        .state('home', {
            url: "/home",
            templateUrl: "/partial/app/home/home.html"

        })
        .state('user', {
            url: "/user",
            templateUrl: "/partial/app/user/user.html"

        })
        .state('currency', {
            url: "/currency",
            templateUrl: "/partial/app/currency/currency.html"

        });
    $urlRouterProvider.otherwise('/home');


}