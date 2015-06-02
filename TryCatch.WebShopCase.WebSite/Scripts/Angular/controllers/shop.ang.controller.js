(function () {
    //init app
    var app = angular.module('demoShop');

    //new dependencies for this controller
    app.requires.push('ui.bootstrap');

    //shopCatalogController
    app.controller('shopCatalogController', ['$scope', '$http', '$modal', 'productService',
    function ($scope, $http, $modal, productService) {

        //--------------------------Variables
        $scope.products = null;
        $scope.productsAddedToCart = [];
        $scope.errorMessage = null;

        $scope.itemsPerPage = 10;
        $scope.pagingInfo = {
            page: 1,
            itemsPerPage: $scope.itemsPerPage,
            totalItems: 0
        };
        //-------------------------- Variables end


        //-------------------------- General

        //Refresh the current list of approvers
        $scope.refreshProductList = function () {
            productService.getAll($scope.pagingInfo).then(function (response) {
                var parsedResponse = JSON.parse(response)
                $scope.products = JSON.parse(parsedResponse.data);
                $scope.pagingInfo.totalItems = parsedResponse.count;
            },
             function (err) {
                 $scope.errorMessage = err.statusMessage || err;
             });
        };

        $scope.addToCart = function (product) {

            //check if is a new entry or a product already added
            var found = false;
            for (var i = 0; i < $scope.productsAddedToCart.length; i++) {

                if ($scope.productsAddedToCart[i].product.Id == product.Id) {
                    $scope.productsAddedToCart[i].amount++;
                    found = true;
                }
            }

            //if this product was not found add it
            if (!found) {
                $scope.productsAddedToCart.push({ "amount": 1, "product": product });
            }            
        };

        $scope.getFinalPrice = function (product) {

            return productService.calculateFinalPrice(product);
        }

        $scope.showProductDetailsPopUp = function (element) {

            $scope.selectedProductForDetails = element;            
            var modalInstance = $modal.open({
                templateUrl: 'ProductDetailsPopUp',
                size: 'lg',
                scope: $scope                
            });
        }

        $scope.getCartSubtotal = function () {
            var subTotal = 0;
            $.each($scope.productsAddedToCart, function (index) {
                subTotal += ($scope.productsAddedToCart[index].amount * $scope.productsAddedToCart[index].product.Price);
            });
            return subTotal;
        };

        //Initial load
        $scope.init = function () {
            //Initial product load           
            $scope.refreshProductList();
        };

        //-------------------------- General end

        //-------------------------- Pagination

        $scope.setPage = function (pageNo) {
            $scope.pagingInfo.page = pageNo;
        };

        $scope.pageChanged = function () {
            $scope.refreshProductList();
        };

        //-------------------------- Pagination end


        $scope.init();

    }]);

})();