controllerProvider.register('CouriersController', ['$scope', 'CouriersApi', '$rootScope', '$timeout', '$uibModal', '$filter', 'fileReader', 'toastr', CouriersController]);


function CouriersController($scope, CouriersApi, $rootScope, $timeout, $uibModal, $filter, fileReader, toastr) {
    debugger;
    $scope.ShowTableData = true;
    $scope.ShowFrmAddUpdate = false;
    $rootScope.ViewLoading = true;
    CouriersApi.GetAll().then(function (response) {
        $scope.Couriers = response.data;
       
    });

    CouriersApi.GetCitiesByCountryId(7).then(function (response) {
        debugger;
        $scope.Cities = response.data;

        CouriersApi.GetAllShipmentTypes().then(function (response) {
            debugger;
            $scope.ShipmentTypes = response.data;

            CouriersApi.GetAllProductSizes().then(function (response) {
                $scope.ProductSizes = response.data;
                $rootScope.ViewLoading = false;
            });
        });
    });

   


    $scope.open = function (Courier) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $scope.ShowTableData = false;
        $scope.ShowFrmAddUpdate = true;
        $scope.action = Courier == null ? 'add' : 'edit';
        //$scope.FrmAddUpdate.$setPristine();
        //$scope.FrmAddUpdate.$setUntouched();
        this.isFrmAddUpdateInvalid = false;
        $scope.isFrmRowformInvalid = false;
        $scope.isSecondFrmRowformInvalid = false;
        if (Courier == null) Courier = { CourierDetails: [] };
        else{
            var ShipmentTypeIndex = $scope.ShipmentTypes.indexOf($filter('filter')($scope.ShipmentTypes, { 'ID': Courier.ShipmentTypeID }, true)[0]);
            $scope.SelectedShipmentType = $scope.ShipmentTypes[ShipmentTypeIndex];
        }
        $scope.Courier = angular.copy(Courier);


        $timeout(function () {
            document.querySelector('select[name="LstShipmentType"]').focus();
        }, 500);



    }

 
 
    $scope.selectedShipmentTypesChanged = function (SelectedShipmentType) {
        debugger;
        $scope.SelectedShipmentType = SelectedShipmentType;
    }
   

    $scope.Restore = function (Courier) {
        debugger;
        $scope.Courier = angular.copy(Courier);
        $scope.Courier.IsDeleted = false;
        $scope.action = 'restore';
        $scope.save();
    }


    $scope.save = function () {
        debugger;
        $rootScope.ViewLoading = true;
        debugger;
        var c = $scope.Courier;
        if ($scope.action != 'delete' && $scope.action != 'restore') {
            $scope.Courier.ShipmentTypeID = $scope.SelectedShipmentType.ID;
        }
        CouriersApi.Save($scope.Courier).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {

                case "Succeded":
                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.Couriers.indexOf($filter('filter')($scope.Couriers, { 'ID': $scope.Courier.ID }, true)[0]);
                            $scope.Couriers[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.Couriers.indexOf($filter('filter')($scope.Couriers, { 'ID': $scope.Courier.ID }, true)[0]);
                            $scope.Couriers[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.Couriers.indexOf($filter('filter')($scope.Couriers, { 'ID': $scope.Courier.ID }, true)[0]);
                            $scope.Couriers[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.Couriers.push(angular.copy(response.data));
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
            ss = response;
        });
    }

    $scope.SaveCourierDetails = function (CourierDetail) {
        $rootScope.ViewLoading = true;
        CouriersApi.SaveCourierDetails(CourierDetail).then(function (response) {
            debugger;
            switch (response.data.OperationStatus) {

                case "Succeded":
                    switch ($scope.action) {
                        case 'delete':
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                    }
                    break;
                default:

            }

            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            $rootScope.ViewLoading = false;
        });
    }

    $scope.back = function () {
        $scope.ShowFrmAddUpdate = false;
        $scope.ShowTableData = true;
    }

    $scope.Delete = function (Courier) {
        debugger;
        $scope.action = 'delete';
        $scope.Courier = Courier;
        $scope.Courier.IsDeleted = true;
        $scope.save();
    }



    $scope.showCity = function (CourierDetail) {
        var selected = [];
        if (CourierDetail.CityID) {
            selected = $filter('filter')($scope.Cities, { ID: CourierDetail.CityID });
        }
        return selected.length ? selected[0].NameAr : 'Not set';
    };

    $scope.showProductSize = function (CourierDetail) {
        var selected = [];
        if (CourierDetail.ProductSizeID) {
            selected = $filter('filter')($scope.ProductSizes, { ID: CourierDetail.ProductSizeID });
        }
        return selected.length ? selected[0].NameAr : 'Not set';
    };

    $scope.removeCourierDetail = function (CourierDetail) {
        debugger;
        if (CourierDetail.ID == null) {
            var index = $scope.Courier.CourierDetails.indexOf($filter('filter')($scope.Courier.CourierDetails, { '$$hashKey': CourierDetail.$$hashKey }, true)[0]);
            $scope.Courier.CourierDetails.splice(index, 1);
        }
        else {
            var index = $scope.Courier.CourierDetails.indexOf($filter('filter')($scope.Courier.CourierDetails, { 'ID': CourierDetail.ID }, true)[0]);
            CourierDetail.IsDeleted = true;
            $scope.Courier.CourierDetails[index] = angular.copy(CourierDetail);
        }
        //$scope.$apply();
    };

    $scope.addCourierDetail = function () {
        debugger;
        var c = /^([a-z0-9]{5,})$/.test('abc1');
        $scope.inserted = {
            DeliveryFrom: null,
            DeliveryTo: null,
            CityID: null,
            ValuePerItem: null,
            ValueManyItems: null,
            ProductSizeID: null,
            IsDeleted:null
        };
        $scope.Courier.CourierDetails.push($scope.inserted);
        $scope.isFrmRowformInvalid = true;
        $scope.isSecondFrmRowformInvalid = true;
    };

    $scope.cancelRowform = function (rowform, CourierDetail) {
        debugger;
        !$scope.isFrmRowformInvalid ? rowform.$cancel() : $scope.removeCourierDetail(CourierDetail)
        $scope.isSecondFrmRowformInvalid = false;
        $scope.isFrmRowformInvalid = false;

    }

    $scope.cancelSecondRowform = function (rowform, CourierDetail) {
        debugger;
        !$scope.isSecondFrmRowformInvalid ? rowform.$cancel() : $scope.removeCourierDetail(CourierDetail)
        $scope.isSecondFrmRowformInvalid = false;
        $scope.isFrmRowformInvalid = false;
    }
   

    $scope.validateCity = function ($data)
    {
        $scope.isFrmRowformInvalid = false;
        if ($data == null) {
            $scope.isFrmRowformInvalid = true;
            return "Please Choose a City";
        }
    }

    $scope.validateDeliveryFrom = function ($data)
    {
        $scope.isFrmRowformInvalid = false;
        debugger
        if ($data == null) {
            $scope.isFrmRowformInvalid = true;
            return "Please enter the delivery from time";
        }
        if (!/^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$/.test($data)) {
            $scope.isFrmRowformInvalid = true;
            return "Delivery From Fieled should be more than or equal zero";
        }
    }

    $scope.validateDeliveryTo = function ($data)
    {
        $scope.isFrmRowformInvalid = false;
        debugger;
        if ($data == null){
            $scope.isFrmRowformInvalid = true;
            return "enter the delivery to time";
        }
          
        if (!/^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$/.test($data)){
            $scope.isFrmRowformInvalid = true;
            return "Delivery To Fieled shouldn't be less than zero";
        }
        if ($data < $('#txtDeliveryFromId').val()) {
            $scope.isFrmRowformInvalid = true;
            return "Delivery To shouldn't be less than Delivery From";
        }
    }

    $scope.validateProductSize = function ($data) {
        $scope.isSecondFrmRowformInvalid = false;
        debugger;
        if ($data == null) {
            $scope.isSecondFrmRowformInvalid = true;
            return "select Product Size";
        }
    }

    $scope.validateValuePerItem = function ($data) {
        $scope.isSecondFrmRowformInvalid = false;
        if (!/^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$/.test($data)) {
            $scope.isSecondFrmRowformInvalid = true;
            return "Fees amount shouldn't be less than Zero";
        }
    }

    $scope.validateValueManyItems = function ($data){
        $scope.isSecondFrmRowformInvalid = false;
        if (!/^\s*(?=.*[0-9])\d*(?:\.\d{1,9})?\s*$/.test($data)) {
            $scope.isSecondFrmRowformInvalid = true;
            return "Fees amount shouldn't be less than Zero";
        }
    }

}