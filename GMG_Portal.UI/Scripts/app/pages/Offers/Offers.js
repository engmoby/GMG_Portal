﻿

controllerProvider.register('OffersController', ['$scope', 'offersApi', 'uploadNewsService', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr', OffersController]);
function OffersController($scope, offersApi, uploadNewsService, $rootScope, $timeout, $filter, $uibModal, toastr) {

  $rootScope.ViewLoading = true;
  var langId = document.querySelector('#HCurrentLang').value;
  var CurrentLanguage = langId;
  $("#DropdwonLang").change(function () {
    var selectedText = $(this).find("option:selected").text();
    var selectedValue = $(this).val();
    document.getElementById("HCurrentLang").value = selectedValue;
    CurrentLanguage = selectedValue;

    offersApi.GetAll(CurrentLanguage).then(function (response) {
      $scope.offerList = response.data;
      $rootScope.ViewLoading = false;
    });

    offersApi.GetAllCategories(CurrentLanguage).then(function (response) {
      debugger;
      $scope.Categorys = response.data;
    });
  });
  $scope.SelectedCategory = null;

  $scope.Image = "";
  $scope.ImageFormatValidaiton = "Please upload Images ";
  $scope.ImageSizeValidaiton = "Can't upload image more than 2MB";
  var maxFileSize = 2048000; // 1MB -> 1000 * 1024

  $scope.letterLimit = 20;
  offersApi.GetAll(CurrentLanguage).then(function (response) {
    $scope.offerList = response.data;
    $rootScope.ViewLoading = false;
  });
  //get Categories

  offersApi.GetAllCategories(CurrentLanguage).then(function (response) {
    debugger;
    $scope.Categorys = response.data;
  });
  $scope.open = function (offer) {
    debugger;
    $scope.countFiles = '';
    $scope.invalidupdateAddFrm = true;
    $('#ModelAddUpdate').modal('show');
    $scope.action = offer == null ? 'add' : 'edit';
    $scope.FrmAddUpdate.$setPristine();
    $scope.FrmAddUpdate.$setUntouched();
    if (offer == null) offer = {}

    else {
      $scope.SelectedCategory = null;

      console.log($scope.SelectedCategory);
      var categoryIndex = $scope.Categorys.indexOf($filter('filter')($scope.Categorys, { 'Id': offer.Currency }, true)[0]);
      $scope.SelectedCategory = $scope.Categorys[categoryIndex];
      //console.log($scope.SelectedCategory.DisplayValue);
    };
    $scope.offer = angular.copy(offer);
    if ($scope.offer.Image)
      $scope.countFiles = $scope.offer.Image;

  }
  $scope.openImage = function (offer) {
    debugger;
    $('#ModelImage').modal('show');
    //$scope.action = offer == null ? 'add' : 'edit';
    if (offer == null) offer = {};
    $scope.offer = angular.copy(offer);
    //if ($scope.offer.Image)
    //    $scope.countFiles = $scope.offer.Image;

  }
  $scope.back = function () {
    $('#ModelAddUpdate').modal('hide');
  }

  $scope.Restore = function (offer) {
    debugger;
    $scope.offer = angular.copy(offer);
    $scope.offer.IsDeleted = false;
    $scope.action = 'edit';
    $scope.save();
  }
  $scope.Delete = function (offer) {
    $scope.action = 'delete';
    $scope.offer = offer;
    $scope.offer.IsDeleted = true;
    $scope.save();
  }

  $scope.save = function () {
    $rootScope.ViewLoading = true;
    if ($scope.Image) {
      $scope.offer.Image = $scope.Image;
      $scope.Image = "";
    }
    //  uploadNewsService.uploadFiles();
    debugger;
    if ($scope.SelectedCategory != null)
      $scope.offer.Currency = $scope.SelectedCategory.Id;
    $scope.offer.langId = CurrentLanguage;

    offersApi.Save($scope.offer).then(function (response) {

        switch (response.data.OperationStatus) {
        case "Succeded":
          var index;
          switch ($scope.action) {
          case 'edit':
            index = $scope.offerList.indexOf($filter('filter')($scope.offerList, { 'Id': $scope.offer.Id }, true)[0]);
            $scope.offerList[index] = angular.copy(response.data);
            offersApi.GetAll(CurrentLanguage).then(function (response) {
                $scope.offerList = response.data;
            });
            toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
            break;
          case 'delete':
            index = $scope.offerList.indexOf($filter('filter')($scope.offerList, { 'Id': $scope.offer.Id }, true)[0]);
            $scope.offerList[index] = angular.copy(response.data);
            //offersApi.GetAll(CurrentLanguage).then(function (response) {
            //    $scope.offerList = response.data;
            //});
            toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
            break;
          case 'add':
            //offersApi.GetAll(CurrentLanguage).then(function (response) {
            //    $scope.offerList = response.data;
            //});
            $scope.offerList.push(angular.copy(response.data));
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
    if (extn !== "jpg" && extn !== "png") {
      $scope.countFiles = null;
      angular.element("input[type='file']").val(null);
      //  alert("neeed jpg");
      alert($scope.ImageFormatValidaiton);
      return;
    }
    //if (extn !== "png") {
    //    $scope.countFiles = null;
    //    angular.element("input[type='file']").val(null);
    //    alert("neeed jpeg");
    //    // alert($scope.ImageFormatValidaiton);
    //    return;
    //}
    uploadNewsService.uploadFiles($scope)
      // then() called when uploadFiles gets back
      .then(function (data) {
          // promise fulfilled
          $scope.uploading = false;
          if (data === '') {
            // console.log(data);
            //   $scope.Image=data.
            $scope.save();
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



  $scope.selectedCategorysChanged = function (selectedCategory) {
    if (selectedCategory != null) {
      $scope.SelectedCategory = selectedCategory;
    }

  }

}










































