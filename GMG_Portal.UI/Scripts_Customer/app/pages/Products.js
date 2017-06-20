controllerProvider.register('ProductsController', ['$scope', 'ProductsApi', ProductsController]);

function ProductsController($scope, ProductsApi) {
    debugger;
    ProductsApi.GetAllProductsWithMainImage().then(function (response) {
        debugger;
        $scope.Products = response.data;
    });

}
