angular.module("Login", []);
angular.module("Login")
    .controller("LoginController", LoginController);

function LoginController($scope,$http) {
     
    $scope.userName="";
    $scope.passWord = "";

    $scope.Login = function () {
        debugger;
        this.signIn($scope.userName, $scope.passWord).then(function (response) {
            alert(response);
        });
    }


    this.signIn = function (userName,Password) {
        return $http({
            url: 'http://localhost:2798/SystemParameters/Login',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(AccountType)
        });
    }



}