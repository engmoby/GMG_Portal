provide.service('CustomerPointsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/CustomerPoints/GetAllWithDeleted');
    }

    this.GetAllInvoiceTypes = function () {
        return $http.get(ApiURL + '/SystemParameters/InvoiceTypes/GetAll');
    }

    this.GetAllPaymentTypes = function () {
        return $http.get(ApiURL + '/SystemParameters/PaymentTypes/GetAll');
    }

    this.GetAllPaymentTimes = function () {
        return $http.get(ApiURL + '/SystemParameters/PaymentTimes/GetAll');
    }

    this.Save = function (CustomerPoint) {
        return $http({
            url: ApiURL + '/SystemParameters/CustomerPoints/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(CustomerPoint)
        });
    }

   
});