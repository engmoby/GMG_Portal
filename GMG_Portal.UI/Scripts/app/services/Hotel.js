provide.service('HotelsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 

    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Hotels/GetAllWithDeleted?LangId=' + lang);
    }
    this.GetHotelDetails = function (hotelId, langId) {
        return $http.get(apiUrl + '/SystemParameters/Hotels/GetHotelDetailsWith?Id=' + hotelId + '&langId?=' + langId);
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
    this.GetAllFeatures = function () {
        return $http.get(apiUrl + '/SystemParameters/Features/GetAll');
    }
    this.SaveFeature = function (hotelFeatures) {
        return $http({
            url: apiUrl + '/SystemParameters/Hotels/SaveFeatures',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(hotelFeatures)
        });
    }


});