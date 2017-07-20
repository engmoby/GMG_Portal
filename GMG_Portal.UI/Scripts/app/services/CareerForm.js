provide.service('CareerFormsApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/CareerForm/GetAllWithDeleted');
    }

    this.Save = function (careerForm) {
        return $http({
            url: apiUrl + '/SystemParameters/CareerForm/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(careerForm)
        });
    }


    this.Download = function (careerForm) {
        return $http({
            url: apiUrl + '/SystemParameters/CareerForm/download',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            responseType: 'arraybuffer',
            data: JSON.stringify(careerForm)
        });
    }
});