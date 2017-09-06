controllerProvider.register('UsersController', ['$scope', 'UsersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', UsersController]);
function UsersController($scope, UsersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    UsersApi.GetAll().then(function (response) {
        $scope.Users = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (user) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = user == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (user == null) user = {};
        $scope.User = angular.copy(user);
        if ($scope.User.Image)
            $scope.countFiles = $scope.User.Image;
 
    }
    $scope.openImage = function (User) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = User == null ? 'add' : 'edit';
        if (User == null) User = {};
        $scope.User = angular.copy(User);
        //if ($scope.User.Image)
        //    $scope.countFiles = $scope.User.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (User) {
        debugger;
        $scope.User = angular.copy(User);
        $scope.User.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        UsersApi.Save($scope.User).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Users.indexOf($filter('filter')($scope.Users, { 'Id': $scope.User.Id }, true)[0]);
                            $scope.Users[index] = angular.copy(response.data);
                            //UsersApi.GetAll().then(function (response) {
                            //    $scope.Users = response.data; 
                            //}); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Users.indexOf($filter('filter')($scope.Users, { 'Id': $scope.User.Id }, true)[0]);
                            $scope.Users[index] = angular.copy(response.data);
                            //UsersApi.GetAll().then(function (response) {
                            //    $scope.Users = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //UsersApi.GetAll().then(function (response) {
                            //    $scope.Users = response.data;
                            //});
                           $scope.Users.push(angular.copy(response.data));
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
                    index = $scope.Users.indexOf($filter('filter')($scope.Users, { 'Id': $scope.User.Id }, true)[0]);
                    $scope.Users[index] = angular.copy(response.data); toastr.error($('#HCannotDeleted').val(), 'Error');
                    UsersApi.GetAll().then(function (response) {
                        $scope.Users = response.data;
                    });
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

    $scope.Delete = function (User) {
        $scope.action = 'delete';
        $scope.User = User;
        $scope.User.IsDeleted = true;
        $scope.save();
    }
  
}






