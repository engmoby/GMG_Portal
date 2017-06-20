provide.service('ProductSizesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductSizes/GetAllWithDeleted');
    }

    this.Save = function (ProductSize) {
        return $http({
            url: ApiURL + '/SystemParameters/ProductSizes/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(ProductSize)
        });
    }
});