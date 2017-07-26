controllerProvider.register('NewslettersController', ['$scope', 'NewslettersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', NewslettersController]);
function NewslettersController($scope, NewslettersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    NewslettersApi.GetAll().then(function (response) {
        debugger;
        $scope.Newsletters = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Newsletter) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Newsletter == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Newsletter == null) Newsletter = {};
        $scope.Newsletter = angular.copy(Newsletter);
        if ($scope.Newsletter.Image)
            $scope.countFiles = $scope.Newsletter.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.downloadFile = function (name) {
        NewslettersApi.Download(name).then(function (data, status, headers) {
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
    $scope.openImage = function (Newsletter) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Newsletter == null ? 'add' : 'edit';
        if (Newsletter == null) Newsletter = {};
        $scope.Newsletter = angular.copy(Newsletter);
        //if ($scope.Newsletter.Image)
        //    $scope.countFiles = $scope.Newsletter.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }  

    $scope.Seen = function (newsletter) {
        debugger;
        $scope.Newsletter = angular.copy(newsletter);
        if (newsletter.Seen === true) {
        $scope.Newsletter.Seen = false;
            
        } else {
            
            $scope.Newsletter.Seen = true;
        }
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        NewslettersApi.Save($scope.Newsletter).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Newsletters.indexOf($filter('filter')($scope.Newsletters, { 'ID': $scope.Newsletter.ID }, true)[0]);
                           // $scope.Newsletters[index] = angular.copy(response.data);
                            NewslettersApi.GetAll().then(function (response) {
                                $scope.Newsletters = response.data; 
                            }); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Newsletters.indexOf($filter('filter')($scope.Newsletters, { 'ID': $scope.Newsletter.ID }, true)[0]);
                           // $scope.Newsletters[index] = angular.copy(response.data);
                            NewslettersApi.GetAll().then(function (response) {
                                $scope.Newsletters = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            NewslettersApi.GetAll().then(function (response) {
                                $scope.Newsletters = response.data;
                            });
                            // $scope.Newsletters.push(angular.copy(response.data));
                            toastr.success($('#HSaveSuccessMessage').val(), 'Success');
                            break;
                    }
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

    $scope.Delete = function (Newsletter) {
        $scope.action = 'delete';
        $scope.Newsletter = Newsletter;
        $scope.Newsletter.IsDeleted = true;
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






