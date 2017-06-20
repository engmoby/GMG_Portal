controllerProvider.register('SearchCategoryController', ['$scope', 'SearchCategoryApi', SearchCategoryController]);

function SearchCategoryController($scope, SearchCategoryApi) {
    debugger;
   

    SearchCategoryApi.GetAllProductCategories().then(function (response) {
        $scope.ProductCategories = response.data;
    });

    SearchCategoryApi.GetAllBrands().then(function (response) {
        $scope.Brands = response.data;
    });

    $scope.SelectedCategory = [];
    $scope.SelectedBrands = [];
    $scope.test = function ()
    {
        debugger;
        var c = $scope.SelectedCategory;
        var c = $scope.SelectedBrands;

    }
    $scope.filter = function ()
    {
        debugger;
        SearchCategoryApi.GetAllProductsWithMainImage($scope.SelectedBrands, $scope.SelectedCategory).then(function (response) {
            $scope.Products = response.data;
        });
    }
   
}
