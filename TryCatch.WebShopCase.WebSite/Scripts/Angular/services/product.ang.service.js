(function () {
    //init app
    var app = angular.module('demoShop');

    app.factory('productService', ['$http', '$q', function ($http, $q) {

        return {            
            //retrieve products from the server
            getAll: function (pagingInfo) {

                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: 'Http://localhost:49312/api/products',
                    params: pagingInfo
                })
                .error(function () {
                    deferred.reject("Unknown error");
                })
                .success(function (response) {
                    if (response.statusCode) {
                        deferred.reject(response);
                    } else {
                        deferred.resolve(response);
                    }
                });
                return deferred.promise;
            },

            calculateFinalPrice: function (product)
            {
                return (product.Price * (1 + (product.VatPercentage / 100))).toFixed(2);
            }
        };
    }]);

})();