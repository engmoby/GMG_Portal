provide.service('HotelsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    debugger;

    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Hotels/GetAllWithDeleted');
    }
    this.GetHotelDetails = function (hotelId) {
        return $http.get(apiUrl + '/SystemParameters/Hotels/GetHotelDetails?Id=' + hotelId);
    }
    this.Save = function (hotel) {
        return $http({
            url: apiUrl + '/SystemParameters/Hotels/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(hotel)
        });
    }

    this.SaveImage = function (hotelImages) {
        return $http({
            url: apiUrl + '/SystemParameters/Hotels/SaveImages',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(hotelImages)
        });
    }

    this.DeleteImage = function (hotelImage) {
        return $http({
            url: apiUrl + '/SystemParameters/Hotels/DeleteImage',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(hotelImage)
        });
    }
});