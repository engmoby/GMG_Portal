controllerProvider.register('OwnersController', ['$scope', 'appCONSTANTS', 'OwnersApi', 'uploadOwnersService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OwnersController]);
function OwnersController($scope, appCONSTANTS, OwnersApi, uploadOwnersService, $rootScope, $timeout, $filter, $uibModal, toastr) {

    $scope.data = { static: true }

    var tmpList = [];

    for (var i = 1; i <= 6; i++) {
        tmpList.push({
            text: 'Item ' + i,
            value: i
        });
    }

    $scope.list = tmpList;


    $scope.sortingLog = [];


    $scope.sortableOptions = { 
         
        stop: function (e, ui) {
            // this callback has the changed model
            var logEntry = $scope.Owners.map(function (i) {
                return i.Sorder;
            }).join(', ');
            debugger;

            $scope.sortingLog.push(logEntry);
            for (var i = 0; i < $scope.Owners.length; i++) {
                $scope.Owners[i].Sorder = i;
                 //for (var s = 0; s < $scope.sortingLog.length; s++) {
                 //    $scope.Owners[i].Sorder = $scope.sortingLog[s].Sorder;
                 //    s += 1;
                 //    continue;
                 //}
            }
             console.log($scope.Owners);
        }
    };

    $scope.submitOrder = function () {
        debugger;
        // $scope.orderList = []; // $scope.selectedFeatures.features.HotelId = $scope.Hotel.Id;
        // for (var i = 0; i < $scope.Owners.length; i++) {
        //    $scope.orderList.push({ 
        //        Feature_Id: $scope.sortingLogId[i] 
        //    });
        //}
         OwnersApi.SaveOrder($scope.Owners).then(function (response) {
        //    $scope.Owners = response.data;
            $('#ModelOrder').modal('hide');

        },
            function (response) {
                debugger;
                $rootScope.ViewLoading = false;
                //toastr.error(response.data, 'Error');

            });
    }
    $scope.language = appCONSTANTS.supportedLanguage;

    $scope.langId = document.querySelector('#HCurrentLang').value;
    $scope.CurrentLanguage = $scope.langId;
    $("#DropdwonLang").change(function () {
        var selectedValue = $(this).val();
        document.getElementById("HCurrentLang").value = selectedValue;
        $scope.CurrentLanguage = selectedValue;
        OwnersApi.GetAll($scope.CurrentLanguage).then(function (response) {
            $scope.Owners = response.data;
            $rootScope.ViewLoading = false;
        });
    });
    $scope.ImageFormatValidaiton = "Please upload Images ";
    $scope.ImageSizeValidaiton = "Can't upload image more than 1MB";
    var maxFileSize = 2048000; // 1MB -> 1000 * 1024
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    OwnersApi.GetAll($scope.CurrentLanguage).then(function (response) {
        $scope.Owners = response.data;
        $rootScope.ViewLoading = false;

        console.log($scope.Owners);
    });
    $scope.open = function (Owner) {
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Owner == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Owner == null) Owner = {};
        $scope.Owner = angular.copy(Owner);
        if ($scope.Owner.Image)
            $scope.countFiles = $scope.Owner.Image;

    }
    $scope.openImage = function (Owner) {
        debugger;
        $('#ModelImage').modal('show');
        $scope.action = Owner == null ? 'add' : 'edit';
        if (Owner == null) Owner = {};
        $scope.Owner = angular.copy(Owner);
        if ($scope.Owner.Image)
            $scope.countFiles = $scope.Owner.Image;

    }
    $scope.openOrder = function (Owner) {
        debugger;
        $('#ModelOrder').modal('show');
        //$scope.action = Owner == null ? 'add' : 'edit';
        //if (Owner == null) Owner = {};
        //$scope.Owner = angular.copy(Owner);
        //if ($scope.Owner.Image)
        //    $scope.countFiles = $scope.Owner.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Owner) {
        debugger;
        $scope.Owner = angular.copy(Owner);
        $scope.Owner.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        debugger;
        $scope.back();
        $rootScope.ViewLoading = true;
        if ($scope.Image) {
            $scope.Owner.Image = $scope.Image;
            $scope.Image = "";
        }
        debugger;
        $scope.Owner.LangId = $scope.CurrentLanguage;
        OwnersApi.Save($scope.Owner).then(function (response) {
            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'Id': $scope.Owner.Id }, true)[0]);
                            $scope.Owners[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');

                            OwnersApi.GetAll($scope.CurrentLanguage).then(function (response) {
                                $scope.Owners = response.data;
                                $rootScope.ViewLoading = false;
                            });

                            break;
                        case 'delete':
                            index = $scope.Owners.indexOf($filter('filter')($scope.Owners, { 'Id': $scope.Owner.Id }, true)[0]);
                            $scope.Owners[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Owners.push(angular.copy(response.data));
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
            $rootScope.ViewLoading = false;
            //toastr.error(response.data, 'Error');

        });
    }

    $scope.Delete = function (Owner) {
        $scope.action = 'delete';
        $scope.Owner = Owner;
        $scope.Owner.IsDeleted = true;
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
        var extn = $scope.Image.split(".").pop();
        var fileLength = $scope.data[0].FileLength;
        if (fileLength > maxFileSize) {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageSizeValidaiton);
            return;
        }
        if (extn !== "jpg" && extn !== "jpeg" && extn !== "png") {
            $scope.countFiles = null;
            angular.element("input[type='file']").val(null);
            alert($scope.ImageFormatValidaiton);
            return;
        }
        uploadOwnersService.uploadFiles($scope)
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






