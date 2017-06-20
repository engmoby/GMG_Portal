provide.service('ComplaintTypesApi', function ($http) {
    var ApiURL = document.querySelector('#HServicesURL').value;
    this.GetAll = function () {
        return $http.get(ApiURL + '/SystemParameters/ComplaintTypes/GetAllWithDeleted');
    }

    this.Save = function (ComplaintType) {
        return $http({
            url: ApiURL + '/SystemParameters/ComplaintTypes/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(ComplaintType)
        });
    }
});