controllerProvider.register('HotelsController', ['$scope', 'HotelsApi', 'uploadHotlesService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', HotelsController]);
function HotelsController($scope, HotelsApi, uploadHotlesService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024
    var validFileExtensions = ["image/jpg", "image/jpeg", "image/bmp", "image/gif", "image/png"];

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
        $scope.imagesList = false;
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

    $scope.saveExist = function () {
        $rootScope.ViewLoading = true;
        debugger;

        HotelsApi.Save($scope.Hotel).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotels.indexOf(
                                $filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotels.indexOf(
                                $filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            $scope.Hotels.push(angular.copy(response.data));
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
            $rootScope.ViewLoading = false;
            $scope.back();
        },
            function (response) {
                debugger;
                ss = response;
            });

    }
    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if (!$scope.FrmAddUpdate.$dirty) {
            $scope.basicInfo = false;
            $scope.imagesList = true;
            $rootScope.ViewLoading = false;

        } else {
            HotelsApi.Save($scope.Hotel).then(function (response) {

                switch (response.data.OperationStatus) {
                    case "Succeded":
                        var index;
                        switch ($scope.action) {
                            case 'edit':
                                index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                                $scope.Hotels[index] = angular.copy(response.data);
                                //HotelsApi.GetAll().then(function (response) {
                                //    $scope.Hotels = response.data;
                                //});
                                toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                                break;
                            case 'delete':
                                index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                                $scope.Hotels[index] = angular.copy(response.data);
                                //HotelsApi.GetAll().then(function (response) {
                                //    $scope.Hotels = response.data;
                                //});
                                toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                                break;
                            case 'add':
                                //HotelsApi.GetAll().then(function (response) {
                                //    $scope.Hotels = response.data;
                                //});
                                $scope.Hotels.push(angular.copy(response.data));
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
                // $scope.back();
            },
            function (response) {
                debugger;
                ss = response;
            });
        }

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

        HotelsApi.SaveImage($scope.HotelDetails.ImageList).then(function (response) {
            switch (response.data[0].OperationStatus) {

                case "Succeded":
                    debugger;
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HotelDetails.ImageList.indexOf($filter('filter')($scope.HotelDetails.ImageList, { 'Id': $scope.HotelDetails.ImageList.Id }, true));
                            $scope.HotelDetails.ImageList[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'Id': $scope.HotelDetails.Id }, true));
                            // $scope.Hotels[index] = angular.copy(response.data);
                            HotelsApi.GetAll().then(function (response) {
                                $scope.Hotels = response.data;
                            });
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            index = $scope.HotelDetails.indexOf($filter('filter')($scope.HotelDetails, { 'Id': $scope.HotelDetails.Id }, true));
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

            //  $scope.HotelDetails = response.data;
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
        debugger;
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
            debugger;
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    debugger;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.HotelDetails.ImageList.indexOf($filter('filter')($scope.HotelDetails.ImageList, { 'ID': $scope.HotelDetails.ImageList.Id }, true)[0]);
                            $scope.HotelDetails.ImageList[index] = angular.copy(response.data);

                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            debugger;
                            index = $scope.HotelDetails.ImageList.indexOf($filter('filter')($scope.HotelDetails.ImageList, { 'Id': $scope.HotelDetails.ImageList.Id }, true));
                            $scope.HotelDetails.ImageList[index] = angular.copy(response.data);

                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            debugger;
                            index = $scope.HotelDetails.ImageList.indexOf($filter('filter')($scope.HotelDetails.ImageList, { 'Id': $scope.HotelDetails.ImageList.Id }, true)[0]);
                            $scope.HotelDetails.ImageList[index] = angular.copy(response.data);

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

            //   $scope.HotelDetails = response.data;
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

            var extn = $scope.files[i].type;
            var fileLength = $scope.files[i].size;
            if (fileLength > maxFileSize) {
                $scope.countFiles = null;
                angular.element("input[type='file']").val(null);
                alert($scope.ImageSizeValidaiton);
                return;
            }
            if (extn !== "image/jpg" && extn !== "image/png") {
                $scope.countFiles = null;
                angular.element("input[type='file']").val(null); 
                alert($scope.ImageFormatValidaiton);
                return;
            }
            //for (var j = 0; j < validFileExtensions.length; j++) {
            //    var sCurExtension = validFileExtensions[j];
            //    if (extn !== sCurExtension) {
            //        angular.element("input[type='file']").val(null); 
            //        alert($scope.ImageFormatValidaiton);
            //        return;

            //    }
            //}
            data.append("uploadedFile", $scope.files[i]);
            $scope.HotelDetails.ImageList.push(
                {
                    "Id": 0,
                    // "Hotel_Id": $scope.HotelDetails.ImageList[0].Hotel_Id,
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






