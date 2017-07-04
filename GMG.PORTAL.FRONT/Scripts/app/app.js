/// <reference path="../lib/angular.js" />
'use strict';
var app = angular.module('BlurAdmin', [
  'ngAnimate',
  'ui.bootstrap',
  'ui.sortable',
  'ui.router',
  'ngTouch',
  'toastr',
  'smart-table',
  "xeditable",
  'ui.slimscroll',
  'ngJsTree',
  'angular-progress-button-styles',
  'BlurAdmin.theme',
  'BlurAdmin.pages',
  'flow',
  'ui.select'


]);

app.directive("notInList", function () {
    return {
        require: "ngModel",
        scope: {
            listSource: "=notInList",
            listItemId: "="
        },
        link: function (scope, element, attributes, ngModel) {
            var propertyNameTokens = attributes.ngModel.split('.');
            var propertyName = propertyNameTokens[propertyNameTokens.length - 1];

            var propertyIdTokens = attributes.listItemId.split('.');
            var propertyId = propertyIdTokens[propertyIdTokens.length - 1];


            ngModel.$validators.notInList = function (modelValue) {
                if (modelValue == null) modelValue = '';
                modelValue = modelValue.toLowerCase();
                var IsValid = true;
                angular.forEach(scope.listSource, function (listSourceItem) {
                    if (listSourceItem[propertyId] != scope.listItemId) {
                        if (listSourceItem[propertyName] != null) {
                            if (listSourceItem[propertyName].toLowerCase() == modelValue) {
                                IsValid = false;
                                return;
                            }
                        }
                    }
                });
                return IsValid;
            };

            scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (newValue) {
                ngModel.$validate();
            });
        }
    };
});

app.directive("compareTo", function () {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                if (modelValue == null) modelValue = '';
                return modelValue == scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
});


app.directive('ngConfirm', function ($compile) {
    return {
        restrict: 'A',
        priority: 1,
        terminal: true,
        link: function (scope, element, attr) {
            var Model = '<div id="divNgConfirmModel" class="modal fade" role="dialog">' +
                            '<div class="modal-dialog">' +
                                '<div class="modal-content">' +
                                    '<div class="modal-body"></div>' +
                                    '<div class="modal-footer">' +
                                    '<button type="button" class="btn btn-success btn-with-icon" data-dismiss="modal"><i class="fa fa-ban" aria-hidden="true"></i>Cancel</button><button btnconfirm type="button" class="btn btn-warning btn-with-icon"><i class="ion-android-checkmark-circle"></i>Ok</button>' +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>';
            function Confirm() {
                $('#divNgConfirmModel').modal('hide');
                scope.$eval(attr.ngClick);

            }
            element.bind('click', function () {
                $('body').append(Model);
                $('#divNgConfirmModel .modal-body').html(attr.ngConfirm);
                $('#divNgConfirmModel').modal('show');
                $('#divNgConfirmModel').on('hidden.bs.modal', function () {
                    $('#divNgConfirmModel button[btnconfirm]').off('click', Confirm);
                    $('#divNgConfirmModel').remove();
                });
                $('#divNgConfirmModel button[btnconfirm]').on('click', Confirm);
            });

        }
    };
});



//app.directive('customUploader', function () {
//    debugger;
//    var uploaderLinkFun = function (scope, element, attrs) {
//        debugger;

//        scope.processFiles = function (files) {
//            debugger;
//            angular.forEach(files, function (flowFile, i) {
//                var fileReader = new FileReader();
//                fileReader.onload = function (event) {
//                    var uri = event.target.result;
//                    scope.imagesBases.push(uri);
//                    scope.$apply();
//                };
//                fileReader.readAsDataURL(flowFile.file);
//            });
//        };

//        scope.DeleteImage = function ($index) {
//            debugger;
//            scope.imagesBases.splice($index, 1);
//        }
//    };

//    return {
//        restrict: 'AE',
//        scope: {
//            imagesBases: "="
//        },
//        link: uploaderLinkFun,
//        template: '<div flow-init flow-name="uploader.flow" flow-files-added="processFiles($files)">' +
//                       '<button class="btn btn-default"  flow-btn type="file">Select Images</button>' +
//                       ' <div class="form-group">'+
//                           '<div class="uploader col-md-3" ng-repeat="image in uploader.flow.files track by $index">'+
//                               '<div class="form-group">'+
//                                     '<img ng-show="true"  class="preview" ng-src="{{imagesBases[$index]}}"  id="{{image.uniqueIdentifier}}"/>' +
//                                     '<button class="btn btn-danger" ng-click="uploader.flow.removeFile(image);DeleteImage($index)">Delete</button>'+
//                               '</div>'+
//                           '</div>'+
//                       '</div>'+
//                   '</div>'
//    };
//});

app.directive('customUploader', function () {
    debugger;
    var uploaderLinkFun = function (scope, element, attrs) {
        debugger;
        scope.SliderStartIndex = 0;
        scope.SliderEndIndex = 3;

        scope.processFiles = function (files) {
            debugger;
            angular.forEach(files, function (flowFile, i) {
                var fileReader = new FileReader();
                fileReader.onload = function (event) {
                    var uri = event.target.result;
                    scope.imagesBases.push(uri);
                    scope.$apply();
                };
                fileReader.readAsDataURL(flowFile.file);
            });
        };

        scope.DeleteImage = function ($index) {
            debugger;
            scope.imagesBases.splice($index, 1);
            if ($index <= scope.imagesBases.length)
                scope.pre();
        }

        scope.next = function () {
            if (scope.SliderEndIndex < scope.imagesBases.length) {
                scope.SliderStartIndex++;
                scope.SliderEndIndex++;
            }
        }

        scope.pre = function () {
            if (scope.SliderStartIndex > 0) {
                scope.SliderStartIndex--;
                scope.SliderEndIndex--;
            }
        }

    };

    return {
        restrict: 'AE',
        scope: {
            imagesBases: "="
        },
        link: uploaderLinkFun,
        template: '<div flow-init flow-name="uploader.flow" flow-files-added="processFiles($files)">' +
                       '<button class="btn btn-default" flow-btn type="file">Browse Images ...</button>' +
                       '<div class="form-group" ng-show="imagesBases.length">' +
                            '<div class="col-md-1" style="margin-top:40px">' +
                                 '<button ng-show="imagesBases.length>4" ng-disabled="SliderStartIndex<=0"  ng-click="pre()" type="button" class="btn btn-primary btn-with-icon"><span aria-hidden="true"></span> <i class="fa fa-arrow-left" aria-hidden="true"></i></button>' +
                            '</div>' +
                            '<div class="col-md-10">' +
                                 '<div ng-show="$index>=SliderStartIndex && $index<=SliderEndIndex" class="uploader col-md-3" ng-repeat="image in imagesBases track by $index">' +
                                      '<div class="form-group">' +
                                           '<img class="preview img-rounded" ng-src="{{image}}" id="{{image.uniqueIdentifier}}" />' +
                                           '<button class="btn btn-danger" ng-click="DeleteImage($index);uploader.flow.removeFile(uploader.flow.files[$index])">Delete</button>' +
                                      '</div>' +
                                 '</div>' +
                            '</div>' +
                            '<div class="col-md-1" style="margin-top:40px">' +
                                 '<button ng-show="imagesBases.length>4"  ng-disabled="SliderEndIndex>=imagesBases.length-1"  ng-click="next()" type="button" class="btn btn-primary btn-with-icon"><i class="fa fa-arrow-right" aria-hidden="true"></i> <span aria-hidden="true"></span></button>' +
                            '</div>' +
                      '</div>' +
                 '</div>'

    };
});

app.directive('customUploaderWithMainImage', function ($compile) {
    debugger;
    var uploaderLinkFun = function (scope, element, attrs, controller) {
        debugger;
        scope.SliderStartIndex = 0;
        scope.SliderEndIndex = 3;
        if (scope.imagesBases && scope.imagesBases.length == 1) {
            scope.mainImage = scope.imagesBases[0];
        }
        scope.processFiles = function (files) {
            debugger;
            angular.forEach(files, function (flowFile, i) {
                var fileReader = new FileReader();
                fileReader.onload = function (event) {
                    var uri = event.target.result;
                    scope.imagesBases.push(uri);
                    if (scope.imagesBases.length == 1)
                        scope.mainImage = uri;
                    scope.$apply();
                };
                fileReader.readAsDataURL(flowFile.file);
            });
        };

        scope.DeleteImage = function ($index) {
            debugger;
            if (scope.imagesBases[$index] == scope.mainImage)
                scope.mainImage = null;

            scope.imagesBases.splice($index, 1);

            if (scope.imagesBases.length == 1)
                scope.mainImage = scope.imagesBases[0];

            if ($index <= scope.imagesBases.length)
                scope.pre();
        }

        scope.next = function () {
            if (scope.SliderEndIndex < scope.imagesBases.length) {
                scope.SliderStartIndex++;
                scope.SliderEndIndex++;
            }
        }

        scope.pre = function () {
            if (scope.SliderStartIndex > 0) {
                scope.SliderStartIndex--;
                scope.SliderEndIndex--;
            }
        }

        scope.selected = {};
        scope.changeSlecedMain = function (image) {
            scope.mainImage = image;
        }


    };

    return {
        restrict: 'AE',
        scope: {
            imagesBases: "=",
            mainImage: "="
        },
        link: uploaderLinkFun,
        template: '<div flow-init flow-name="uploader.flow" flow-files-added="processFiles($files)">' +
                       '<button class="btn btn-default" flow-btn type="file">Browse Images ...</button>' +
                       '<div class="form-group" ng-show="imagesBases.length">' +
                            '<div class="col-md-1" style="margin-top:40px">' +
                                 '<button ng-show="imagesBases.length>4" ng-disabled="SliderStartIndex<=0"  ng-click="pre()" type="button" class="btn btn-primary btn-with-icon"><span aria-hidden="true"></span> <i class="fa fa-arrow-left" aria-hidden="true"></i></button>' +
                            '</div>' +
                            '<div class="col-md-10">' +
                                 '<div ng-show="$index>=SliderStartIndex && $index<=SliderEndIndex" class="uploader col-md-3" ng-repeat="image in imagesBases track by $index" ng-init="selected.val=imagesBases[0]">' +
                                      '<div class="form-group" >' +
                                           '<img class="preview img-rounded" ng-src="{{image}}" id="{{image.uniqueIdentifier}}" />' +
                                           '<button class="btn btn-danger" ng-click="DeleteImage($index);uploader.flow.removeFile(uploader.flow.files[$index])">Delete</button>' +
                                           '<label><input type="radio"  name="MainProductImage" ng-model="selected.val" ng-value="image" ng-change="changeSlecedMain(image)" >set Main</label>' +
                                      '</div>' +
                                 '</div>' +
                            '</div>' +
                            '<div class="col-md-1" style="margin-top:40px">' +
                                 '<button ng-show="imagesBases.length>4"  ng-disabled="SliderEndIndex>=imagesBases.length-1"  ng-click="next()" type="button" class="btn btn-primary btn-with-icon"><i class="fa fa-arrow-right" aria-hidden="true"></i> <span aria-hidden="true"></span></button>' +
                            '</div>' +
                      '</div>' +
                 '</div>'

    };
});