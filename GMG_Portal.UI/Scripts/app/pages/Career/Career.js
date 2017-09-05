controllerProvider.register('CareersController', ['$scope', 'CareersApi', 'uploadService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', CareersController]);
function CareersController($scope, CareersApi, uploadService, $rootScope, $timeout, $filter, $uibModal, toastr) {
    $scope.Image = "";
    $scope.letterLimit = 20;
    $rootScope.ViewLoading = true;
    CareersApi.GetAll().then(function (response) {
        $scope.Careers = response.data;
        $rootScope.ViewLoading = false;
    });
    $scope.open = function (Career) {
        debugger;
        $scope.countFiles = '';
        $scope.invalidupdateAddFrm = true;
        $('#ModelAddUpdate').modal('show');
        $scope.action = Career == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (Career == null) Career = {};
        $scope.Career = angular.copy(Career);
        if ($scope.Career.Image)
            $scope.countFiles = $scope.Career.Image;

        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 1000);
    }
    $scope.openImage = function (career) {
        debugger;
        $('#ModelImage').modal('show');
        //$scope.action = Career == null ? 'add' : 'edit';
        if (career == null) career = {};
        $scope.Career = angular.copy(career);
        //if ($scope.Career.Image)
        //    $scope.countFiles = $scope.Career.Image;

    }
    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    $scope.Restore = function (Career) {
        debugger;
        $scope.Career = angular.copy(Career);
        $scope.Career.IsDeleted = false;
        $scope.action = 'edit';
        $scope.save();
    }

    $scope.save = function () {
        $rootScope.ViewLoading = true; 
        debugger;
        CareersApi.Save($scope.Career).then(function (response) {

            switch (response.data.OperationStatus) {
                case "Succeded":
                    var index;
                    switch ($scope.action) {
                        case 'edit':
                            index = $scope.Careers.indexOf($filter('filter')($scope.Careers, { 'Id': $scope.Career.Id }, true)[0]);
                            $scope.Careers[index] = angular.copy(response.data);
                            //CareersApi.GetAll().then(function (response) {
                            //    $scope.Careers = response.data; 
                            //}); 
                            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                            break;
                        case 'delete':
                            index = $scope.Careers.indexOf($filter('filter')($scope.Careers, { 'Id': $scope.Career.Id }, true)[0]);
                            $scope.Careers[index] = angular.copy(response.data);
                            //CareersApi.GetAll().then(function (response) {
                            //    $scope.Careers = response.data;
                            //});
                            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                            break;
                        case 'add':
                            //CareersApi.GetAll().then(function (response) {
                            //    $scope.Careers = response.data;
                            //});
                           $scope.Careers.push(angular.copy(response.data));
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
                    index = $scope.Careers.indexOf($filter('filter')($scope.Careers, { 'Id': $scope.Career.Id }, true)[0]);
                    $scope.Careers[index] = angular.copy(response.data); toastr.error($('#HCannotDeleted').val(), 'Error');
                    CareersApi.GetAll().then(function (response) {
                        $scope.Careers = response.data;
                    });
                    break;
                default:

            }

           
            $scope.back();
            $rootScope.ViewLoading = false;
        },
        function (response) {
            debugger;
            ss = response;
        });
    }

    $scope.Delete = function (Career) {
        $scope.action = 'delete';
        $scope.Career = Career;
        $scope.Career.IsDeleted = true;
        $scope.save();
    }
  
}






