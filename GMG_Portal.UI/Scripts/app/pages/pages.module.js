/**
 * @author v.lugovsky
 * created on 16.12.2015
 */

var controllerProvider = null;
var provide = null;

(function () {
    'use strict';

    angular.module('BlurAdmin.pages', [
      'ui.router',
      'ngMessages'
    ], function ($controllerProvider, $provide) {
        controllerProvider = $controllerProvider;
        provide = $provide;
    }).config(routeConfig);

    var addRoute = function ($stateProvider, menuItem) {       
        $stateProvider
          .state(menuItem.stateRef, {
              url: '/'+menuItem.stateRef,
              templateUrl: menuItem.templateUrl,
              title: menuItem.title
          });
    }

    var addRouteStates = function ($stateProvider, menuItem) {
        if (menuItem.stateRef != null && menuItem.templateUrl!=null)
            addRoute($stateProvider, menuItem);
        if (Object.prototype.toString.call(menuItem.subMenu) === '[object Array]') {
            for (var i = 0; i < menuItem.subMenu.length; i++) {
                addRouteStates($stateProvider, menuItem.subMenu[i]);
            }
        }        
    }

    ///** @ngInject */
    function routeConfig($urlRouterProvider, $stateProvider) {

        //$stateProvider
        //  .state(menuItem.stateRef, {
        //      url: '/'+menuItem.stateRef,
        //      templateUrl: menuItem.templateUrl,
        //      title: menuItem.title
        //  });
        debugger;
        for (var i = 0; i < menuItems.length; i++) {
            addRouteStates($stateProvider,menuItems[i]);
        }        
        $urlRouterProvider.otherwise('/HomePage');
    }

})();
