controllerProvider.register('ProductsController', ['$scope', 'ProductsApi', '$rootScope', '$timeout', '$uibModal', '$filter', 'fileReader', 'toastr', '$compile', ProductsController]);


function ProductsController($scope, ProductsApi, $rootScope, $timeout, $uibModal, $filter, fileReader, toastr, $compile) {
    debugger;
    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;
    $rootScope.ViewLoading = true;
    var servicesBackCount = 1;
    var stopLoading = function () {
        servicesBackCount++;
        if (servicesBackCount > 4)
            $rootScope.ViewLoading = false;
    }

    ProductsApi.GetAll().then(function (response) {
        $scope.Products = response.data;
        stopLoading();
    });

    ProductsApi.GetAllBrands().then(function (response) {
        $scope.Brands = response.data;
        stopLoading();
    });

    ProductsApi.GetAllPoductSizes().then(function (response) {
        $scope.PoductSizes = response.data;
        stopLoading();
    });

    ProductsApi.GetAllProductCategoriesLeafs().then(function (response) {
        $scope.ProductCategories = response.data;
        stopLoading();
    });

    //$scope.uploader = {};
    //$scope.upload = function () {
    //    $scope.uploader.flow.upload();

    //}

    $scope.RemoveAllImages = null;
    $scope.open = function (Product) {
        debugger;
        $('custom-Uploader').each(function () {
            var el = angular.element($(this));
            $compile(el)($scope);
        });
        $scope.invalidupdateAddFrm = true;
        $scope.action = Product == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Product == null) {
            Product = { ProductImages: [], MainProductImage :""};
            $scope.SelectedProductCategory = null;
            $scope.SelectedProductSize = null;
            $scope.SelectedBrand = null;
            //here
            $scope.SliderStartIndex = 0;
            $scope.SliderEndIndex = 3;
            //there

            $scope.Product = angular.copy(Product);

        }
        else {
            debugger;
            var ProductCategoryindex = $scope.ProductCategories.indexOf($filter('filter')($scope.ProductCategories, { 'ID': Product.ProductCategoryID }, true)[0]);
            $scope.SelectedProductCategory = $scope.ProductCategories[ProductCategoryindex];

            var PoductSizeindex = $scope.PoductSizes.indexOf($filter('filter')($scope.PoductSizes, { 'ID': Product.ProductSizeID }, true)[0]);
            $scope.SelectedProductSize = $scope.PoductSizes[PoductSizeindex];

            var Brandindex = $scope.Brands.indexOf($filter('filter')($scope.Brands, { 'ID': Product.BrandID }, true)[0]);
            $scope.SelectedBrand = $scope.Brands[Brandindex];

            ProductsApi.GetProductImages(Product.ID).then(function (response) {
                $rootScope.ViewLoading = true;
                Product.ProductImages = response.data;
                if (Product.ProductImages && Product.ProductImages.length>0)
                    Product.MainProductImage = Product.ProductImages[0];
                $scope.Product = angular.copy(Product);
                $rootScope.ViewLoading = false;
            });
        }


        $timeout(function () {
            document.querySelector('select[name="LstProductCategory"]').focus();
        }, 500);

        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;
        
    }

   

    //$scope.$watch('SelectedBrand', function handleFooChange(newVal, oldVal) {
        
    //},true);
    $scope.selectedBrandChanged = function (SelectedBrand) {
        debugger;
        $scope.SelectedBrand = SelectedBrand;
    }
    $scope.selectedProductCategoriesChanged = function (SelectedProductCategory) {
        debugger;
        $scope.SelectedProductCategory = SelectedProductCategory;
    }
    $scope.SelectedProductSizeChanged = function (SelectedProductSize) {
        debugger;
        $scope.SelectedProductSize = SelectedProductSize;
    }


    $scope.Restore = function (Product) {
        debugger;
        $scope.Product = angular.copy(Product);
        $scope.Product.IsDeleted = false;
        $scope.action = 'restore';
        $scope.save();
    }

        $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        debugger;
        var c = $scope.Product;
        if ($scope.action != 'delete'&&$scope.action != 'restore') {
            $scope.Product.ProductCategoryID = $scope.SelectedProductCategory.ID;
            $scope.Product.BrandID = $scope.SelectedBrand.ID;
            $scope.Product.ProductSizeID = $scope.SelectedProductSize.ID;
        }
        ProductsApi.Save($scope.Product).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {
              
                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Products.indexOf($filter('filter')($scope.Products, { 'ID': $scope.Product.ID }, true)[0]);
                            $scope.Products[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Products.indexOf($filter('filter')($scope.Products, { 'ID': $scope.Product.ID }, true)[0]);
                            $scope.Products[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.Products.indexOf($filter('filter')($scope.Products, { 'ID': $scope.Product.ID }, true)[0]);
                            $scope.Products[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Products.push(angular.copy(response.data));
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
            $rootScope.ViewLoading = false;
            ss = response;
        });
    }
    

        $scope.back = function () {
            $scope.RemoveAllImages = false;
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowTableData = true;
    }

    $scope.Delete = function (Product) {
        debugger;
        $scope.action = 'delete';
        $scope.Product = Product;
        $scope.Product.IsDeleted = true;
        $scope.save();
    }
  
    $scope.getFile = function () {
        debugger;
        fileReader.readAsDataUrl($scope.file, $scope)
            .then(function (result) {
                $scope.picture = result;
            });
    };


    $scope.removePicture = function () {
        debugger;
        $scope.picture = $filter('appImage')('theme/no-photo.png');
        $scope.noPicture = true;
    };

    //start uploader 2
   
    $scope.processFiles = function (files) {
        debugger;
        angular.forEach(files, function (flowFile, i) {
            var fileReader = new FileReader();
            fileReader.onload = function (event) {
                var uri = event.target.result;
                $scope.Product.ProductImages.push(uri);
                $scope.$apply();
            };
            fileReader.readAsDataURL(flowFile.file);
        });
    };

    $scope.DeleteImage = function ($index) {
        debugger;
        $scope.Product.ProductImages.splice($index, 1);
        if ($index == $scope.Product.ProductImages.length )
            $scope.pre();
    }
   
    $scope.Change = function (UnitNo)
    {
        debugger;
        alert(UnitNo);
    }

    $scope.next = function ()
    {
        if ($scope.SliderEndIndex < $scope.Product.ProductImages.length) {
            $scope.SliderStartIndex++;
            $scope.SliderEndIndex++;
        }
    }

    $scope.pre = function () {
        if ($scope.SliderStartIndex > 0) {
            $scope.SliderStartIndex--;
            $scope.SliderEndIndex--;
        }
    }

    //end uploader 2
   
}

