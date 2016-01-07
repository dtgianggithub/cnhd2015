(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("PromotionManage", function ($scope, $http) {
        $http.get(url + 'api/admin/promotion/all').
          success(function (data, status, headers, config) {
              $scope.promotions = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditPromotion", function ($scope, $http) {

        $scope.init = function (ids) {

            $http.get(url + 'api/admin/promotion/byID/' + ids).
            success(function (data, status, headers, config) {
                $scope.Ma = data.MA;
                $scope.NgayBatDau = data.NGAYBATDAU;
                $scope.NgayKetThuc = data.NGAYKETTHUC;
                $scope.NoiDung = data.NOIDUNG;
                $scope.xoa = data.DAXOA;
          
          });
        }

        $scope.capnhat = function capnhat( NgayBatDau, NgayKetThuc, NoiDung, xoa) {
            

            var data = { "MA": $scope.Ma, "NGAYBATDAU": NgayBatDau, "NGAYKETTHUC": NgayKetThuc, "NOIDUNG": NoiDung, "DAXOA": xoa };

            $http.put(url + 'api/admin/promotion/update', data).
                success(function(data, status, headers, config) {
                    alert('Cập nhật khuyến mãi thành công');
                });

        }
    });

    app.controller("CreatePromotion", function ($scope, $http) {

        $scope.TaoMoi = function (NgayBatDau, NgayKetThuc, NoiDung) {
            if (Ma) {
                var data = { "NGAYBATDAU": NgayBatDau, "NGAYKETTHUC": NgayKetThuc, "NOIDUNG": NoiDung, "DAXOA": false };

                $http.post(url + 'api/admin/promotion/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm khuyễn mãi thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

})();