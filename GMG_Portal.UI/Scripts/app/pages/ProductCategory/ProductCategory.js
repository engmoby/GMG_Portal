controllerProvider.register('ProductCategoryController', ['$scope', 'ProductCategoryApi', '$rootScope', '$timeout', '$filter', '$uibModal', 'toastr','$compile', ProductCategoryController]);


function ProductCategoryController($scope, ProductCategoryApi, $rootScope, $timeout, $filter, $uibModal, toastr,$compile) {


    var loadTree = function () {
        $rootScope.ViewLoading = true;
        ProductCategoryApi.GetAll().then(function (response) {
            debugger;
            $scope.ProductCategories = response.data;
            loadDrawTree($scope.ProductCategories);


            //$('i[ng-confirm]').each(function () {
            //    var el = angular.element($(this));
            //    $compile(el)($scope);
            //});

            d3.select(self.frameElement).style("height", "500px");
            $rootScope.ViewLoading = false;
        });
    }
         
    loadTree();

        $scope.save = function () {
            debugger;
            $rootScope.ViewLoading = true;
            if($scope.action== 'add')
            $scope.ProductCategory.ParentID = $scope.ProductCategoryParent.ID;
            ProductCategoryApi.Save($scope.ProductCategory).then(function (response) {

                switch (response.data.OperationStatus) {
                    case "Succeded":
                        switch ($scope.action) {
                            case 'edit':
                                toastr.success($('#HUpdateSuccessMessage').val(), 'Success');
                                break;
                            case 'delete':
                                toastr.success($('#HDeleteSuccessMessage').val(), 'Success');
                                break;
                            case 'add':
                                //$scope.AccountTypes.push(angular.copy(response.data));
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
                    case "HasChildreen":
                        toastr.error($('#HHasChildreen').val(), 'Error');
                        break;
                    default:

                }
                debugger;
                $rootScope.ViewLoading = false;
                loadTree();
                $scope.back();

            },
            function (response) {
                debugger;
                ss = response;
            });
        }

        $scope.Delete = function (AccountType) {
            $scope.action = 'delete';
            $scope.AccountType = AccountType;
            $scope.AccountType.IsDeleted = true;
            $scope.save();
        }

            /////////////////////////
    var margin = {top: 30, right: 30, bottom: 30, left: 30},
        width = 1000 - margin.right - margin.left,
        height = 1000- margin.top - margin.bottom;

    var i = 0,
        duration = 750,
        root;

    var tree = d3.layout.tree()
        .size([height, width]);

    var diagonal = d3.svg.diagonal()
        .projection(function (d) { return [d.x, d.y]; });



   /* var svg = d3.select("#TreeGraph").append("svg")
            .attr("id", "playgraph")
             //better to keep the viewBox dimensions with variables
            .attr("viewBox", "0 0 " + width + " " + height)
            .attr("preserveAspectRatio", "xMidYMid meet");*/

      var svg = d3.select("#TreeGraph").append("svg")
        .attr("width", width + margin.right + margin.left)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
    debugger;
    var loadDrawTree = function (ProductCategories)
    {
        debugger;
        root = ProductCategories;
        root.x0 = height / 2;
        root.y0 = 0;

        function collapse(d) {
            if (d.children) {
                d._children = d.children;
                d._children.forEach(collapse);
                d.children = null;
            }
        }

        //root.children.forEach(collapse); //
        update(root);


        $('i[ng-confirm]').each(function () {
            var el = angular.element($(this));
            $compile(el)($scope);
        });

    }
    //loadDrawTree($scope.ProductCategories);
    //d3.select(self.frameElement).style("height", "800px");

    function update(source) {

      // Compute the new tree layout.
      var nodes = tree.nodes(root).reverse(),
          links = tree.links(nodes);

      // Normalize for fixed-depth.
      nodes.forEach(function(d) { d.y = d.depth * 70; });

      // Update the nodes…
      var node = svg.selectAll("g.node")
          .data(nodes, function(d) { return d.id || (d.id = ++i); });

      // Enter any new nodes at the parent's previous position.
      var nodeEnter = node.enter().append("g")
          .attr("class", "node")
          .attr("transform", function (d) { return "translate(" + source.y0 + "," + source.x0 + ")"; })
          //.on("click", click);
      debugger;
        nodeEnter.append("circle")
          .attr("r", 15)
          .style("fill", function (d) { return d._children ? "lightsteelblue" : "#fff"; })
          .on("click", clickToExpand);

        nodeEnter.append("foreignObject")
       .attr("x", 20)
       .attr("y", -20)
       .html('<i class="fa fa-plus-square" aria-hidden="true"></i>')
       .on("click", AddNewNode);

         nodeEnter.append("foreignObject")
        .attr("x", 20)
        .attr("y", 0)
        .html('<i class="fa fa-pencil-square" aria-hidden="true"></i>')
        .on("click", EditNode);

         nodeEnter.append("foreignObject")
        .attr("x", 20)
        .attr("y", 20)
        .html(function (d) { return '<i class="fa fa-trash" aria-hidden="true" ng-click=Delete("' + d.ID + '")  ng-confirm="Confirm Delete ?"></i>' });
        //.on("click", DeleteNode);

      //nodeEnter.append("text")
      //.attr("r", "4.5")
      //.attr("dx", "2em")
      //.attr("dy", "-1.5em")
      //.text(function (d) { return '<b>Add</b>' })
      //.on("click", AddNewNode);

      nodeEnter.append("text")
          .attr("x", function (d) { return -20 ; })
          .attr("dy", ".35em")
          .attr("text-anchor", function (d) { return  "end"; })
          .text(function (d) { return d.NameAr; });


      // Transition nodes to their new position.
      var nodeUpdate = node.transition()
          .duration(duration)
          .attr("transform", function(d) { return "translate(" + d.x + "," + d.y + ")"; }); 

      nodeUpdate.select("circle") //here
          .attr("r", 15)
          .style("fill", function(d) { return d._children ? "lightsteelblue" : "#fff"; });

      nodeUpdate.select("text")
          .style("fill-opacity", 1);

      // Transition exiting nodes to the parent's new position.
      var nodeExit = node.exit().transition()
          .duration(duration)
          .attr("transform", function(d) { return "translate(" + source.x + "," + source.y + ")"; })
          .remove();

      nodeExit.select("circle")
          .attr("r", 15);

      nodeExit.select("text")
          .style("fill-opacity", 1e-6);

      // Update the links…
      var link = svg.selectAll("path.link")
          .data(links, function(d) { return d.target.id; });

      // Enter any new links at the parent's previous position.
      link.enter().insert("path", "g")
          .attr("class", "link")
          .attr("d", function(d) {
            var o = {x: source.x0, y: source.y0};
            return diagonal({source: o, target: o});
          });

      // Transition links to their new position.
      link.transition()
          .duration(duration)
          .attr("d", diagonal);

      // Transition exiting nodes to the parent's new position.
      link.exit().transition()
          .duration(duration)
          .attr("d", function(d) {
            var o = {x: source.x, y: source.y};
            return diagonal({source: o, target: o});
          })
          .remove();

      // Stash the old positions for transition.
      nodes.forEach(function(d) {
        d.x0 = d.x;
        d.y0 = d.y;
      });
    }

    // Toggle children on click.
    function clickToExpand(d) {

      if (d.children) {
        d._children = d.children;
        d.children = null;
      } else {
        d.children = d._children;
        d._children = null;
      }
      update(d);

      $('i[ng-confirm]').each(function () {
          var el = angular.element($(this));
          $compile(el)($scope);
      });
    }

    $scope.open = function (ProductCategory) {
        $scope.invalidupdateAddFrm = true;
        $scope.action = ProductCategory == null ? 'add' : 'edit';
        $scope.FrmAddUpdate.$setPristine();
        $scope.FrmAddUpdate.$setUntouched();
        if (ProductCategory == null) ProductCategory = { 'NameAr': '', 'NameEn': '' };
        $scope.ProductCategory = angular.copy(ProductCategory);
        $('#ModelAddUpdate').modal('show');
        $timeout(function () {
            document.querySelector('input[name="TxtNameAr"]').focus();
        }, 10);
    }

    $scope.back = function () {
        $('#ModelAddUpdate').modal('hide');
    }

    function AddNewNode(ProductCategory)
    {
        debugger;
        $scope.ProductCategoryParent = ProductCategory;
        $scope.open(null);
    }

    function EditNode(ProductCategory) {
        debugger;
        $scope.ProductCategory = ProductCategory;
        $scope.ProductCategory.parent = null;
        $scope.ProductCategory.children = null;
        $scope.open(ProductCategory);
    }


    $scope.Delete = function (ProductCategoryID) {
        debugger;
        $scope.action = 'delete';
        $scope.ProductCategory = {ID :""};
        $scope.ProductCategory.ID = ProductCategoryID;
        $scope.ProductCategory.IsDeleted = true;
        $scope.save();
    }



    }





