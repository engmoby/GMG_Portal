controllerProvider.register('DepartmentsController', ['$scope', 'DepartmentsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', DepartmentsController]);


function DepartmentsController($scope, DepartmentsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    DepartmentsApi.GetAll().then(function (response) {
        $scope.Departments = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Department) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Department == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Department == null) Department = { 'NameAr': '', 'NameEn': '' };
        $scope.Department = angular.copy(Department);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 500);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        debugger;
        DepartmentsApi.Save($scope.Department).then(function (response) {

     
            switch (response.data.OperationStatus) {
               

                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Departments.indexOf($filter('filter')($scope.Departments, { 'ID': $scope.Department.ID }, true)[0]);
                            $scope.Departments[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Departments.indexOf($filter('filter')($scope.Departments, { 'ID': $scope.Department.ID }, true)[0]);
                            $scope.Departments[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.Departments.indexOf($filter('filter')($scope.Departments, { 'ID': $scope.Department.ID }, true)[0]);                           
                            $scope.Departments[index] = angular.copy(response.data);
                            toastr.success($('#HUnDeleteSuccessMessage').val(), 'Success');
                           
                            break;
                        case 'add':
                            $scope.Departments.push(angular.copy(response.data));
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

    $scope.Delete = function (Department) {
        $scope.action = 'delete';
        $scope.Department = Department;
        $scope.Department.IsDeleted = true;
        $scope.save();
    }
    $scope.Restore = function (Department) {
        $scope.action = 'restore';
        $scope.Department = Department;
        $scope.Department.IsDeleted = false;
        $scope.save();
    }
}





