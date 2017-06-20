controllerProvider.register('BrandsController', ['$scope', 'BrandsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', BrandsController]);
function BrandsController($scope, BrandsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $rootScope.ViewLoading = true;
    BrandsApi.GetAll().then(function (response) {
        $scope.Brands = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Brand) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Brand == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Brand == null) Brand = { 'NameAr': '', 'NameEn': '' };
        $scope.Brand = angular.copy(Brand);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Brand) {
        debugger;
        $scope.Brand = angular.copy(Brand);
        $scope.Brand.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }


    $scope.save = function () {
        $rootScope.ViewLoading = true;
        BrandsApi.Save($scope.Brand).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Brands.indexOf($filter('filter')($scope.Brands, { 'ID': $scope.Brand.ID }, true)[0]);
                            $scope.Brands[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Brands.indexOf($filter('filter')($scope.Brands, { 'ID': $scope.Brand.ID }, true)[0]);
                            $scope.Brands[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Brands.push(angular.copy(response.data));
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

    $scope.Delete = function (Brand) {
        $scope.action = 'delete';
        $scope.Brand = Brand;
        $scope.Brand.IsDeleted = true;
        $scope.save();
    }

}





