controllerProvider.register('HotelsController', ['$scope', 'HotelsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HotelsController]);
function HotelsController($scope, HotelsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    HotelsApi.GetAll().then(function (response) {
        $scope.Hotels = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Hotel) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Hotel == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Hotel == null) Hotel = { 'NameAr': '', 'NameEn': '' };
        $scope.Hotel = angular.copy(Hotel);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Hotel) {
        debugger;
        $scope.Hotel = angular.copy(Hotel);
        $scope.Hotel.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        HotelsApi.Save($scope.Hotel).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Hotels.push(angular.copy(response.data));
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

    $scope.Delete = function (Hotel) {
        $scope.action = 'delete';
        $scope.Hotel = Hotel;
        $scope.Hotel.IsDeleted = true;
        $scope.save();
    }

}





