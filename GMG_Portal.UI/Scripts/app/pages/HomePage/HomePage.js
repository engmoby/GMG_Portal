controllerProvider.register('HomePageController', ['$scope', 'HomePageApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HomePageController]);
function HomePageController($scope, HomePageApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        HomePageApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.HomePage = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    HomePageApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.HomePage = response.data;
        $rootScope.ViewLoading = false;
    });
   

   
}






