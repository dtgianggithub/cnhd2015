(function () {
    var app = angular.module('myapp', []);
    var url = "http://apicnhd.somee.com/";

    app.controller("SupportManage", function ($scope, $http) {
        $http.get(url + 'api/admin/supportonline/all').
          success(function (data, status, headers, config) {
              $scope.supports = data;

          }).
          error(function (data, status, headers, config) {
              alert("error");
          });
    });

    app.controller("EditSupport", function ($scope, $http) {

        $scope.init = function (ids) {

            $http.get(url + 'api/admin/supportonline/byID/' + ids).
            success(function (data, status, headers, config) {
                $scope.Ma = data.MA;
                $scope.Skype = data.SKYPE;
                $scope.Ten = data.TEN;
                $scope.xoa = data.DAXOA;
            }).
          error(function (data, status, headers, config) {
              alert("error");
          });
        }

        $scope.capnhat = function (Ma, Skype, Ten, xoa) {
           
                var data = { "MA": Ma, "SKYPE": Skype, "TEN": Ten, "DAXOA": xoa };

                $http.put(url + 'api/admin/supportonline/update', data).
                    success(function (data, status, headers, config) {
                        alert('Cập nhật hỗ trợ thành công');
                    }).
          error(function (data, status, headers, config) {
              alert("error");
          });
           
        }
    });

    app.controller("CreateSupport", function ($scope, $http) {

        $scope.TaoMoi = function (Ma, Skype, Ten) {
            if (Ten) {

                var data = { "MA": Ma, "SKYPE": Skype, "TEN": Ten, "DAXOA": false };

                $http.post(url + 'api/admin/supportonline/add', data).
                    success(function (data, status, headers, config) {
                        alert('Thêm hỗ trợ thành công');
                    });
            } else {

                alert("Vui lòng điền đầy đủ thông tin");
            }
        }
    });

})();