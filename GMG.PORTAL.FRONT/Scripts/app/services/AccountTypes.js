provide.service('AccountTypesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/AccountTypes/GetAllWithDeleted');
    }


    this.Save = function (AccountType) {        
        return $http({
            url: ApiURL + '/SystemParameters/AccountTypes/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(AccountType)
        });
    }
});