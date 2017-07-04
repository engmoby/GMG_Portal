controllerProvider.register('LanguagesController', ['$scope', 'LanguagesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', LanguagesController]);
function LanguagesController($scope, LanguagesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    LanguagesApi.GetAll().then(function (response) {
        $scope.Languages = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (language) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = language == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (language == null) language = { 'NameAr': '', 'NameEn': '' };
        $scope.Language = angular.copy(language);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (language) {
        debugger;
        $scope.Language = angular.copy(language);
        $scope.Language.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        LanguagesApi.Save($scope.Language).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Languages.indexOf($filter('filter')($scope.Languages, { 'ID': $scope.Language.ID }, true)[0]);
                            $scope.Languages[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Languages.indexOf($filter('filter')($scope.Languages, { 'ID': $scope.Language.ID }, true)[0]);
                            $scope.Languages[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Languages.push(angular.copy(response.data));
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

    $scope.Delete = function (language) {
        $scope.action = 'delete';
        $scope.Language = language;
        $scope.Language.IsDeleted = true;
        $scope.save();
    }

}





