angular
    .module('SurveryApp')
    .controller('CurrencysCtrl', ['$scope', 'appCONSTANTS', 'CurrencyApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CurrencysCtrl]);
function CurrencysCtrl($scope, appCONSTANTS, CurrencyApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.activePageTitle = "sdsd";
    $scope.language = appCONSTANTS.supportedLanguage;
    var langId = "en";
    $scope.CurrentLanguage = langId;
    var cm = this;
    cm.langg = 'ar';
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        $scope.CurrentLanguage = selectedValue;

        debugger;

        //CurrencyApi.GetAll($scope.CurrentLanguage).then(function (response) {
        //    $scope.CurrencyList = response.data;
        //    $rootScope.ViewLoading = false;
        //});
    });

    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CurrencyApi.GetAll($scope.CurrentLanguage).then(function (response) {
        debugger;
        $scope.CurrencyList = response.data;
        $rootScope.ViewLoading = false;
        console.log($scope.CurrencyList);
    });
    $scope.open = function (Currency) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Currency == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Currency == null) Currency = {};
        $scope.Currency = angular.copy(Currency);
        if ($scope.Currency.Image)
            $scope.countFiles = $scope.Currency.Image;

        //$timeout(function () {
        //    document.querySelector('input[name="TxtNameAr"]').focus();
        //}, 1000);
    }
    $scope.openImage = function (Currency) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Currency == null ? 'add' : 'edit';
        if (Currency == null) Currency = {};
        $scope.Currency = angular.copy(Currency);
        //if ($scope.Currency.Image)
        //    $scope.countFiles = $scope.Currency.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Currency) {
        debugger;
        $scope.Currency = angular.copy(Currency);
        $scope.Currency.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;

        $scope.Currency.langId = $scope.CurrentLanguage;

        debugger;
        CurrencyApi.Save($scope.Currency).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.CurrencyList.indexOf($filter('filter')($scope.CurrencyList, { 'Id': $scope.Currency.Id }, true)[0]);
                            $scope.CurrencyList[index] = angular.copy(response.data);
                            //CurrencyApi.GetAll().then(function (response) {
                            //    $scope.CurrencyList = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.CurrencyList.indexOf($filter('filter')($scope.CurrencyList, { 'Id': $scope.Currency.Id }, true)[0]);
                            $scope.CurrencyList[index] = angular.copy(response.data);
                            //CurrencyApi.GetAll().then(function (response) {
                            //    $scope.CurrencyList = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //CurrencyApi.GetAll().then(function (response) {
                            //    $scope.CurrencyList = response.data;
                            //});
                            $scope.CurrencyList.push(angular.copy(response.data));
                            toastr.success($('#HSaveSuccessMessage').val(), 'Success');
                            break;
                    }
                    break;
                case "NameEnMustBeUnique":
                    toastr.error($('#HEnglishNameUnique').val(), 'Error');
                    break;
                case "NameArMustBeUnique":
                    toastr.error($('#HArabicNameUnique').val(), 'Error');
                    break;
                case "HasRelationship":
                    CurrencyApi.GetAll().then(function (response) {
                        $scope.CurrencyList = response.data;
                    });
                    toastr.error($('#HCannotDeleted').val(), 'Error');
                    break;
                default:

            }

            $rootScope.ViewLoading = false;
            $scope.back();
        },
            function (response) {
                debugger;
                ss = response;
            });
    }

    $scope.Delete = function (Currency) {
        $scope.action = 'delete';
        $scope.Currency = Currency;
        $scope.Currency.IsDeleted = true;
        $scope.save();
    }

}






