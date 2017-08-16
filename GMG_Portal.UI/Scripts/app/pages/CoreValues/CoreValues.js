controllerProvider.register('CoreValuesController', ['$scope', 'CoreValuesApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CoreValuesController]);
function CoreValuesController($scope, CoreValuesApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        CoreValuesApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.CoreValues = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CoreValuesApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.CoreValues = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Values) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Values == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Values == null) Values = {};
        $scope.Values = angular.copy(Values);
        if ($scope.Values.Image)
            $scope.countFiles = $scope.Values.Image;
         
    } 
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Values) {
        debugger;
        $scope.Values = angular.copy(Values);
        $scope.Values.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        $scope.Values.LangId = CurrentLanguage;

        CoreValuesApi.Save($scope.Values).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.CoreValues.indexOf($filter('filter')($scope.CoreValues, { 'Id': $scope.Values.Id }, true)[0]);
                             $scope.CoreValues[index] = angular.copy(response.data);
                            //CoreValuesApi.GetAll().then(function (response) {
                            //    $scope.CoreValues = response.data; 
                            //}); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.CoreValues.indexOf($filter('filter')($scope.CoreValues, { 'Id': $scope.Values.Id }, true)[0]);
                             $scope.CoreValues[index] = angular.copy(response.data);
                            //CoreValuesApi.GetAll().then(function (response) {
                            //    $scope.CoreValues = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //CoreValuesApi.GetAll().then(function (response) {
                            //    $scope.CoreValues = response.data;
                            //});
                             $scope.CoreValues.push(angular.copy(response.data));
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
                    toastr.error($('#HCannotDeleted').val(), 'Error');
                    break;
                default:

            }

            $scope.back();
            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (Values) {
        $scope.action = 'delete';
        $scope.Values = Values;
        $scope.Values.IsDeleted = true;
        $scope.save();
    }
     
}






