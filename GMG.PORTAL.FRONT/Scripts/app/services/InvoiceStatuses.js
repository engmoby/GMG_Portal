provide.service('InvoiceStatusesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/InvoiceStatuses/GetAll');
    }

    this.Save = function (InvoiceStatus) {
        return $http({
            url: ApiURL + '/SystemParameters/InvoiceStatuses/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(InvoiceStatus)
        });
    }
});