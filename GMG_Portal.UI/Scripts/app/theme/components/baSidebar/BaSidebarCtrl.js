/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('BlurAdmin.theme.components')
    .controller('BaSidebarCtrl', BaSidebarCtrl);
 
  /** @ngInject */
  function BaSidebarCtrl($scope, baSidebarService) {
      var menuItems = [];

      menuItems.push({ "title": "Home  ", "level": "0", "icon": "ion-android-home", "stateRef": "HomePage", "templateUrl": "/partial/app/currency/currency.html" });

      menuItems.push({
          "name": "Contactus", "title": "ContactUs", "level": "0", "icon": "ion-gear-a", "subMenu": [
              { "name": "sdsdsd", "title": "abouts", "level": "1", "stateRef": "about", "templateUrl": "/partial/app/currency/currency.html" }



          ]
      });
      $scope.menuItems = menuItems;
    $scope.defaultSidebarState = $scope.menuItems[0].stateRef;

    $scope.hoverItem = function ($event) {
      $scope.showHoverElem = true;
      $scope.hoverElemHeight =  $event.currentTarget.clientHeight;
      var menuTopValue = 66;
      $scope.hoverElemTop = $event.currentTarget.getBoundingClientRect().top - menuTopValue;
    };

    $scope.$on('$stateChangeSuccess', function () {
      if (baSidebarService.canSidebarBeHidden()) {
        baSidebarService.setMenuCollapsed(true);
      }
    });
  }
})();