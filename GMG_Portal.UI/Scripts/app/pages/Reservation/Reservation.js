controllerProvider.register('ReservationController', ['$scope', 'ReservationApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', ReservationController]);
function ReservationController($scope, ReservationApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    ReservationApi.GetAll().then(function (response) {
        debugger;
        $scope.Reservations = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Reservation) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Reservation == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Reservation == null) Reservation = {};
        $scope.Reservation = angular.copy(Reservation);
    }
      $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }
     
    $scope.Seen = function (Reservation) {
        debugger;
        $scope.Reservation = angular.copy(Reservation);
        if (Reservation.Seen === true) {
            $scope.Reservation.Seen = false;

        } else {

            $scope.Reservation.Seen = true;
        }
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        ReservationApi.Save($scope.Reservation).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Reservations.indexOf($filter('filter')($scope.Reservations, { 'Id': $scope.Reservation.Id }, true)[0]);
                            $scope.Reservations[index] = angular.copy(response.data);
                            //ReservationApi.GetAll().then(function (response) {
                            //    $scope.Reservation = response.data;
                            //});
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Reservations.indexOf($filter('filter')($scope.Reservations, { 'Id': $scope.Reservation.Id }, true)[0]);
                            $scope.Reservations[index] = angular.copy(response.data);
                            //ReservationApi.GetAll().then(function (response) {
                            //    $scope.Reservation = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //ReservationApi.GetAll().then(function (response) {
                            //    $scope.Reservation = response.data;
                            //});
                            $scope.Reservations.push(angular.copy(response.data));
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

     

}






