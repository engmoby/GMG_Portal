provide.service('SparePartsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;

    this.GetAllProductCategories = function () {
        return $http.get(ApiURL + '/SystemParameters/ProductCategory/GetAllLeafsRelatedWithProducts');
    }

    this.GetProductsByProductCategoryId = function (ProductCategoryId) {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAll?ProductCategoryID=' + ProductCategoryId);
    }

    this.GetAllSparePartsByProductId = function (ProductId) {
        return $http.get(ApiURL + '/SystemParameters/SpareParts/GetAll?ProductId=' + ProductId);
    }

    this.Save = function (SparePart) {
        return $http({
            url: ApiURL + '/SystemParameters/SpareParts/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(SparePart)
        });
    }
});