(function () {
    var app = angular.module('myapp', []);
    var url = "http://localhost:7009/";

    app.controller("ProductManage", function ($scope, $http) {
        $http.get(url + 'api/Manufactory/AllManu').
          success(function (data, status, headers, config) {
              $scope.products = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditProduct", function ($scope, $http) {

        $scope.init = function (Ma) {

            $http.get(url + 'api/Manufactory/ByID/' + Ma).
          success(function (data, status, headers, config) {
              $scope.Ma = data.MA;
              $scope.Ten = data.TEN;
              $scope.xoa = data.DAXOA;
          })
        }

        $scope.capnhat = function (Ten, xoa) {
            if (Ten) {

                var data = { "MA": $scope.Ma, "TEN": Ten, "DAXOA": xoa };

                $http.put(url + 'api/admin/manufactory/update', data).
                    success(function (data, status, headers, config) {
                        alert('Cập nhật nhà sản xuất thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

    app.controller("CreateManufactory", function ($scope, $http) {

        $scope.Tao = function (Ten) {
            if (Ten) {

                var data = { "TEN": Ten, "DAXOA": false };

                $http.post(url + 'api/admin/manufactory/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm nhà sản xuất thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }



    });

})();