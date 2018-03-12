/// <reference path="E:\Projects\Portal\Project\Main\GMG_Portal.UI\partial/ba-sidbar.html" />
/// <reference path="E:\Projects\Portal\Project\Main\GMG_Portal.UI\partial/ba-sidbar.html" />
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
  //"xeditable",
  'ui.slimscroll',
  //'ngJsTree',
  'angular-progress-button-styles',
  'BlurAdmin.theme',
  'BlurAdmin.pages',
  //'flow',
  //'ui.select',
  'angular-loading-bar',
   'checklist-model',
'ui.sortable', 
'google.places',
'as.sortable'


]);
app.constant('appCONSTANTS',
    { 
        'defaultLanguage': 'en',
        'supportedLanguage': {
            'en': {'id':'1', 'key': 'en', 'value': 'english' },
            'fr': { 'id': '2', 'key': 'fr', 'value': 'frensh' },
            'ar': { 'id': '3', 'key': 'ar', 'value': 'arabic' }
        }
    });
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


app.directive('myDirectory',
    [
        '$parse', function ($parse) {

            function link(scope, element, attrs) {
                var model = $parse(attrs.myDirectory);
                element.on('change',
                    function (event) {
                        scope.data = []; //Clear shared scope in case user reqret on the selection
                        model(scope, { file: event.target.files });

                    });
            };

            return {
                link: link
            }
        }
    ]);
app.factory('uploadService', function ($http, $q) {
    return {
        uploadFiles: function ($scope) {
            debugger;
            console.log($scope.formdata);
            var request = {
                method: 'POST',
                url: '/api/upload/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                    function (response) {
                        if (typeof response.data === 'string') {
                            return response.data;
                        } else {
                            return $q.reject(response.data);
                        }
                    },
                    function (response) {
                        return $q.reject(response.data);
                    }
                );
        }

    };
});
app.factory('uploadNewsService', function ($http, $q) {
    return {
        uploadFiles: function ($scope) {
            debugger;
            console.log($scope.formdata);
            var request = {
                method: 'POST',
                url: '/api/uploadNews/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                    function (response) {
                        if (typeof response.data === 'string') {
                            return response.data;
                        } else {
                            return $q.reject(response.data);
                        }
                    },
                    function (response) {
                        return $q.reject(response.data);
                    }
                );
        }

    };
});
app.factory('uploadOwnersService', function ($http, $q) {
    return {
        uploadFiles: function ($scope) {
            debugger;
            console.log($scope.formdata);
            var request = {
                method: 'POST',
                url: '/api/uploadOwners/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                    function (response) {
                        if (typeof response.data === 'string') {
                            return response.data;
                        } else {
                            return $q.reject(response.data);
                        }
                    },
                    function (response) {
                        return $q.reject(response.data);
                    }
                );
        }

    };
});

app.factory('uploadOffersService', function ($http, $q) {
    return {
        uploadFiles: function ($scope) {
            debugger;
            console.log($scope.formdata);
            var request = {
                method: 'POST',
                url: '/api/uploadOffers/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                    function (response) {
                        if (typeof response.data === 'string') {
                            return response.data;
                        } else {
                            return $q.reject(response.data);
                        }
                    },
                    function (response) {
                        return $q.reject(response.data);
                    }
                );
        }

    };
});
app.factory('uploadHotlesService', function ($http, $q) {
    return {
        uploadFiles: function ($scope) {
            var request = {
                method: 'POST',
                url: '/api/uploadHotles/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                    function (response) {
                        if (typeof response.data === 'string') {
                            return response.data;
                        } else {
                            return $q.reject(response.data);
                        }
                    },
                    function (response) {
                        return $q.reject(response.data);
                    }
                );
        }

    };
});
app.directive('myMaxlength', ['$compile', '$log', function ($compile, $log) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            attrs.$set("ngTrim", "false");
            var maxlength = parseInt(attrs.myMaxlength, 10);
            ctrl.$parsers.push(function (value) {
                $log.info("In parser function value = [" + value + "].");
                if (value.length > maxlength) {
                    $log.info("The value [" + value + "] is too long!");
                    value = value.substr(0, maxlength);
                    ctrl.$setViewValue(value);
                    ctrl.$render();
                    $log.info("The value is now truncated as [" + value + "].");
                }
                return value;
            });
        }
    };
}]);


 

app.factory('Scopes', function ($rootScope) {
    var mem = {};

    return {
        store: function (key, value) {
            $rootScope.$emit('scope.stored', key);
            mem[key] = value;
        },
        get: function (key) {
            return mem[key];
        }
    };
});


app.directive('googleplace', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, model) {
            var options = {
                types: [],
                componentRestrictions: {}
            };
            scope.gPlace = new google.maps.places.Autocomplete(element[0], options);

            google.maps.event.addListener(scope.gPlace, 'place_changed', function () {
                scope.$apply(function () {
                    model.$setViewValue(element.val());
                });
            });
        }
    };
});


app.service('Map', function ($q) {

    this.init = function () {
        var options = {
            center: new google.maps.LatLng(40.7127837, -74.00594130000002),
            zoom: 13,
            disableDefaultUI: true
        }
        this.map = new google.maps.Map(
            document.getElementById("map"), options
        );
        this.places = new google.maps.places.PlacesService(this.map);
    }

    this.search = function (str) {
        var d = $q.defer();
        this.places.textSearch({ query: str }, function (results, status) {
            if (status == 'OK') {
                d.resolve(results[0]);
            }
            else d.reject(status);
        });
        return d.promise;
    }

    this.addMarker = function (res) {
        if (this.marker) this.marker.setMap(null);
        this.marker = new google.maps.Marker({
            map: this.map,
            position: res.geometry.location,
            animation: google.maps.Animation.DROP
        });
        this.map.setCenter(res.geometry.location);
    }

});


app.directive("ba-sidebar", function () {
    return {
        
        templateUrl: '/partial/ba-sidbar.html'
    };

});


app.directive("content-top", function () {
    debugger;
    return {
        restrict: "A",
        template: '<div class="content-top clearfix"><h1 class="al-title">{{ activePageTitle }}</h1><ul class="breadcrumb al-breadcrumb"><li><a href="#/dashboard">Home</a></li><li>{{ activePageTitle }}</li></ul></div>'
    };

});