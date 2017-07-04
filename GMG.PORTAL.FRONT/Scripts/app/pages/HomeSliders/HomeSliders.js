controllerProvider.register('HomeSlidersController', ['$scope', 'HomeSliders', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomeSlidersController]);
function HomeSlidersController($scope, homeSliders, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $rootScope.ViewLoading = true;
    homeSliders.GetAll().then(function (response) {
        $scope.HomeSliders = response.data;
        $rootScope.ViewLoading = false;
    });

}





