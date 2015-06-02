(function () {
    //init app
    var app = angular.module('demoShop');

    app.factory('cartService', ['$http', '$q', function ($http, $q) {

        return {
            getTotalForOrderLine: function (orderLine) {
                return (orderLine.amount * (orderLine.product.Price * (1 + (orderLine.product.VatPercentage / 100)))).toFixed(2);
            },

            calculateOrderSubTotal: function (order) {
                var total = 0;
                $.each(order, function (index) {
                    total += order[index].amount * order[index].product.Price;
                });
                return total.toFixed(2);
            },

            calculateOrderVat: function (order) {
                var total = 0;
                $.each(order, function (index) {
                    total += order[index].product.Price * (order[index].product.VatPercentage / 100);
                });
                return total.toFixed(2);
            },

            calculateOrderFinalPrice: function (order) {
                var total = 0;
                $.each(order, function (index) {

                    total += order[index].amount * (order[index].product.Price * (1 + (order[index].product.VatPercentage / 100)));
                });
                return total.toFixed(2);
            }

        };
    }]);

})();