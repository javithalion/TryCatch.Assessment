(function () {
    //init app
    var app = angular.module('demoShop');

    //shopCatalogController
    app.controller('shopCatalogController', ['$scope', '$http', 'productService',
    function ($scope, $http, productService) {

        //Variables
        $scope.products = null;
        $scope.productsAddedToCart = [];
        $scope.errorMessage = null;

        //Refresh the current list of approvers
        $scope.refreshProductList = function () {
            productService.getAll().then(function (products) {

                $scope.products = products;
                console.log($scope.products);
            },
             function (err) {
                 $scope.errorMessage = err.statusMessage || err;
             });
        };

        $scope.addToCart = function (product) {
            $scope.productsAddedToCart.push(product);
        };


        //Initial load
        $scope.init = function () {
            //Initial product load
            $scope.refreshProductList();
        };

        $scope.init();

    }]);

})();