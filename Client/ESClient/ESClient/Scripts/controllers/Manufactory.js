(function () {
    var app = angular.module('myapp', []);

    app.controller("ManufactoryManage", function ($scope, $http) {
        $http.get('http://localhost:7009/api/Manufactory/AllManu').
          success(function (data, status, headers, config) {
              $scope.manus = data;

          }).
          error(function (data, status, headers, config) {
              alert("erro");
          });
    });

    app.controller("EditManufactory", function ($scope, $http) {

        $scope.init = function (Ma) {

            $http.get('http://localhost:7009/api/Manufactory/ByID/' + Ma).
          success(function (data, status, headers, config) {
              $scope.HoTen = data;

          }).
          error(function (data, status, headers, config) {
              alert("erro");
          });
        }
    });
})();