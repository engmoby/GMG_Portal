provide.service('OrderStatusesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/OrderStatuses/GetAll');
    }

    this.Save = function (OrderStatus) {
        return $http({
            url: ApiURL + '/SystemParameters/OrderStatuses/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(OrderStatus)
        });   
    }
});