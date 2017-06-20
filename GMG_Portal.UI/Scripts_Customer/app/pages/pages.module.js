var controllerProvider = null;
var provide = null;


(function () {
    'use strict';

    angular.module('CustomerModule.pages', [
      'ui.router',
      'ngMessages'
    ], function ($controllerProvider, $provide) {
        controllerProvider = $controllerProvider;
        provide = $provide;
    }).config(routeConfig);

   

    ///** @ngInject */
    function routeConfig($urlRouterProvider, $stateProvider) {

        $stateProvider
          .state("Home", {
              url: '/Home',
              views:
                    {
                        '': { templateUrl: '/Customer/Products' },
                        'slider': { templateUrl: '/Customer/slider' },
                    }
          }).state("SearchCategory", {
              url: '/SearchCategory',
              views:
                    {
                        '': { templateUrl: '/SearchCategory' }
                    }
          });

        //$stateProvider
        //  .state("SearchCategory", {
        //      url: '/SearchCategory',
        //      templateUrl: '/SearchCategory'
        //  });




        $urlRouterProvider.otherwise('/Home');
    }

})();
