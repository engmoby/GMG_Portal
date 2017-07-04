provide.service('DepartmentsApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/Departments/GetAllWithDeleted');
    }

    this.Save = function (AccountType) {        
        return $http({
            url: ApiURL + '/SystemParameters/Departments/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(AccountType)
        });
    }
});