controllerProvider.register('ContactController', ['$scope','appCONSTANTS', 'ContactApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ContactController]);
function ContactController($scope,appCONSTANTS, ContactApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.language = appCONSTANTS.supportedLanguage;
    var langId = document.querySelector('#HCurrentLang').value;
    $scope.CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        $scope.CurrentLanguage = selectedValue;

        debugger;

        ContactApi.GetAll($scope.CurrentLanguage).then(function (response) {
            $scope.Contacts = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    ContactApi.GetAll($scope.CurrentLanguage).then(function (response) {
        $scope.Contacts = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Contact) {
        debugger;
        $scope.countFiles = '';
        $scope.invalIdupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Contact == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Contact == null) Contact = {};
        $scope.Contact = angular.copy(Contact); 
         
    } 
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
     

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        $scope.Contact.langId = $scope.CurrentLanguage;

        ContactApi.Save($scope.Contact).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Contacts.indexOf($filter('filter')($scope.Contacts, { 'Id': $scope.Contact.Id }, true)[0]);
                             $scope.Contacts[index] = angular.copy(response.data);
                            //ContactApi.GetAll().then(function (response) {
                            //    $scope.Contacts = response.data; 
                            //}); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Contacts.indexOf($filter('filter')($scope.Contacts, { 'Id': $scope.Contact.Id }, true)[0]);
                            $scope.Contacts[index] = angular.copy(response.data);
                            //ContactApi.GetAll().then(function (response) {
                            //    $scope.Contacts = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //ContactApi.GetAll().then(function (response) {
                            //    $scope.Contacts = response.data;
                            //});
                              $scope.Contacts.push(angular.copy(response.data));
                            toastr.success($('#HSaveSuccessMessage').val(), 'Success');
                            break;
                    }
                    break;
                case "NameEnMustBeUnique":
                    toastr.error($('#HEnglishNameUnique').val(), 'Error');
                    break;
                case "NameArMustBeUnique":
                    toastr.error($('#HArabicNameUnique').val(), 'Error');
                    break;
                case "HasRelationship":
                    toastr.error($('#HCannotDeleted').val(), 'Error');
                    break;
                default:

            }

           
            $scope.back();
            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

   
}






