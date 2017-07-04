provide.service('BrandsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Brands/GetAllWithDeleted');
    }

    this.Save = function (Brand) {
        return $http({
            url: ApiURL + '/SystemParameters/Brandas/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Brand)
        });
    }
});