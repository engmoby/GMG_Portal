controllerProvider.register('HotelsController', ['$scope', 'HotelsApi', 'uploadHotlesService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', 'Map', HotelsController]);
function HotelsController($scope, HotelsApi, uploadHotlesService, $rootScope, $timeout, $filter, $uibModal, toastr, Map) {
    $rootScope.ViewLoading = true;

    var langId = document.querySelector('#HCurrentLang').value;
    var CurrentLanguage = langId;
    $scope.features = "";
    $scope.hotelFeatures = [];
    $scope.hotelFeatures = makeList('b');
    $scope.sortingLog = [];
    $scope.sortingLogId = [];
    $scope.showSaveFeatureBtn = false;

    $scope.sortingLog = [];


    $scope.place = {};

    $scope.search = function () {
        $scope.apiError = false;
        Map.search($scope.searchPlace)
            .then(
                function (res) { // success
                    Map.addMarker(res);
                    $scope.place.name = res.name;
                    $scope.place.lat = res.geometry.location.lat();
                    $scope.place.lng = res.geometry.location.lng();
                    $scope.Hotel.Late = res.geometry.location.lat();
                    $scope.Hotel.Long = res.geometry.location.lng();
                },
                function (status) { // error
                    $scope.apiError = true;
                    $scope.apiStatus = status;
                }
            );
    }

    Map.init();
    $scope.openLocation = function () {
        $('#ModelLocation').modal('show');

    }
    $("#DropdwonLang").change(function () {
        var selectedText = $(this).find("option:selected").text();
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        CurrentLanguage = selectedValue;
        HotelsApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Hotels = response.data;
            $rootScope.ViewLoading = false;
        });
    });

    $scope.Image = "";
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024
    var validFileExtensions = ["image/jpg", "image/jpeg", "image/bmp", "image/gif", "image/png"];

    $scope.letterLimit = 20;
    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;
    $scope.basicInfo = true;
    $scope.imagesList = false;
    $scope.featuresListDiv = false;

    HotelsApi.GetAll(CurrentLanguage).then(function (response) {
        $scope.Hotels = response.data;
        $rootScope.ViewLoading = false;
    });

    $scope.open = function (hotel) {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.invalidupdateAddFrm = true;
        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;
        $scope.basicInfo = true;
        $scope.imagesList = false;
        $scope.featuresListDiv = false;
        $scope.action = hotel == null ? 'add' : 'edit';

        this.isFrmAddUpdateInvalid = false;
        $scope.isFrmRowformInvalid = false;
        $scope.isSecondFrmRowformInvalid = false;

        if (hotel == null) hotel = {};
        else {
            HotelsApi.GetHotelDetails(hotel.Id, CurrentLanguage).then(function (response) {
                $scope.HotelDetails = response.data;
            });

        }
        hotel.CheckIn = new Date(hotel.CheckIn);
        hotel.CheckOut = new Date(hotel.CheckOut);
        $scope.Hotel = angular.copy(hotel);


        $rootScope.ViewLoading = false;


    }
    $scope.back = function () {
        $rootScope.ViewLoading = true;

        HotelsApi.GetAll(CurrentLanguage).then(function (response) {
            $scope.Hotels = response.data;

            $rootScope.ViewLoading = false;
            $scope.ShowFrmAddUpdate = false;
            $scope.ShowTableData = true;
        });

    }
    $scope.Restore = function (hotel) {
        $scope.Hotel = angular.copy(hotel);
        $scope.Hotel.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }
    $scope.saveExist = function () {
        $rootScope.ViewLoading = true;
        $scope.back();


        HotelsApi.Save($scope.Hotel).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);

                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);

                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':

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
        },
            function (response) {
                debugger;
                ss = response;
            });

    }
    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        $scope.Hotel.LangId = CurrentLanguage;
      //  $scope.back();

        HotelsApi.Save($scope.Hotel).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            HotelsApi.GetHotelDetails($scope.Hotel.Id, CurrentLanguage).then(function (response) {
                                $scope.HotelDetails = response.data;
                                $scope.Hotel = response.data;
                            });

                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            $scope.basicInfo = false;
                            $scope.imagesList = true;
                            $rootScope.ViewLoading = false; break;
                        case 'delete':
                            index = $scope.Hotels.indexOf($filter('filter')($scope.Hotels, { 'Id': $scope.Hotel.Id }, true)[0]);
                            $scope.Hotels[index] = angular.copy(response.data);
                            //HotelsApi.GetHotelDetails($scope.Hotel.Id, CurrentLanguage).then(function (response) {
                            //    $scope.HotelDetails = response.data;
                            //    $scope.Hotel = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            $scope.basicInfo = false;
                            $scope.imagesList = true;
                            $rootScope.ViewLoading = false;
                            break;
                        case 'add':

                            $scope.Hotels.push(angular.copy(response.data));
                            //HotelsApi.GetHotelDetails(response.Hotel.Id, CurrentLanguage).then(function (response) {
                            //    $scope.HotelDetails = response.data;
                            //    $scope.Hotel = response.data;
                            //});
                            $scope.basicInfo = false;
                            $scope.imagesList = true;
                            $rootScope.ViewLoading = false; toastr.success($('#HSaveSuccessMessage').val(), 'Success');
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

            // $scope.HotelDetails = response.data;

            // $scope.back();
        },
        function (response) {
            debugger;
            ss = response;
        });


    }
    $scope.Delete = function (Hotel) {
        $scope.action = 'delete';
        $scope.Hotel = Hotel;
        $scope.Hotel.IsDeleted = true;
        $scope.save();
    }


    $scope.saveImage = function () {
        $rootScope.ViewLoading = true;
        $scope.backImage();

        HotelsApi.SaveImage($scope.Hotel.ImageList).then(function (response) {
            switch (response.data[0].OperationStatus) {

                case "Succeded":
                    debugger;
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': $scope.Hotel.ImageList.Id }, true));
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);
                            //HotelsApi.GetAll().then(function (response) {
                            //    $scope.Hotels = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': $scope.Hotel.ImageList.Id }, true));
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);

                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': $scope.Hotel.ImageList.Id }, true));
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);
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
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.DeleteImage = function (hotelImage) {
        debugger;
        $scope.action = 'delete';
        //$scope.Hotel = hotelImage;
        hotelImage.IsDeleted = true;
        $scope.DeleteImageFunction(hotelImage);
    }
    $scope.RestoreImage = function (hotelImage) {
        debugger;
        $scope.action = 'edit';
        //$scope.Hotel = angular.copy(hotelImage);
        hotelImage.IsDeleted = false;
        $scope.DeleteImageFunction(hotelImage);
    }
    $scope.DeleteImageFunction = function (hotelImage) {
        $rootScope.ViewLoading = true;
        HotelsApi.DeleteImage(hotelImage).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    debugger;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': hotelImage.Id }, true)[0]);
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);

                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            debugger;
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': hotelImage.Id }, true)[0]);
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);

                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            debugger;
                            index = $scope.Hotel.ImageList.indexOf($filter('filter')($scope.Hotel.ImageList, { 'Id': $scope.Hotel.ImageList.Id }, true)[0]);
                            $scope.Hotel.ImageList[index] = angular.copy(response.data);

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
        $('#ModelAddUpdateImage').modal('show');
        if (Hotel == null) Hotel = {};
        $scope.Hotel = angular.copy(Hotel);

    }
    $scope.backImage = function () {
        $('#ModelAddUpdateImage').modal('hide');
    }
    $scope.showFeatures = function () {
        $rootScope.ViewLoading = true;
        $scope.features = [];
        HotelsApi.GetAllFeatures(CurrentLanguage).then(function (response) {
            $scope.features = response.data;
        });

        $scope.basicInfo = false;
        $scope.imagesList = false;
        $scope.featuresListDiv = true;

        $scope.selectedFeatures.features = [];
        $scope.hotelFeatures = [];
        var logEntry = "";
        $scope.sortingLog = [];
        if ($scope.HotelDetails.FeaturesList != null) {
            for (var j = 0; j < $scope.HotelDetails.FeaturesList.length; j++) {
                logEntry = $scope.HotelDetails.FeaturesList[j].DisplayValue;
                logEntry = (j + 1) + ': ' + logEntry;
                $scope.sortingLog.push(logEntry);
                $scope.hotelFeatures.push({
                    DisplayValue: $scope.HotelDetails.FeaturesList[j].DisplayValue,
                    Id: $scope.HotelDetails.FeaturesList[j].Id,
                    Icon: $scope.HotelDetails.FeaturesList[j].Icon
                });
            }

        }
        $rootScope.ViewLoading = false;
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
            if (extn !== "image/jpg" && extn !== "image/png" && extn !== "image/jpeg") {
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

            if ($scope.Hotel.ImageList != null || $scope.Hotel.ImageList != 0) {
            $scope.Hotel.ImageList.push(
                {
                    "Id": 0,
                    "Hotel_Id": $scope.HotelDetails.Id,
                    "Image": $scope.files[i].name
                }
            );
                
            } else {
                $scope.Hotel.ImageList = [];
                $scope.Hotel.ImageList.push(
                    {
                        "Id": 0,
                        "Hotel_Id": $scope.HotelDetails.Id,
                        "Image": $scope.files[i].name
                    }
                );
            }

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
        $scope.backImage();
        angular.element("input[type='file']").val(null);

        alert("Files uploaded successfully.");
    }

    $scope.clickCheck = function (obj) {
        if ($scope.selectedFeatures.features.indexOf(obj) !== -1) {
            debugger;
            var index = $scope.selectedFeatures.features.indexOf(obj);
            $scope.selectedFeatures.features[index].Selected = false;

            $scope.selectedFeatures.features.splice(index, 1);

        } else {
            $scope.selectedFeatures.features.push(obj);

        }
    };
    $scope.selectedFeatures = {
        features: []
    };
    $scope.checkAll = function () {
        $scope.selectedFeatures.features = angular.copy($scope.features);
    };
    $scope.uncheckAll = function () {
        $scope.selectedFeatures.features = [];
    };
    $scope.checkFirst = function () {
        $scope.selectedFeatures.features = [];
        $scope.selectedFeatures.features.push($scope.features[0]);
    };
    $scope.setToNull = function () {
        $scope.selectedFeatures.features = null;

    };

    $scope.saveSelectedFeatures = [];
    $scope.clickCheckedFeatures = function (obj) {
        if ($scope.selectedFeatures.features.indexOf(obj) !== -1) {
            debugger;
            var index = $scope.selectedFeatures.features.indexOf(obj);
            $scope.selectedFeatures.features[index].Selected = false;

            $scope.selectedFeatures.features.splice(index, 1);

        } else {
            $scope.selectedFeatures.features.push(obj);

        }
    };
    $scope.saveFeatures = function () {
        $rootScope.ViewLoading = true;
        debugger;
        $scope.logModels();
        // $scope.selectedFeatures.features.HotelId = $scope.Hotel.Id;
        for (var i = 0; i < $scope.sortingLogId.length; i++) {
            $scope.saveSelectedFeatures.push({
                Hotel_Id: $scope.Hotel.Id,
                Feature_Id: $scope.sortingLogId[i]
            });
        }
        HotelsApi.SaveFeature($scope.saveSelectedFeatures).then(function (response) {

            switch (response.data[0].OperationStatus) {

                case "Succeded":
                    debugger;
                    $scope.back();
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
            //$scope.basicInfo = true;
            //$scope.imagesList = false;
            //$scope.featuresList = false;
            //$rootScope.ViewLoading = false;
            //$scope.backImage();
        },
            function (response) {
                debugger;
                ss = response;
            });
    }


    function makeList(letter) {
        var tmpList = [];

        for (var i = 1; i <= letter.lenght; i++) {
            tmpList.push({
                DisplayValue: 'Item ' + i + letter,
                value: i
            });
        }
        return tmpList;
    }

    //$scope.sortableOptions = {
    //    placeholder: "app",
    //    connectWith: ".apps-container", scrollSensitivity: 10,
    //    update: function (event, ui) {
    //        // on cross list sortings recieved is not true
    //        // during the first update
    //        // which is fired on the source sortable
    //        if (!ui.item.sortable.received && ui.item.sortable.droptargetModel.length >= 10) {
    //            var originNgModel = ui.item.sortable.sourceModel;
    //            var itemModel = originNgModel[ui.item.sortable.index];

    //            // check that its an actual moving
    //            // between the two lists
    //            if (originNgModel == $scope.features && ui.item.sortable.droptargetModel == $scope.hotelFeatures) {
    //                var exists = !!$scope.hotelFeatures.filter(function (x) { return x.DisplayValue === itemModel.DisplayValue }).length;
    //                if (exists) {
    //                    ui.item.sortable.cancel();
    //                }
    //            }
    //        }


    //        //if (// ensure we are in the first update() callback
    //        //    !ui.item.sortable.received &&
    //        //  // check that its an actual moving
    //        //  // between the two lists
    //        //    ui.item.sortable.source[0] !== ui.item.sortable.droptarget[0] &&
    //        //  // check the size limitation
    //        //    ui.item.sortable.droptargetModel.length >= 10) {
    //        //  ui.item.sortable.cancel();
    //        //}
    //    }
    //};


    $scope.logModels = function () {
        $scope.showSaveFeatureBtn = true;
        $scope.sortingLogId = [];
        $scope.sortingLog = [];
        for (var i = 0; i < $scope.hotelFeatures.length; i++) {
            var logEntry = "";
            var logEntryId = "";
            if ($scope.hotelFeatures[i].Id != null) {
                logEntryId = $scope.hotelFeatures[i].Id;
            } else {
                logEntryId = $scope.hotelFeatures[i].Id;
            }
            $scope.sortingLogId.push(logEntryId);


            logEntry = $scope.hotelFeatures[i].DisplayValue;
            logEntry = (i + 1) + ': ' + logEntry;
            $scope.sortingLog.push(logEntry);
        }
        //$scope.saveStaticTopTen();
    };




    $scope.sortableOptions = {
        placeholder: "app",
        connectWith: ".apps-container",
        update: function (event, ui) {
            debugger;
            // on cross list sortings recieved is not true
            // during the first update
            // which is fired on the source sortable
            if (!ui.item.sortable.received) {
                var originNgModel = ui.item.sortable.sourceModel;
                var itemModel = originNgModel[ui.item.sortable.index];

                // check that its an actual moving
                // between the two lists
                if (originNgModel == $scope.features && ui.item.sortable.droptargetModel == $scope.hotelFeatures) {
                    var exists = !!$scope.hotelFeatures.filter(function (x) { return x.DisplayValue === itemModel.DisplayValue }).length;
                    if (exists) {
                        ui.item.sortable.cancel();
                    }

                }
                if (ui.item.sortable.droptargetModel == $scope.features) {
                    var index;
                    var exists2 = !!$scope.features.filter(function (x) { return x.DisplayValue === itemModel.DisplayValue }).length;
                    if (exists2) {
                        if ($scope.hotelFeatures.length === 1) {
                            index = $scope.hotelFeatures.indexOf($filter('filter')($scope.hotelFeatures, { 'Id': itemModel.Id }, true)[0]);
                            //  $scope.hotelFeatures.splice(index, 1);
                            alert("hotel must have one feature at least");
                        } else {
                            index = $scope.hotelFeatures.indexOf($filter('filter')($scope.hotelFeatures, { 'Id': itemModel.Id }, true)[0]);
                            // $scope.hotelFeatures[index] = angular.copy(response.data);
                            $scope.hotelFeatures.splice(index, 1);
                        }
                        ui.item.sortable.cancel();
                    }
                }
            }
        }
    };


}
