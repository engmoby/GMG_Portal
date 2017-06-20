provide.service('ReturnPolicyApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/ReturnPolicy/GetAll');
    }

    this.Save = function (ReturnPolicy) {
        return $http({
            url: ApiURL + '/SystemParameters/ReturnPolicy/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(ReturnPolicy)
        });
    }
});