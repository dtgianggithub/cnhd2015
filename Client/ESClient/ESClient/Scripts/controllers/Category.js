(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("CategoryManage", function ($scope, $http) {
        $http.get(url + 'api/admin/typeproduct/all').
          success(function (data, status, headers, config) {
              $scope.abc = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditCategory", function ($scope, $http) {

        $scope.init = function (Ma) {

            $http.get(url + 'api/admin/typeproduct/byID/' + Ma).
          success(function (data, status, headers, config) {
              $scope.Ma = data.MA;
              $scope.Ten = data.TEN;
              $scope.xoa = data.DAXOA;
          })
        }

        $scope.capnhat = function (Ten, xoa) {
            if (Ten) {

                var data = { "MA": $scope.Ma, "TEN": Ten, "DAXOA": xoa };

                $http.put(url + 'api/admin/typeproduct/update', data).
                    success(function (data, status, headers, config) {
                        alert('Cập nhật loại sản phẩm thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

    app.controller("CreateCategory", function ($scope, $http) {

        $scope.Tao = function (Ten) {
            if (Ten) {

                var data = { "TEN": Ten, "DAXOA": false };

                $http.post(url + 'api/admin/typeproduct/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm danh mục sản phẩm thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

})();