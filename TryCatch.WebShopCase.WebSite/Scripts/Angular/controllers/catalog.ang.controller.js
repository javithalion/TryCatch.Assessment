(function () {
    //init app
    var app = angular.module('demoShop');

    //new dependencies for this controller
    app.requires.push('ui.bootstrap');

    //shopCatalogController
    app.controller('shopCatalogController', ['$scope', '$http', '$modal', 'productService', '$localStorage', '$sessionStorage',
    function ($scope, $http, $modal, productService, $localStorage, $sessionStorage) {

        //--------------------------Variables
        $scope.products = null;
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
            for (var i = 0; i < $sessionStorage.productsAddedToCart.length; i++) {

                if ($sessionStorage.productsAddedToCart[i].product.Id == product.Id) {
                    $sessionStorage.productsAddedToCart[i].amount++;
                    found = true;
                }
            }
            //if this product was not found add it
            if (!found) {
                $sessionStorage.productsAddedToCart.push({ "amount": 1, "product": product });
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
            $.each($sessionStorage.productsAddedToCart, function (index) {
                subTotal += ($sessionStorage.productsAddedToCart[index].amount * $sessionStorage.productsAddedToCart[index].product.Price);
            });
            return subTotal;
        };

        $scope.getCartNumberOfDifferentProducts = function () {
            return $sessionStorage.productsAddedToCart.length
        }

        //Initial load
        $scope.init = function () {
            if (!$sessionStorage.productsAddedToCart)
            {
                $sessionStorage.productsAddedToCart = []
            }
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