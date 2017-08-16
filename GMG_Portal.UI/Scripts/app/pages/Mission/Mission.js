controllerProvider.register('MissionsController', ['$scope', 'MissionsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', MissionsController]);
function MissionsController($scope, MissionsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
  
    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;
        debugger;
        MissionsApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Missions = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $rootScope.ViewLoading = true;
    $scope.letterLimit = 20;
    MissionsApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.Missions = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (mission) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = mission == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (mission == null) mission = {};
        $scope.Mission = angular.copy(mission);

    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
 
    $scope.save = function () {
        $scope.back();

        $rootScope.ViewLoading = true;
        $scope.Mission.LangId = CurrentLanguage;

        MissionsApi.Save($scope.Mission).then(function (response) {
            console.log(response);
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Missions.indexOf($filter('filter')($scope.Missions, { 'Id': $scope.Mission.Id }, true)[0]);
                            $scope.Missions[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Missions.indexOf($filter('filter')($scope.Missions, { 'Id': $scope.Mission.Id }, true)[0]);
                            $scope.Missions[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Missions.push(angular.copy(response.data));
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
            $scope.FrmAddUpdate.$setPristine();
            $scope.FrmAddUpdate.$setUntouched();
            $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }
     
}






