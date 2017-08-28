controllerProvider.register('HomePageController', ['$scope', 'HomePageApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomePageController]);
function HomePageController($scope, HomePageApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    //var langId = document.querySelector('#HCurrentLang').value;
    //var CurrentLanguage = langId;
    //$("#DropdwonLang").change(function () {
    //    var selectedText = $(this).find("option:selected").text();
    //    var selectedValue = $(this).val();
    //    document.getElementById("HCurrentLang").value = selectedValue;
    //    CurrentLanguage = selectedValue;

    //    debugger;

    //    HomePageApi.GetAll(CurrentLanguage).then(function (response) {
    //        $scope.HomePage = response.data;
    //        $rootScope.ViewLoading = false;
    //    });
    //});
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
        $scope.HomePageInquiry = response.data; 
    });


   
}






