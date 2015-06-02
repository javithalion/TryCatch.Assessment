(function () {
    //init app
    var app = angular.module('demoShop');

    app.factory('productService', ['$http', '$q', function ($http, $q) {

        return {            
            //retrieve products from the server
            getAll: function (jobId) {

                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: 'Http://localhost:49312/api/products'
                })
                .error(function () {
                    deferred.reject("Unknown error");
                })
                .success(function (response) {
                    if (response.statusCode) {
                        deferred.reject(response);
                    } else {
                        deferred.resolve(JSON.parse(response));
                    }
                });
                return deferred.promise;
            }            
        };
    }]);

})();