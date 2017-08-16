controllerProvider.register('VisionsController', ['$scope', 'VisionsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', VisionsController]);
function VisionsController($scope, VisionsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;
        VisionsApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Visions = response.data;
            $rootScope.ViewLoading = false;
        });
    });
    $rootScope.ViewLoading = true;

    $scope.letterLimit = 20;

    VisionsApi.GetAll().then(function (response) {
        $scope.Visions = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (vision) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = vision == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (vision == null) vision = { };
        $scope.Vision = angular.copy(vision); 
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
     
    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.Vision.LangId = CurrentLanguage;

        VisionsApi.Save($scope.Vision).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Visions.indexOf($filter('filter')($scope.Visions, { 'Id': $scope.Vision.Id }, true)[0]);
                            $scope.Visions[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Visions.indexOf($filter('filter')($scope.Visions, { 'Id': $scope.Vision.Id }, true)[0]);
                            $scope.Visions[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Visions.push(angular.copy(response.data));
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

            $rootScope.ViewLoading = false;
            $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    

}
  





