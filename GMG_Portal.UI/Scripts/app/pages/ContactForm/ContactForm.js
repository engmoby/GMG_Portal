controllerProvider.register('ContactFormController', ['$scope', 'ContactFormApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ContactFormController]);
function ContactFormController($scope, ContactFormApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    ContactFormApi.GetAll().then(function (response) {
        debugger;
        $scope.ContactForm = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (contact) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = contact == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (contact == null) contact = {};
        $scope.contact = angular.copy(contact);
        if ($scope.contact.Image)
            $scope.countFiles = $scope.contact.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.downloadFile = function (name) {
        ContactFormApi.Download(name).then(function (data, status, headers) {
            headers = headers();

                var filename = headers['x-filename'];
                var contentType = headers['content-type'];

                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    console.log(ex);
                }
                $rootScope.ViewLoading = false;
                $scope.back();
            },
            function (data) {
                debugger; 
            });
         
    };
    $scope.openImage = function (contact) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = CareerForm == null ? 'add' : 'edit';
        if (contact == null) contact = {};
        $scope.contact = angular.copy(contact);
        //if ($scope.contact.Image)
        //    $scope.countFiles = $scope.contact.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
    $scope.downloadFileImage = function (downloadPath) {
        window.open(downloadPath.Attach, '_blank', '');
    }
    $scope.Restore = function (contact) {
        debugger;
        $scope.contact = angular.copy(contact);
        $scope.contact.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.Seen = function (contact) {
        debugger;
        $scope.contact = angular.copy(contact);
        if (contact.Seen === true) {
            $scope.contact.Seen = false;
            
        } else {
            
            $scope.contact.Seen = true;
        }
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.contact.Image = $scope.Image;
            $scope.Image = "";
        }
        //  uploadService.uploadFiles();
        debugger;
        ContactFormApi.Save($scope.contact).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.ContactForm.indexOf($filter('filter')($scope.ContactForm, { 'ID': $scope.ContactForm.ID }, true)[0]);
                           // $scope.contact[index] = angular.copy(response.data);
                            ContactFormApi.GetAll().then(function (response) {
                                $scope.ContactForm = response.data;
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.ContactForm.indexOf($filter('filter')($scope.ContactForm, { 'ID': $scope.ContactForm.ID }, true)[0]);
                           // $scope.contact[index] = angular.copy(response.data);
                            ContactFormApi.GetAll().then(function (response) {
                                $scope.ContactForm = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            ContactFormApi.GetAll().then(function (response) {
                                $scope.ContactForm = response.data;
                            });
                            // $scope.contact.push(angular.copy(response.data));
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

            $rootScope.ViewLoading = false;
            $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (contact) {
        $scope.action = 'delete';
        $scope.contact = contact;
        $scope.contact.IsDeleted = true;
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






