(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("OrderManage", function ($scope, $http) {
        $http.get(url + 'api/admin/orderform/all').
          success(function (data, status, headers, config) {
              $scope.orders = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditOrder", function ($scope, $http) {

        $scope.init = function (ids) {

            $http.get(url + 'api/admin/orderform/byID/' + ids).
            success(function (data, status, headers, config) {
                $scope.Ma = data.MA;
                $scope.Tien = data.TONGTIEN;
                $scope.NgayDat = data.NGAYDATHANG;
                $scope.NgayNhan = data.NGAYNHANHANG;
                $scope.Ten = data.TENNGUOINHAN;
                $scope.DiaChi = data.DIACHINHAN;
                $scope.DienThoai = data.DIENTHOAINGUOINHAN;
                $scope.TrangThai = data.TRANGTHAI;
                $scope.xoa = data.DAXOA;
            }).
          error(function (data, status, headers, config) {
              alert("error");
          });
        }

        $scope.capnhat = function (Tien, NgayDat, NgayNhan, Ten, DiaChi, DienThoai, TrangThai,xoa) {
            if (Ten) {

                var data = { "MA": $scope.Ma, "TONGTIEN": Tien, "NGAYDATHANG": NgayDat, "NGAYNHANHANG": NgayNhan, "TENNGUOINHAN": Ten, "DIACHINHAN": DiaChi, "DIENTHOAINGUOINHAN": DienThoai, "TRANGTHAI": TrangThai, "DAXOA": xoa };

                $http.put(url + 'api/admin/orderform/update', data).
                    success(function (data, status, headers, config) {
                        alert('Cập nhật đơn hàng thành công');
                   
          });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

    app.controller("CreateOrder", function ($scope, $http) {

        $scope.TaoMoi = function (Ma, Tien, NgayDat, NgayNhan, Ten, DiaChi, DienThoai, TrangThai) {
            if (Ten) {

                var data = { "MA": Ma, "TONGTIEN": Tien, "NGAYDATHANG": NgayDat, "NGAYNHANHANG": NgayNhan, "TENNGUOINHAN": Ten, "DIACHINHAN": DiaChi, "DIENTHOAINGUOINHAN": DienThoai, "TRANGTHAI": TrangThai, "DAXOA": false };

                $http.post(url + 'api/admin/orderform/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm đơn hàng thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

})();