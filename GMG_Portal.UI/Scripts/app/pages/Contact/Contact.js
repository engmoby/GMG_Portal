controllerProvider.register('ContactController', ['$scope', 'ContactApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ContactController]);
function ContactController($scope, ContactApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    ContactApi.GetAll().then(function (response) {
        $scope.Contacts = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Contact) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Contact == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Contact == null) Contact = {};
        $scope.Contact = angular.copy(Contact);
        if ($scope.Contact.Image)
            $scope.countFiles = $scope.Contact.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (Contact) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Contact == null ? 'add' : 'edit';
        if (Contact == null) Contact = {};
        $scope.Contact = angular.copy(Contact);
        //if ($scope.Contact.Image)
        //    $scope.countFiles = $scope.Contact.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Contact) {
        debugger;
        $scope.Contact = angular.copy(Contact);
        $scope.Contact.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        ContactsApi.Save($scope.Contact).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Contacts.indexOf($filter('filter')($scope.Contacts, { 'ID': $scope.Contact.ID }, true)[0]);
                           // $scope.Contacts[index] = angular.copy(response.data);
                            ContactsApi.GetAll().then(function (response) {
                                $scope.Contacts = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Contacts.indexOf($filter('filter')($scope.Contacts, { 'ID': $scope.Contact.ID }, true)[0]);
                           // $scope.Contacts[index] = angular.copy(response.data);
                            ContactsApi.GetAll().then(function (response) {
                                $scope.Contacts = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            ContactsApi.GetAll().then(function (response) {
                                $scope.Contacts = response.data;
                            });
                            // $scope.Contacts.push(angular.copy(response.data));
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

    $scope.Delete = function (Contact) {
        $scope.action = 'delete';
        $scope.Contact = Contact;
        $scope.Contact.IsDeleted = true;
        $scope.save();
    }
    $scope.uploading = false;
    $scope.countFiles = '';
    $scope.data = []; //For displaying file name on browser
    $scope.formdata = new FormData();
    $scope.getFiles = function (file) {
        angular.forEach(file, function (value, key) {
            $scope.formdata.append(key, value);
            $scope.data.push({ FileName: value.name, FileLength: value.size });
            $scope.Image = value.name;
            // console.log($scope.Image);
        });
        //This line is just show you there is possible to
        //send in extra parameter to server.


        $scope.countFiles = $scope.data.length == 0 ? '' : $scope.data.length + ' files selected';
        $scope.$apply();
        $scope.formdata.append('countFiles', $scope.countFiles);
        // console.log($scope.data);
    };

    $scope.uploadFiles = function () {
        debugger;
        if ($scope.data.length === 0) {
            $scope.save();
            return;
        }
        uploadService.uploadFiles($scope)
            // then() called when uploadFiles gets back
            .then(function (data) {
                // promise fulfilled
                $scope.uploading = false;
                if (data === '') {
                    // console.log(data);
                    //   $scope.Image=data.
                    $scope.save();
                    $scope.data = [];
                    alert("Done!!!");
                    $scope.formdata = new FormData();
                    $scope.data = [];
                    $scope.countFiles = '';
                    $scope.$apply;
                } else {
                    // console.log(data);
                    //Server Error
                    $scope.data = [];
                    alert("Shit, What happended up there!!! " + data);
                }
            }, function (error) {
                $scope.uploading = false;
                $scope.data = [];
                //Server Error
                alert("Shit, What happended up there!!! " + error);
            }

            );
    };

}






