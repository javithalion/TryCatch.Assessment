(function () {

    //init app
    var app = angular.module('demoShop');

    //new dependencies for this controller
    app.requires.push('ui.bootstrap');

    //shopCatalogController
    app.controller('cartController', ['$rootScope', '$scope', '$http', 'cartService', '$modal', '$localStorage', '$sessionStorage',
    function ($rootScope, $scope, $http, cartService, $modal, $localStorage, $sessionStorage) {

        $scope.currentOrderLines = $sessionStorage.productsAddedToCart;

        $scope.deleteOrderLine = function (orderLine) {

            var modal = $modal.open({
                templateUrl: 'DeleteOrderLineConfirmation',
                size: 'sm'
            });

            modal.result.then(function () {
                for (var index in $scope.currentOrderLines) {
                    if ($scope.currentOrderLines[index].$$hashKey == orderLine.$$hashKey) {
                        $scope.currentOrderLines.splice(index, 1);
                    }
                }
            });
        };

        $scope.getCartNumberOfProducts = function () {
            var total = 0;
            $.each($sessionStorage.productsAddedToCart, function (index) {
                total += $sessionStorage.productsAddedToCart[index].amount;
            });
            return total;
        };

        $scope.getCartSubtotal = function () {
            var subTotal = 0;
            $.each($sessionStorage.productsAddedToCart, function (index) {
                subTotal += ($sessionStorage.productsAddedToCart[index].amount * $sessionStorage.productsAddedToCart[index].product.Price);
            });
            return subTotal;
        };

        $scope.cleanCart = function () {
            $sessionStorage.productsAddedToCart = [];
        };

        $scope.getTotalForOrderLine = function (orderLine) {
            return cartService.getTotalForOrderLine(orderLine);
        };

        $scope.getSubTotalForOrder = function () {
            return cartService.calculateOrderSubTotal($scope.currentOrderLines);
        };

        $scope.getVatForOrder = function (orderLine) {
            return cartService.calculateOrderVat($scope.currentOrderLines);
        };

        $scope.getTotalForOrder = function (orderLine) {
            return cartService.calculateOrderFinalPrice($scope.currentOrderLines);
        };


    }]);

})();