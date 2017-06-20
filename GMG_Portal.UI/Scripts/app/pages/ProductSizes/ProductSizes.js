controllerProvider.register('ProducrSizesController', ['$scope', 'ProductSizesApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ProducrSizesController]);


function ProducrSizesController($scope, ProductSizesApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $rootScope.ViewLoading = true;
    ProductSizesApi.GetAll().then(function (response) {
        $scope.ProductSizes= response.data;
        $rootScope.ViewLoading = false;
    });

    $scope.open = function (ProductSize) {
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = ProductSize == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (ProductSize == null) ProductSize = { 'NameAr': '', 'NameEn': '' };
        $scope.ProductSize = angular.copy(ProductSize);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (ProductSize) {
        debugger;
        $scope.ProductSize = angular.copy(ProductSize);
        $scope.ProductSize.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }
 
    $scope.save = function () {
        $rootScope.ViewLoading = true;
        ProductSizesApi.Save($scope.ProductSize).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.ProductSizes.indexOf($filter('filter')($scope.ProductSizes, { 'ID': $scope.ProductSize.ID }, true)[0]);
                            $scope.ProductSizes[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.ProductSizes.indexOf($filter('filter')($scope.ProductSizes, { 'ID': $scope.ProductSize.ID }, true)[0]);
                            $scope.ProductSizes[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.ProductSizes.push(angular.copy(response.data));
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
        function (response)
        {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (ProductSize) {
        $scope.action = 'delete';
        $scope.ProductSize = ProductSize;
        $scope.ProductSize.IsDeleted = true;
        $scope.save();
    }

    

}

