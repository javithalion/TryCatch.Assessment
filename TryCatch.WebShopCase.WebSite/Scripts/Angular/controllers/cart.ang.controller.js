(function () {

    //init app
    var app = angular.module('demoShop');   

    //shopCatalogController
    app.controller('cartController', ['$rootScope', '$scope', '$http', 'cartService', '$localStorage', '$sessionStorage',
    function ($rootScope, $scope, $http, cartService, $localStorage, $sessionStorage) {

        $scope.currentOrderLines = $sessionStorage.productsAddedToCart;

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