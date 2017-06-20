controllerProvider.register('CustomerPointsController', ['$scope', 'CustomerPointsApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CustomerPointsController]);


function CustomerPointsController($scope, CustomerPointsApi, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $rootScope.ViewLoading = true;
    CustomerPointsApi.GetAll().then(function (response) {
        $scope.CustomerPoints = response.data;
        $rootScope.ViewLoading = false;
    },
    function (error) {
        $rootScope.ViewLoading = false;
    });

    CustomerPointsApi.GetAllInvoiceTypes().then(function (response) {
        $scope.InvoiceTypes = response.data;
        $scope.selectedInvoiceType = $scope.InvoiceTypes[0];
    },
    function (error) {
        $rootScope.ViewLoading = false;
    });

    CustomerPointsApi.GetAllPaymentTypes().then(function (response) {
        $scope.PaymentTypes = response.data;
    },
    function (error) {
        $rootScope.ViewLoading = false;
    });

    CustomerPointsApi.GetAllPaymentTimes().then(function (response) {
        $scope.PaymentTimes = response.data;
    },
    function (error) {
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (CustomerPoint) {
        debugger;
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = CustomerPoint == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (CustomerPoint == null) {
            CustomerPoint = {};
            $scope.selectedPaymentType = null;
            $scope.selectedPaymentTime = null;
        }
        else {
            var PaymentTypeindex = $scope.PaymentTypes.indexOf($filter('filter')($scope.PaymentTypes, { 'ID': CustomerPoint.PaymentTypeID }, true)[0]);
            $scope.selectedPaymentType = $scope.PaymentTypes[PaymentTypeindex];

            var PaymentTimeindex = $scope.PaymentTimes.indexOf($filter('filter')($scope.PaymentTimes, { 'ID': CustomerPoint.PaymentTimeID }, true)[0]);
            $scope.selectedPaymentTime = $scope.PaymentTimes[PaymentTimeindex];
        }
        $scope.CustomerPoint = angular.copy(CustomerPoint);
        $timeout(function () {
            document.querySelector('select[name="LstPaymentType"]').focus();
        }, 500);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (CustomerPoint) {
        debugger;
        $scope.CustomerPoint = angular.copy(CustomerPoint);
        $scope.CustomerPoint.IsDeleted = false;
        $scope.action = 'restore';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true;
        if ($scope.action == 'add' || $scope.action == 'edit') {
            $scope.CustomerPoint.PaymentTypeID = $scope.selectedPaymentType.ID;
            $scope.CustomerPoint.InvoiceTypeID = $scope.selectedInvoiceType.ID;
            $scope.CustomerPoint.PaymentTimeID = $scope.selectedPaymentTime.ID;
        }
        debugger;
        CustomerPointsApi.Save($scope.CustomerPoint).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":

                    var PaymentTypeindex = $scope.PaymentTypes.indexOf($filter('filter')($scope.PaymentTypes, { 'ID': response.data.PaymentTypeID }, true)[0]);
                    response.data.PaymentTypeNameAr = $scope.PaymentTypes[PaymentTypeindex].NameAr;
                    response.data.PaymentTypeNameEn = $scope.PaymentTypes[PaymentTypeindex].NameEn;

                    var PaymentTimeindex = $scope.PaymentTimes.indexOf($filter('filter')($scope.PaymentTimes, { 'ID': response.data.PaymentTimeID }, true)[0]);
                    response.data.PaymentTimeNameAr = $scope.PaymentTimes[PaymentTimeindex].NameAr;
                    response.data.PaymentTimeNameEn = $scope.PaymentTimes[PaymentTimeindex].NameEn;

                    var InvoiceTypeindex = $scope.InvoiceTypes.indexOf($filter('filter')($scope.InvoiceTypes, { 'ID': response.data.InvoiceTypeID }, true)[0]);
                    response.data.InvoiceTypeNameAr = $scope.InvoiceTypes[InvoiceTypeindex].NameAr;
                    response.data.InvoiceTypeNameAr = $scope.InvoiceTypes[InvoiceTypeindex].NameEn;

                    switch ($scope.action) {
                        case 'edit':
                            var index = $scope.CustomerPoints.indexOf($filter('filter')($scope.CustomerPoints, { 'ID': $scope.CustomerPoint.ID }, true)[0]);
                            $scope.CustomerPoints[index] = angular.copy(response.data);
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            var index = $scope.CustomerPoints.indexOf($filter('filter')($scope.CustomerPoints, { 'ID': $scope.CustomerPoint.ID }, true)[0]);
                            $scope.CustomerPoints[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'restore':
                            var index = $scope.CustomerPoints.indexOf($filter('filter')($scope.CustomerPoints, { 'ID': $scope.CustomerPoint.ID }, true)[0]);
                            $scope.CustomerPoints[index] = angular.copy(response.data);
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            $scope.CustomerPoints.push(angular.copy(response.data));
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

    $scope.Delete = function (CustomerPoint) {
        $scope.action = 'delete';
        $scope.CustomerPoint = CustomerPoint;
        $scope.CustomerPoint.IsDeleted = true;
        $scope.save();
    }

    $scope.SelectedPaymentTypeChanged = function (selectedPaymentType) {
        $scope.selectedPaymentType = selectedPaymentType;
    }

    $scope.selectedInvoiceTypeChanged = function (selectedInvoiceType) {
        $scope.selectedInvoiceType = selectedInvoiceType;
    }

    $scope.SelectedPaymentTimeChanged = function (selectedPaymentTime) {
        $scope.selectedPaymentTime = selectedPaymentTime;
    }
}





