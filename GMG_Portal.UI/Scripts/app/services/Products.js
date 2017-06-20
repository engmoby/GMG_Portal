provide.service('ProductsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAllWithDeleted');
    }


    this.GetAllBrands = function () {
        return $http.get(ApiURL + '/SystemParameters/Brands/GetAll');
    }

    this.GetAllPoductSizes = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductSizes/GetAll');
    }
    this.GetProductImages = function (ProductID) {
        return $http.get(ApiURL + '/SystemParameters/Products/GetProductImages?ProductID=' + ProductID);
    }

    this.GetAllProductCategoriesLeafs = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductCategory/GetAllLeafs');
    }

    this.Save = function (Products) {
        return $http({
            url: ApiURL + '/SystemParameters/Products/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Products)
        });
    }
 

});