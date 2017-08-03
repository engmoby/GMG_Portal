provide.service('offersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Offers/GetAllWithDeleted');
    }

    this.Save = function (offer) {
        return $http({
            url: apiUrl + '/SystemParameters/Offers/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(offer)
        });
    }

    this.GetAllCategories = function () {
        return $http.get(apiUrl + '/SystemParameters/NewsCategory/GetAllWithDeleted');
    }
});