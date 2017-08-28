provide.service('HomePageApi', function ($http) {
    var apiUrl = document.querySelector('#HServicesURL').value; 

    this.GetAll = function (lang) {
        return $http.get(apiUrl + '/SystemParameters/HomePage/GetAllWithDeleted?LangId=' + lang);
    }
     
});