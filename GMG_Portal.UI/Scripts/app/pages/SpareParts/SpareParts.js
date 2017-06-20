controllerProvider.register('SparePartsController', ['$scope', 'SparePartsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', SparePartsController]);
function SparePartsController($scope, SparePartsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    debugger;
    $rootScope.ViewLoading = true;
    $scope.ShowFrmAddUpdate = false;
    $scope.ShowSparePartsTable = true;
    //get ProductCategories
    SparePartsApi.GetAllProductCategories().then(function (response) {
        $scope.ProductCategories = response.data;

        if ($scope.ProductCategories.length>0 ) {
            $scope.selectedProductCategory = $scope.ProductCategories[0];


            //get cities of selected country
            SparePartsApi.GetProductsByProductCategoryId($scope.selectedProductCategory.ID).then(function (response) {
                $scope.Products = response.data;
                if ($scope.Products.length > 0) {
                    $scope.selectedProduct = $scope.Products[0];

                    SparePartsApi.GetAllSparePartsByProductId($scope.selectedProduct.ID).then(function (response) {
                        $scope.SpareParts = response.data;
                    });
                }
            });
        }
        $rootScope.ViewLoading = false;

    });


    //get products of selected product category when selected cactegory changed
    $scope.selectedProductCategoryChanged = function (selectedItem) {
        $scope.Products = null;
        $scope.SpareParts = null;
        if (selectedItem != null) {
            //get products of selected product category 
            $rootScope.ViewLoading = true;
            $scope.selectedProduct = null;
            SparePartsApi.GetProductsByProductCategoryId(selectedItem.ID).then(function (response) {
                debugger;
                $scope.Products = response.data;
                if ($scope.Products.length>0)
                $scope.selectedProduct = $scope.Products[0];
                $rootScope.ViewLoading = false;
            });
        }
        $scope.selectedProductCategory = selectedItem;
    };

    //when selected product changed
    $scope.selectedProductChanged = function (selectedItem) {
        debugger;
        $scope.SpareParts = null;
        if (selectedItem != null) {
            SparePartsApi.GetAllSparePartsByProductId(selectedItem.ID).then(function (response) {
                $scope.SpareParts = response.data;
            });
        }
        $scope.selectedProduct = selectedItem;
        //alert(selectedItem.ID);
    };


    $scope.open = function (SparePart) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $scope.ShowSparePartsTable = false;
        $scope.ShowFrmAddUpdate = true;
        $scope.action = SparePart == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (SparePart == null) SparePart = {};
        $scope.SparePart = angular.copy(SparePart);
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }


    $scope.back = function () {
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowSparePartsTable = true;
    }

    $scope.Restore = function (SparePart) {
        debugger;
        $scope.SparePart = angular.copy(SparePart);
        $scope.SparePart.IsDeleted = false;
        $scope.action = 'restore';
        $scope.save();
    }

    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        if($scope.action != 'restore')
        $scope.SparePart.ProductID = $scope.selectedProduct.ID;
        SparePartsApi.Save($scope.SparePart).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.SpareParts.indexOf($filter('filter')($scope.SpareParts, { 'ID': $scope.SparePart.ID }, true)[0]);
                            $scope.SpareParts[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.SpareParts.indexOf($filter('filter')($scope.SpareParts, { 'ID': $scope.SparePart.ID }, true)[0]);
                            $scope.SpareParts[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.SpareParts.indexOf($filter('filter')($scope.SpareParts, { 'ID': $scope.SparePart.ID }, true)[0]);
                            $scope.SpareParts[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');

                            break;
                        case 'add':
                            $scope.SpareParts.push(angular.copy(response.data));
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
                default:

            }

            $rootScope.ViewLoading = false;
            $scope.back();
        });
    }


    $scope.Delete = function (SparePart) {
        $scope.action = 'delete';
        $scope.SparePart = SparePart;
        $scope.SparePart.IsDeleted = true;
        $scope.save();
    }
}





