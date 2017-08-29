provide.service('HomePageApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 

    this.GetAllAppliedCarrer = function () {
        return $http.get(apiUrl + '/SystemParameters/General/GetAllAppliedCarrer');
    }
    this.GetAllContactInquiry = function () {
        return $http.get(apiUrl + '/SystemParameters/General/GetAllContactInquiry');
    }
    this.GetAllHotelReservation = function () {
        return $http.get(apiUrl + '/SystemParameters/General/GetAllHotelReservation');
    }

});