provide.service('ContactFormApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/ContactForm/GetAllWithDeleted');
    }

    this.Save = function (contact) {
        return $http({
            url: apiUrl + '/SystemParameters/ContactForm/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(contact)
        });
    }


    this.Download = function (contact) {
        return $http({
            url: apiUrl + '/SystemParameters/ContactForm/download',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            responseType: 'arraybuffer',
            data: JSON.stringify(contact)
        });
    }
});