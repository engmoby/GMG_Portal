provide.service('NewslettersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/Newsletter/GetAllWithDeleted');
    }

    this.Save = function (newsletter) {
        return $http({
            url: apiUrl + '/SystemParameters/Newsletter/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(newsletter)
        });
    }
     

  



});