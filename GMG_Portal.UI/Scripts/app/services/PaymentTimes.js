provide.service('PaymentTimesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/PaymentTimes/GetAllWithDeleted');
    }

    this.Save = function (PaymentTime) {
        return $http({
            url: ApiURL + '/SystemParameters/PaymentTimes/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(PaymentTime)
        });
    }
});