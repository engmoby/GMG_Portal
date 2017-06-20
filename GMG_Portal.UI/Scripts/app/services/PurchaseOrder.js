provide.service('PurchaseOrderApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/Customer/PurchaseOrder/GetAll');
    }

    
    this.GetAllProducts = function () {
        return $http.get(ApiURL + '/SystemParameters/Products/GetAll');
    }
    this.GetAllOrderStatus = function () {
        return $http.get(ApiURL + '/SystemParameters/OrderStatuses/GetAll');
    }
    
    this.SavePurchaseOrderDetails = function (PurchaseOrderDetail) {
        return $http({
            url: ApiURL + '/Customer/PurchaseOrderDetails/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(PurchaseOrderDetail)
        });
    }
    this.Save = function (PurchaseOrder) {
        return $http({
            url: ApiURL + '/Customer/PurchaseOrder/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(PurchaseOrder)
        });
    }


});