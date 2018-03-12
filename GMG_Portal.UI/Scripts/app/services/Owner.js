provide.service('OwnersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Owners/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (owner) {
        return $http({
            url: apiUrl + '/SystemParameters/Owners/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(owner)
        });
    }
    this.SaveOrder = function (orderInts) {
        return $http({
            url: apiUrl + '/SystemParameters/Owners/OrderOwner',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(orderInts)
        });
    }
});