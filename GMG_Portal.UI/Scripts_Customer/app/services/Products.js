provide.service('ProductsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAllProductsWithMainImage = function () {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAllWithMainImage');
    }


});