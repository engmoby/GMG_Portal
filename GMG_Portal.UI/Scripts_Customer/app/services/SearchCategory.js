provide.service('SearchCategoryApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;

   

    this.GetAllProductsWithMainImage = function (Brands, ProductCategories) {
        var data = { 'Brands': Brands, 'ProductCategories': ProductCategories };
        return $http({
            url: ApiURL + '/SystemParameters/Products/GetAllWithMainImage',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data)
        });
    }

    this.GetAllProductCategories = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductCategory/GetAllRelatedWithProduct');
    }
    this.GetAllBrands = function () {
        return $http.get(ApiURL + '/SystemParameters/Brands/GetAllRelatedWithProduct');
    }

});