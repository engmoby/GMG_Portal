controllerProvider.register('ReturnPolicyController', ['$scope', 'ReturnPolicyApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ReturnPolicyController]);


function ReturnPolicyController($scope, ReturnPolicyApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    debugger;
    $rootScope.ViewLoading = true;
    ReturnPolicyApi.GetAll().then(function (response) {

        if (response.data.length > 0) {
            $scope.action = 'edit';
            $scope.ReturnPolicy = angular.copy(response.data[0]);
        }
        else {
            $scope.action = 'add';
            $scope.ReturnPolicy = {};
        }
        $rootScope.ViewLoading = false;
    });



    $scope.save = function () {
        $rootScope.ViewLoading = true;
        debugger;
        //var c= CKEDITOR.instances['TxtNameAr'].getData();
        //var cc= CKEDITOR.instances['TxtNameEn'].getData();
        ReturnPolicyApi.Save($scope.ReturnPolicy).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            $scope.ReturnPolicy = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.ReturnPolicy = angular.copy(response.data);
                            toastr.success($('#HSaveSuccessMessage').val(), 'Success');
                            break;
                    }
                    break;

                default:

            }

            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            $rootScope.ViewLoading = false;
        });
    }


}





