controllerProvider.register('HotelsController', ['$scope', 'HotelsApi', 'uploadHotlesService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HotelsController]);
function HotelsController($scope, HotelsApi, uploadHotlesService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;
    $scope.basicInfo = false;
    $scope.imagesList = false;
    $rootScope.ViewLoading = true;
    HotelsApi.GetAll().then(function (response) {
        $scope.Hotels = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Hotel) {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.invalidupdateAddFrm = true;
        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;
        $scope.basicInfo = true;
        $scope.action = Hotel == null ? 'add' : 'edit';

        this.isFrmAddUpdateInvalid = false;
        $scope.isFrmRowformInvalid = false;
        $scope.isSecondFrmRowformInvalid = false;

        if (Hotel == null) Hotel = {};
        else {
            HotelsApi.GetHotelDetails(Hotel.Id).then(function (response) {
                $scope.HotelDetails = response.data;
            });
        }
        $scope.Hotel = angular.copy(Hotel);
        if ($scope.Hotel.Image)
            $scope.countFiles = $scope.Hotel.Image;
        $rootScope.ViewLoading = false;



    }

    //$scope.open = function (Hotel) {
    //    debugger;
    //    $scope.countFiles = '';
    //    $scope.invalidupdateAddFrm = true;
    //    $('#ModelAddUpdate').modal('show');
    //    $scope.action = Hotel == null ? 'add' : 'edit';
    //    $scope.FrmAddUpdate.$setPristine();
    //    $scope.FrmAddUpdate.$setUntouched();
    //    if (Hotel == null) Hotel = {};
    //    $scope.Hotel = angular.copy(Hotel);
    //    if ($scope.Hotel.Image)
    //        $scope.countFiles = $scope.Hotel.Image;
    $scope.back = function () {
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowTableData = true;
    }
    $scope.Restore = function (Hotel) {
        debugger;
        $scope.Hotel = angular.copy(Hotel);
        $scope.Hotel.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Hotel.Image = $scope.Image;
            $scope.Image = "";
        }
        $scope.basicInfo = false;
        $scope.imagesList = true;
        $rootScope.ViewLoading = false;
        //HotelsApi.Save($scope.Hotel).then(function (response) {

        //    switch (response.data.OperationStatus) {
        //        case "Succeded":
        //            var index;
        //            switch ($scope.action) {
        //                case 'edit':
        //                    index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
        //                    // $scope.Hotels[index] = angular.copy(response.data);
        //                    HotelsApi.GetAll().then(function (response) {
        //                        $scope.Hotels = response.data;
        //                    });
        //                    toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
        //                    break;
        //                case 'delete':
        //                    index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
        //                    // $scope.Hotels[index] = angular.copy(response.data);
        //                    HotelsApi.GetAll().then(function (response) {
        //                        $scope.Hotels = response.data;
        //                    });
        //                    toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
        //                    break;
        //                case 'add':
        //                    HotelsApi.GetAll().then(function (response) {
        //                        $scope.Hotels = response.data;
        //                    });
        //                    // $scope.Hotels.push(angular.copy(response.data));
        //                    toastr.success($('#HSaveSuccessMessage').val(), 'Success');
        //                    break;
        //            }
        //            break;
        //        case "NameEnMustBeUnique":
        //            toastr.error($('#HEnglishNameUnique').val(), 'Error');
        //            break;
        //        case "NameArMustBeUnique":
        //            toastr.error($('#HArabicNameUnique').val(), 'Error');
        //            break;
        //        case "HasRelationship":
        //            HotelsApi.GetAll().then(function (response) {
        //                $scope.Hotels = response.data;
        //            });
        //            toastr.error($('#HCannotDeleted').val(), 'Error');

        //            break;
        //        default:

        //    }

        //    $scope.HotelDetails = response.data;
        //    $scope.basicInfo = false;
        //    $scope.imagesList = true;
        //    $rootScope.ViewLoading = false;
        //   // $scope.back();
        //},
        //function (response) {
        //    debugger;
        //    ss = response;
        //});
    }

    $scope.Delete = function (Hotel) {
        $scope.action = 'delete';
        $scope.Hotel = Hotel;
        $scope.Hotel.IsDeleted = true;
        $scope.save();
    }
    //$scope.uploading = false;
    //$scope.countFiles = '';
    //$scope.data = []; //For displaying file name on browser
    //$scope.formdata = new FormData();
    //$scope.getFiles = function (file) {
    //    angular.forEach(file, function (value, key) {
    //        $scope.formdata.append(key, value);
    //        $scope.data.push({ FileName: value.name, FileLength: value.size });
    //        $scope.Image = value.name;
    //        // console.log($scope.Image);
    //    });
    //    //This line is just show you there is possible to
    //    //send in extra parameter to server.

    //    $scope.countFiles = $scope.data.length == 0 ? '' : $scope.data.length + ' files selected';
    //    $scope.$apply();
    //    $scope.formdata.append('countFiles', $scope.countFiles);
    //    // console.log($scope.data);
    //};

    //$scope.uploadFiles = function () {
    //    debugger;
    //    if ($scope.data.length === 0) {
    //        $scope.saveImage();
    //        return;
    //    }
    //    uploadHotlesService.uploadFiles($scope)
    //        // then() called when uploadFiles gets back
    //        .then(function (data) {
    //            // promise fulfilled
    //            $scope.uploading = false;
    //            if (data === '') {
    //                console.log(data);
    //                //   $scope.Image=data.
    //                $scope.saveImage();
    //                alert("Done!!!");
    //                $scope.formdata = new FormData();
    //                $scope.data = [];
    //                $scope.countFiles = '';
    //                $scope.$apply;
    //            } else {
    //                // console.log(data);
    //                //Server Error
    //                $scope.data = [];
    //                alert("Shit, What happended up there!!! " + data);
    //            }
    //        }, function (error) {
    //            $scope.uploading = false;
    //            $scope.data = [];
    //            //Server Error
    //            alert("Shit, What happended up there!!! " + error);
    //        }

    //        );
    //};
    $scope.saveImage = function () {
        $rootScope.ViewLoading = true;

        $rootScope.ViewLoading = false;
        HotelsApi.SaveImage($scope.HotelDetails.ImageList).then(function (response) {
            switch (response.data[0].OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'ID': $scope.HotelDetails.Id }, true)[0]);
                            $scope.HotelDetails[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
                            // $scope.Hotels[index] = angular.copy(response.data);
                            HotelsApi.GetAll().then(function (response) {
                                $scope.Hotels = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'ID': $scope.HotelDetails.Id }, true)[0]);
                            $scope.HotelDetails[index] = angular.copy(response.data);

                            // $scope.Hotels.push(angular.copy(response.data));
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
                    HotelsApi.GetAll().then(function (response) {
                        $scope.Hotels = response.data;
                    });
                    toastr.error($('#HCannotDeleted').val(), 'Error');

                    break;
                default:

            }

            $scope.HotelDetails = response.data;
            $scope.basicInfo = false;
            $scope.imagesList = true;
            $rootScope.ViewLoading = false;
            $scope.backImage();
        },
        function (response) {
            debugger;
            ss = response;
        });
    }
     
    $scope.openUploadImage = function (Hotel) {
        $('#ModelAddUpdateImage').modal('show');
        if (Hotel == null) Hotel = {};
        $scope.Hotel = angular.copy(Hotel);

    }

    $scope.DeleteImage = function (hotelImage) {
        $scope.action = 'delete';
        $scope.HotelDetail = hotelImage;
        $scope.HotelDetail.IsDeleted = true;
        $scope.DeleteImageFunction();
    }
    $scope.RestoreImage = function (hotelImage) {
        debugger;
        $scope.action = 'edit';
        $scope.HotelDetail = angular.copy(hotelImage);
        $scope.HotelDetail.IsDeleted = false;
        $scope.DeleteImageFunction();
    }
    $scope.DeleteImageFunction = function () {
        $rootScope.ViewLoading = true;
        HotelsApi.DeleteImage($scope.HotelDetail).then(function (response) {
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'ID': $scope.HotelDetails.Id }, true)[0]);
                            $scope.HotelDetails[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'ID': $scope.Hotel.ID }, true)[0]);
                            // $scope.Hotels[index] = angular.copy(response.data);
                            HotelsApi.GetAll().then(function (response) {
                                $scope.Hotels = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'ID': $scope.HotelDetails.Id }, true)[0]);
                            $scope.HotelDetails[index] = angular.copy(response.data);

                            // $scope.Hotels.push(angular.copy(response.data));
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
                    HotelsApi.GetAll().then(function (response) {
                        $scope.Hotels = response.data;
                    });
                    toastr.error($('#HCannotDeleted').val(), 'Error');

                    break;
                default:

            }

            $scope.HotelDetails = response.data;
            $rootScope.ViewLoading = false;
            $scope.backImage();
        },
            function (response) {
                debugger;
                ss = response;
            });
    }



    $scope.openImage = function (Hotel) {
        debugger;
        $('#ModelImage').modal('show');
        if (Hotel == null) Hotel = {};
        $scope.Hotel = angular.copy(Hotel);
        //if ($scope.Hotel.Image)
        //    $scope.countFiles = $scope.Hotel.Image;

    }
    $scope.backImage = function () {
        $('#ModelAddUpdateImage').modal('hide');
    }


    // GET THE FILE INFORMATION.
    $scope.getFileDetails = function (e) {

        $scope.files = [];
        $scope.$apply(function () {

            // STORE THE FILE OBJECT IN AN ARRAY.
            for (var i = 0; i < e.files.length; i++) {
                $scope.files.push(e.files[i]);
            }

        });
    };

    // NOW UPLOAD THE FILES.
    $scope.uploadFiles = function () {

        //FILL FormData WITH FILE DETAILS.
        var data = new FormData();
        debugger;
        for (var i in $scope.files) {
            data.append("uploadedFile", $scope.files[i]);
            $scope.HotelDetails.ImageList.push(
                {
                    "Id": 0,
                    "Hotel_Id": $scope.HotelDetails.ImageList[0].Hotel_Id,
                    "Image": $scope.files[i].name
                }
            );

        }

        // ADD LISTENERS.
        var objXhr = new XMLHttpRequest();
        objXhr.addEventListener("progress", updateProgress, false);
        objXhr.addEventListener("load", transferComplete, false);

        // SEND FILE DETAILS TO THE API.
        objXhr.open("POST", "/api/uploadHotles/");
        objXhr.send(data);
    }

    // UPDATE PROGRESS BAR.
    function updateProgress(e) {
        if (e.lengthComputable) {
            document.getElementById('pro').setAttribute('value', e.loaded);
            document.getElementById('pro').setAttribute('max', e.total);
        }
    }

    // CONFIRMATION.
    function transferComplete(e) { 
        $scope.saveImage();
        alert("Files uploaded successfully.");
    }

}






