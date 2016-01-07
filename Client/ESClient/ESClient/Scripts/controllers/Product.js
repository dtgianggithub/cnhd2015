(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("ProductManage", function ($scope, $http) {
        $http.get(url + 'api/admin/product/all').
          success(function (data, status, headers, config) {
              $scope.products = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditProduct", function ($scope, $http) {

        $scope.init = function (ids) {

           $http.get(url + 'api/admin/typeproduct/all').
           success(function (data, status, headers, config) {
           $scope.typeproduct = data;
          
          
           });

            $http.get(url + 'api/admin/manufactory/all').
          success(function (data, status, headers, config) {
              $scope.typemaufactory= data;
            

          
          });

            $http.get(url + 'api/admin/product/byID/' + ids).
            success(function (data, status, headers, config) {
              $scope.Ma = data.MA;
              $scope.Ten = data.TEN;
              $scope.Gia = data.DONGIABAN;
              $scope.Hinh = data.HINHANH;
              $scope.MoTa = data.MOTA;
              $scope.SoLuong = data.SOLUONG;
              $scope.NSX = data.TENNHASANXUAT;
              $scope.Moi = data.SANPHAMMOI;
              $scope.banchay = data.SANPHAMBANCHAY;
              $scope.LSP = data.TENLOAISANPHAM;
              $scope.KhuyenMai = data.MAKHUYENMAI;
              $scope.xoa = data.DAXOA;
       
          });
        }

        $scope.capnhat = function (Ma, Ten, Gia, Hinh, MoTa, SoLuong, LSP, NSX, KhuyenMai, banchay, Moi, xoa) {
            if (Ten) {

                var data = { "MA": Ma, "TEN": Ten, "DONGIABAN": Gia, "HINHANH": Hinh, "MOTA": MoTa, "SOLUONG": SoLuong, "LOAISANPHAM": 3, "SANPHAMMOI": Moi, "NHASANXUAT": 1, "SANPHAMBANCHAY": banchay, "DAXOA": xoa, "MAKHUYENMAI": KhuyenMai, "TENLOAISANPHAM": LSP, "TENNHASANXUAT": NSX };
                //var data = { "MA": 'DT001',"TENLOAISANPHAM": LSP,"TENNHASANXUAT":NSX  };
                $http.put(url + 'api/admin/product/update', data).
                    success(function (data, status, headers, config) {
                        alert('Cập nhật nhà sản xuất thành công');
                   
          });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

    app.controller("CreateProduct", function ($scope, $http) {

        $http.get(url + 'api/admin/typeproduct/all').
          success(function (data, status, headers, config) {
              $scope.typeproduct = data;
              

        
          });

        $http.get(url + 'api/admin/manufactory/all').
      success(function (data, status, headers, config) {
          $scope.typemaufactory = data;
        
     
      });

        $scope.TaoMoi = function (Ma, Ten, Gia, Hinh, MoTa, SoLuong, LSP, NSX, KhuyenMai, banchay, Moi) {
            if (Ten) {

                var data = { "MA": Ma, "TEN": Ten, "DONGIABAN": Gia, "HINHANH": Hinh, "MOTA": MoTa, "SOLUONG": SoLuong, "LOAISANPHAM": LSP, "SANPHAMMOI": Moi, "NHASANXUAT": 1, "SANPHAMBANCHAY": banchay, "DAXOA": false, "MAKHUYENMAI": KhuyenMai, "TENLOAISANPHAM": LSP, "TENNHASANXUAT": NSX };

                $http.post(url + 'api/admin/product/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm nhà sản xuất thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

})();