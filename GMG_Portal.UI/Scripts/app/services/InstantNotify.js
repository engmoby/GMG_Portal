provide.service('NotifyApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 
   
    this.GetAll = function () {
        return $http.get(apiUrl + '/SystemParameters/NotifyController/GetAllWithDeleted');
    }

    this.Save = function (New) {
        return $http({
            url: apiUrl + '/SystemParameters/NotifyController/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(New)
        });
    }

    this.GetAllCategories = function () {
        return $http.get(apiUrl + '/SystemParameters/DepartmentNotifyController/GetAllWithDeleted');
    }

   
});