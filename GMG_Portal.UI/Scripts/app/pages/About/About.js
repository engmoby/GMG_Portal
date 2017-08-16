controllerProvider.register('AboutController', ['$scope', 'AboutApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', AboutController]);

function AboutController($scope, AboutApi, $rootScope, $timeout, $filter, $uibModal, toastr) {

    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;

        debugger;

        AboutApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.About = response.data;
            $rootScope.ViewLoading = false;
        });
    });


        $scope.letterLimit = 20;
        $rootScope.ViewLoading = true;
        AboutApi.GetAll(CurrentLanguage).then(function(response) {
            $scope.About = response.data;
            $rootScope.ViewLoading = false;
        });
        $scope.open = function(about) {
            debugger;
            $scope.invalidupdateAddFrm = true;
            $('#ModelAddUpdate').modal('show');
            $scope.action = about == null ? 'add' : 'edit';
            $scope.FrmAddUpdate.$setPristine();
            $scope.FrmAddUpdate.$setUntouched();
            if (about == null) about = { 'NameAr': '', 'NameEn': '' };
            $scope.About = angular.copy(about);
            $timeout(function() {
                    document.querySelector('input[name="TxtNameAr"]').focus();
                },
                1000);
        }

        $scope.back = function() {
            $('#ModelAddUpdate').modal('hide');
        }

        $scope.Restore = function(about) {
            debugger;
            $scope.About = angular.copy(about);
            $scope.About.IsDeleted = false;
            $scope.action = 'edit';
            $scope.save();
        }

        $scope.save = function() {
            debugger;
            $rootScope.ViewLoading = true;
            $scope.About.LangId = CurrentLanguage;

            AboutApi.Save($scope.About).then(function(response) {

                    switch (response.data.OperationStatus) {
                    case "Succeded":
                        var index;
                        switch ($scope.action) {
                        case 'edit':
                            index = $scope.About.indexOf(
                                $filter('filter')($scope.About, { 'ID': $scope.About.ID }, true)[0]);
                            $scope.About[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.About.indexOf(
                                $filter('filter')($scope.About, { 'ID': $scope.About.ID }, true)[0]);
                            $scope.About[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.About.push(angular.copy(response.data));
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
                function(response) {
                    debugger;
                    ss = response;
                });
        }

        $scope.Delete = function(About) {
            $scope.action = 'delete';
            $scope.About = About;
            $scope.About.IsDeleted = true;
            $scope.save();
        }

        $scope.setFiles = function(element) {
            $scope.$apply(function($scope) {
                console.log('files:', element.files);
                // Turn the FileList object into an Array
                $scope.files = []
                for (var i = 0; i < element.files.length; i++) {
                    $scope.files.push(element.files[i])
                }
                $scope.progressVisible = false
            });
        };

        $scope.uploadFile = function() {
            var fd = new FormData()
            for (var i in $scope.files) {
                fd.append("uploadedFile", $scope.files[i])
            }
            var xhr = new XMLHttpRequest()
            xhr.upload.addEventListener("progress", uploadProgress, false)
            xhr.addEventListener("load", uploadComplete, false)
            xhr.addEventListener("error", uploadFailed, false)
            xhr.addEventListener("abort", uploadCanceled, false)
            xhr.open("POST", "/fileupload")
            $scope.progressVisible = true
            xhr.send(fd)
        }

        function uploadProgress(evt) {
            $scope.$apply(function() {
                if (evt.lengthComputable) {
                    $scope.progress = Math.round(evt.loaded * 100 / evt.total)
                } else {
                    $scope.progress = 'unable to compute'
                }
            })
        }

        function uploadComplete(evt) {
            /* This event is raised when the server send back a response */
            alert(evt.target.responseText)
        }

        function uploadFailed(evt) {
            alert("There was an error attempting to upload the file.")
        }

        function uploadCanceled(evt) {
            $scope.$apply(function () {
                $scope.progressVisible = false
            })
            alert("The upload has been canceled by the user or the browser dropped the connection.")
        }

    }
  



  
