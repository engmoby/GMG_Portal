controllerProvider.register('AboutController', ['$scope', 'appCONSTANTS', 'AboutApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', AboutController]);
function AboutController($scope, appCONSTANTS, AboutApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.oneAtATime = true;
   

    $scope.language = appCONSTANTS.supportedLanguage;
    var langId = document.querySelector('#HCurrentLang').value;
   
    $scope.CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        $scope.CurrentLanguage = selectedValue;

        debugger;

        AboutApi.GetAll($scope.CurrentLanguage).then(function (response) {
            $scope.About = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    AboutApi.GetAll($scope.CurrentLanguage).then(function (response) {
        $scope.About = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (aboutObj) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = aboutObj == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (aboutObj == null) aboutObj = {};
        $scope.aboutObj = angular.copy(aboutObj);

    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;

        $scope.aboutObj.LangId = $scope.CurrentLanguage;

        AboutApi.Save($scope.aboutObj).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.About.indexOf($filter('filter')($scope.About, { 'Id': $scope.aboutObj.Id }, true)[0]);
                            $scope.About[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.About.indexOf($filter('filter')($scope.About, { 'Id': $scope.About.Id }, true)[0]);
                            $scope.About[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.About.push(angular.copy(response.data));
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






