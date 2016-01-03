(function () {
   

    var app = angular.module('myapp', []);

    app.controller("ProductController", function ($scope, $http) {
        $http.get('http://apicnhd.somee.com/api/Manufactory/AllManu').
          success(function (data, status, headers, config) {
              $scope.products = data;

          }).
          error(function (data, status, headers, config) {
              alert("erro");
          });
    });
})();

