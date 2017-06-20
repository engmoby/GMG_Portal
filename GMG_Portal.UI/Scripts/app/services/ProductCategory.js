provide.service('ProductCategoryApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;

   this.GetAll = function () {
       return $http.get(ApiURL + '/SystemParameters/ProductCategory/GetAll');
    }

   this.Save = function (ProductCategory) {
        return $http({
            url: ApiURL + '/SystemParameters/ProductCategory/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(ProductCategory)
        });
    }
});