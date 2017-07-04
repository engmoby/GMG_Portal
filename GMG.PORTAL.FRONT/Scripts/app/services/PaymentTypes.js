provide.service('PaymentTypesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/PaymentTypes/GetAllWithDeleted');
    }

    this.Save = function (PaymentType) {
        return $http({
            url: ApiURL + '/SystemParameters/PaymentTypes/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(PaymentType)
        });
    }
});