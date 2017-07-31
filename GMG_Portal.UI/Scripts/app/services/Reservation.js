provide.service('ReservationApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Reservation/GetAllWithDeleted');
    }

    this.Save = function (reservation) {
        return $http({
            url: apiUrl + '/SystemParameters/Reservation/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(reservation)
        });
    }


    this.Download = function (reservation) {
        return $http({
            url: apiUrl + '/SystemParameters/Reservation/download',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            responseType: 'arraybuffer',
            data: JSON.stringify(reservation)
        });
    }
});