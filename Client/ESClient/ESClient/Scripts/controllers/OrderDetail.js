(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("OrderDetailManage", function ($scope, $http) {
        $http.get(url + 'api/admin/orderformdetail/all').
          success(function (data, status, headers, config) {
              $scope.orders = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditOrderDetail", function ($scope, $http) {

        $scope.init = function (ids) {

            $http.get(url + 'api/admin/orderformdetail/byID/' + ids).
            success(function (data, status, headers, config) {
                $scope.DonHang = data.DONHANG;
                $scope.SanPham = data.SANPHAM;
                $scope.SoLuong = data.SOLUONG;
                $scope.Tien = data.THANHTIEN;
                $scope.xoa = data.DAXOA;
            }).
          error(function (data, status, headers, config) {
              alert("error");
          });
        }

        $scope.capnhat = function (DonHang, SanPham, SoLuong, Tien, xoa) {

            var data = { "DONHANG": DonHang, "SANPHAM": SanPham, "SOLUONG": SoLuong, "THANHTIEN": Tien, "DAXOA": xoa };

           $http.put(url + 'api/admin/orderformdetail/update', data).
               success(function(data, status, headers, config) {
                   alert('Cập nhật chi tiết đơn hàng thành công');
               });
        }
    });

    app.controller("AddrderDetail", function ($scope, $http) {

        $scope.TaoMoi = function (DonHang, SanPham, SoLuong, Tien) {
           
                var data = { "DONHANG": DonHang, "SANPHAM": SanPham, "SOLUONG": SoLuong, "THANHTIEN": Tien, "DAXOA": false };

                $http.post(url + 'api/admin/orderformdetail/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm đơn hàng thành công');
                    });
           
        }
    });

})();