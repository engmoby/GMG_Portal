provide.service('offersApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value;
    var langId = document.querySelector('#HCurrentLang').value;
    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/Offers/GetAllWithDeleted?LangId=' + lang);
    }

    this.Save = function (offer) {
        return $http({
            url: apiUrl + '/SystemParameters/Offers/Save',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(offer)
        });
    }

  this.GetAllCategories = function (lang) {
    return $http.get(apiUrl + '/SystemParameters/Currency/GetAll?LangId=' + lang);
  }

  this.GetAllCurrency = function (lang) {
    return $http.get(apiUrl + '/SystemParameters/Currency/GetAllWithDeleted?LangId=' + lang);
  }

});