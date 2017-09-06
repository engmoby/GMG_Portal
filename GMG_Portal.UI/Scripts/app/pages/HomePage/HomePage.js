controllerProvider.register('HomePageController', ['$scope', 'HomePageApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomePageController]);
function HomePageController($scope, HomePageApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    
    $scope.TotalHotels = '3';
    $scope.TotalReservation = '5';
    $scope.TotalInquiry = '7';
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
  
    HomePageApi.GetAllHotelReservation().then(function (response) {
        $scope.HomePageReservation = response.data; 
    });

  HomePageApi.GetAllAppliedCarrer().then(function (response) {
        $scope.HomePageCarrer = response.data;
        $rootScope.ViewLoading = false;
    });
   
    HomePageApi.GetAllContactInquiry().then(function (response) {
        debugger;
        $scope.HomePageInquiry = response.data;
    });


   
}






