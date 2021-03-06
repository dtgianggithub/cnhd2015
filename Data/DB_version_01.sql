﻿DROP DATABASE ESDB
create database ESDB
go
use ESDB
go

-- 1. Bảng sản phẩm
create table SANPHAM
(
	MA varchar(7) primary key,
	TEN	nvarchar(256),
	DONGIABAN	float,
	HINHANH varchar(max), --đường link hình ảnh
	MOTA nvarchar(max), -- mô tả thông số kỹ thuật của sản phẩm(tự quy định điều kiện để tách chuỗi khi cần)
	SOLUONG int,
	LOAISANPHAM int, -- khóa ngoại tới bảng LOẠI SẢN PHẨM
	SANPHAMMOI bit, -- 
	NHASANXUAT int, -- khóa ngoại tới bảng NHÀ SẢN XUẤT
	SANPHAMBANCHAY bit, --
	DAXOA bit, -- đã xóa hay không, true là đã xóa
	MAKHUYENMAI INT -- có đang khuyễn mãi hay không
)
-- 2. Bảng Nhà Sản Xuất 
create table NHASANXUAT
(
	MA int primary key, 
	TEN	 nvarchar(256),
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)

-- 3. Bảng Nhà Sản Xuất 
create table KHUYENMAI
(
	MA int primary key identity(1,1), 
	NGAYBATDAU date,
	NGAYKETTHUC date,
	NOIDUNG int, -- Số phần trăm khuyến mãi
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)


-- 4. Bảng loại sản phẩm
create table LOAISANPHAM
(
	MA int primary key, 
	TEN	 nvarchar(256),
	DAXOA bit, -- đã xóa hay không, false là có
)


-- 5. Bảng đơn hàng
create table DONHANG
(
	MA varchar(8) primary key,
	TONGTIEN float,
	NGAYDATHANG date,
	NGAYNHANHANG date, 
	TENNGUOINHAN nvarchar(50), 
	DIACHINHAN nvarchar(256),
	DIENTHOAINGUOINHAN varchar(12),
	TRANGTHAI int, -- khóa ngoại
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)

-- 6. Bảng trạng thái đơn hàng
create table TRANGTHAIDONHANG
(
	MA int primary key identity(1,1),
	TINHTRANG nvarchar(50), -- đã giao hàng, đang giao hàng, lỗi , ....
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)

-- 7. Bảng chi tiết đơn hàng
create table CHITIETDONHANG
(
	DONHANG varchar(8) , -- khóa ngoại tới bảng DONHANG
	SANPHAM varchar(7), -- khóa ngoại tới bảng sản phẩm
	SOLUONG int, 
	THANHTIEN float,
	DAXOA bit, -- đã xóa hay không, true là đã xóa
	primary key(DONHANG, SANPHAM)
)

-- 8. Bảng thành viên
create table THANHVIEN
(
	MA int primary key identity(1,1), -- khóa chính là khóa ngoại tới bảng THANHVIEN
	TENDANGNHAP varchar(30),
	MATKHAU nvarchar(max), 
	TEN nvarchar(50),
	DIACHI nvarchar(256),
	DIENTHOAI varchar(12),
	EMAIL varchar(50),
	THONGTINMOTA nvarchar(256),
	ACCESSTOKEN varchar(256),
	LOAITHANHVIEN int ,
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)

--9. Bảng Thanh Toán Online
create table THANHTOANONLINE
(
	MA int primary key, 
	EMAIL nvarchar(256),
	MATHANHVIEN int,
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)

--10. BẢNG LOẠI THÀNH VIÊN
create table LOAITHANHVIEN
(
	MA int primary key,
	TENLOAI NVARCHAR(30), --Gồm: 1:Thành viên ca nhan ; 2:Thành viên công ty 3:ThanhVienQuanLy 
	DAXOA bit, -- đã xóa hay không, true là đã xóa
)



-- ------------------ KHÓA NGOẠI -------------------------------
--Bảng THANHTOANONLINE
alter table THANHTOANONLINE add constraint FK_THANHTOANONLINE_THANHVIEN foreign key(MATHANHVIEN) references THANHVIEN(MA)

-- bảng SANPHAM
alter table SANPHAM add constraint FK_SANPHAM_LOAISANPHAM foreign key(LOAISANPHAM) references LOAISANPHAM(MA)
alter table SANPHAM add constraint FK_SANPHAM_NHASANXUAT foreign key(NHASANXUAT) references NHASANXUAT(MA)
alter table SANPHAM add constraint FK_SANPHAM_KHUYENMAI foreign key(MAKHUYENMAI) references KHUYENMAI(MA)


-- bảng DONHANG
alter table DONHANG add constraint FK_DONHANG_TRANGTHAIDONHANG foreign key(TRANGTHAI) references TRANGTHAIDONHANG(MA)

-- bảng CHITIETDONHANG
alter table CHITIETDONHANG add constraint FK_CHITIETDONHANG_DONHANG foreign key(DONHANG) references DONHANG(MA)
alter table CHITIETDONHANG add constraint FK_CHITIETDONHANG_SANPHAM foreign key(SANPHAM) references SANPHAM(MA)


---BẢNG THANHVIEN
alter table THANHVIEN add constraint FK_THANHVIEN_LOAITHANHVIEN foreign key(LOAITHANHVIEN) references LOAITHANHVIEN(MA)

---DỮ LIỆU
-- 4. Bảng loại sản phẩm
INSERT INTO LOAISANPHAM(MA,TEN,DAXOA) VALUES (1,N'Laptop',0)
INSERT INTO LOAISANPHAM(MA,TEN,DAXOA) VALUES (2,N'Điện Thoại',0)
INSERT INTO LOAISANPHAM(MA,TEN,DAXOA) VALUES (3,N'Phụ Kiện',0)



-- 3. Bảng Khuyen Mai
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-21','2015-12-30',5,0)
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-22','2015-12-30',6,0)
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-21','2015-12-30',7,0)
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-22','2015-12-30',8,0)
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-23','2015-12-30',2,0)
INSERT INTO KHUYENMAI(NGAYBATDAU,NGAYKETTHUC,NOIDUNG,DAXOA) VALUES ('2015-11-22','2015-12-30',3,0)
-- 2. Bảng Nhà Sản Xuất 
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (1,N'ASUS',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (2,N'ACER',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (3,N'TOSHINBA',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (4,N'DELL',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (5,N'APPLE',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (6,N'SAMSUNG',0)
INSERT INTO NHASANXUAT(MA,TEN,DAXOA) VALUES (7,N'CANON',0)
--1. Sản Phẩm

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L001',N'Laptop Asus K551LA-XX317H (I5-4210U) (Bạc)',13500000,N'L001.jpg',N'- CPU:Intel Core i5 4210U 1.7GHz , 3M
- RAM:DDRAM 4GB/1600 Onboard + 2GB/1600 
- Đĩa cứng:HDD 500GB SATA3 5400 rpm
- Đồ họa:Intel HD 4400
- Đĩa quang:DVD RW
- USB:Card Reader 2.1 , USB 3.0
- Màn hình:15.6" HD LED , HDMI , Webcam
- Kết nối:LAN 10/100/1000 , Wireless N , Bluetooth
- Trọng lượng:Weight 2.4Kg 
- Pin:Battery 50Wh
- HDH:OS Windows 8.1 Single Language 64Bit
- Bảo hành:2 năm (Pin BH 1 năm & Adaptor 2 năm) ',120,1,1,1,1,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L002',N'Laptop Asus K451LA-WX147H (I3-4030U) (Đen)',11000000,N'L002.png',N'- CPU:Intel Core i3 4030U 1.9GHz , 3M
- RAM:DDRAM 1x4GB/1600 Onboard
- Đĩa cứng:HDD 500GB SATA 5400rpm
- Đồ họa:Intel HD 4400
- Đĩa quang:DVD RW
- USB:USB 3.0
- Màn hình:14.0" HD LED , HDMI , Webcam
- Kết nối:LAN 10/100/1000 , Wireless N , Bluetooth 4.0
- Trọng lượng:Weight 2.0Kg 
- Pin:Battery 4 Cell (46Wh)
- HDH:OS Windows 8.1 Single Language 64 Bit
- Bảo hành:2 năm (Pin 1 năm & Adaptor 2 năm)',300,1,1,1,1,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L003',N'Laptop Asus K551LA-XX315H (I3-4030U) (Bạc)',11000000,N'L003.jpg',N'- CPU:Intel Core i3 4030U 1.9GHz , 3M
- RAM:DDRAM 4GB/1600 Onboard  upto 12GB
- Đĩa cứng:HDD 1.0TB SATA 5400 rpm
- Đồ họa:Intel HD 4400
- Đĩa quang:DVD RW
- USB:Card Reader , USB 2.0, 2x USB 3.0
- Màn hình:15.6" HD LED , HDMI , Webcam
- Kết nối:LAN 10/100/1000 , Wireless N , Bluetooth
- Trọng lượng:Weight 2.4Kg 
- Pin:Battery 50Wh
- HDH:OS Windows 8.1 Single Language 64Bit (upto Windows 10)
- Bảo hành:2 năm (Pin BH 1 năm & Adaptor 2 năm)',300,1,1,1,1,0,2)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L004',N'Máy xách tay Laptop Asus X454LA-WX424D (I5-5200U) (Đen)',11680000,N'L004.jpg',N'- CPU:Intel Core i5 5200U 2.2GHz , 3M
- RAM:DDRAM 4GB/DDR3L 1600 onboard
- Đĩa cứng:HDD 500GB 5400rpm
- Đồ họa:Intel HD Graphics 5500
- Đĩa quang:DVD RW
- USB:Reader , USB 3.0, 2xUSB 2.0
- Màn hình:14" HD Led (1366x768) , VGA, HDMI , Webcam
- Kết nối:Lan 1G , Wifi , No Bluetooth
- Trọng lượng:Weight 2.1kg 
- Pin:Battery 2Cell
- HDH:OS Option
- Bảo hành:24 tháng',350,1,1,1,0,0,2)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L005',N'Laptop Asus P550LDV-XO1025H (I5-4210U) (Đen)',11750000,N'L005.jpg',N'- CPU:Intel Core i5 5200U 2.2GHz , 3M
 - RAM:DDRAM 4GB/1600 onboard 
 - Đĩa cứng:HDD 500GB 5400rpm
 - Đồ họa:AMD Radeon R5 M230 1GB hoặc Intel HD Graphics 5500
 - Đĩa quang:DVD RW
 - USB:Card Reader , USB 2.0, 2xUSB 3.0
 - Màn hình:15.6" HD Led (1366x768) , D Sub , HDMI
 - Kết nối:LAN 10/100 , Wireless 
 - Trọng lượng:Weight 2.4Kg 
 - Pin:Battery 37WHrs
 - HDH:OS Option
 - Bảo hành:2 năm ',230,1,1,1,0,0,3)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L006',N'Laptop Acer V3-572G-70WY (002) (Bạc)',16000000,N'L006.png',N'- CPU:Intel Core i7 4510U 2.0GHz , 4M
- RAM:DDRAM 4GB/1600 
- Đĩa cứng:HDD 500GB SATA 5400rpm
- Đồ họa:NVIDIA GF GT 840M 2GB DDR3 hoặc Intel HD 4400
- Đĩa quang:DVD RW
- USB:Card Reader , USB 3.0
- Màn hình:15.6" HD LED , HDMI ,  Webcam
- Kết nối:10/100/1000 ,  Wireless N , Bluetooth 4.0
- Trọng lượng:Weight 2.55Kg 
- Pin:Battery 5000mAh
- HDH:OS Linpus Linux
- Bảo hành:12 tháng',340,1,1,2,0,0,2)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L007',N'Laptop Acer E5-771G-501W (001) (Iron)',11900000,N'L007.jpg',N'- CPU:Intel Core i5 5200U 2.2GHz , 3M
- RAM:DDRAM 4GB/1600 
- Đĩa cứng:HDD 500GB SATA 5400rpm
- Đồ họa:NVIDIA GF 820M 2GB GDDR3 hoặc Intel HD 5500
- Đĩa quang:DVD RW
- USB:Card Reader , USB 3.0
- Màn hình:17.3" HD LED , HDMI , Webcam
- Kết nối:LAN 10/100/1000 , Wireless , Bluetooth
- Trọng lượng:Weight 3.0Kg 
- Pin:Battery 3270mAh
- HDH:OS Option
- Bảo hành:1 năm',500,1,0,2,1,0,2)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'L008',N'Máy xách tay Laptop Acer Nitro VN7-571G-58CT (001)',17800000,N'L008.jpg',N'- CPU:Intel Core i5 5200U 2.20GHz , 3M
- RAM:DDRAM 1x4GB/DDR3L 1600
- Đĩa cứng:HDD 1TB 5400rpm
- Đồ họa:NVIDIA GF 850M 4GB VDDR3 hoặc Intel HD 5500
- Đĩa quang:DVD RW
- USB:Card Reader , 3x USB 3.0
- Màn hình:15.6" IPS FHD Led (1920x1080) , HDMI
- Kết nối:Lan 10/100/1000 , Wireless N , Bluetooth 4.0
- Trọng lượng:Weight 2.4Kg 
- Pin:Battery 4605mAh
- HDH:OS Linux
- Bảo hành:1 năm',390,1,1,2,0,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'DT001',N'Điện thoại iPhone 6 16GB',17200000,N'DT001.jpg',N'- Màn hình:Retina HD, 4.7", 1334 x 750
- CPU:Apple A8, 2 nhân, 1.4 GHz
- RAM:1 GB
- Hệ điều hành:iOS 8.0
- Camera chính:8.0 MP, Quay phim FullHD 1080p@60fps
- Camera phụ:1.2 MP
- Bộ nhớ trong:16 GB
- Thẻ nhớ ngoài:Không
- Dung lượng pin:1810 mAh',400,2,0,5,1,0,2)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'DT002',N'ĐTDĐ Iphone 5S (Gold) 16GB',13300000,N'DT002.png',N'- Màn hình:Led backlit TFT 16 triệu màu 4"
- CPU:Dual Core 1.3GHz
- Ram:1GB
- Hệ điều hành:iOS v7
- Camera chính:8.0MP,Quay phim HD1080p
- Cảm ứng:điện dung đa điểm, Mặt kính chống trầy xước
- Bộ nhớ trong:16GB
- Kết nối:3G, Wifi, EDGE, GPRS tốc độ cao, định vị toàn cầu
- Hỗ trợ:mạng xã hội, la bàn số, google maps,loại bỏ tiếng ồn với micro chuyên dụng.',420,2,1,5,1,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'DT003',N'Điện thoại Samsung A800F (Galaxy A8) (Trắng)',11000000,N'DT003.jpg',N'- Màn hình:Full HD, 5.7", 1080 x 1920 pixels
- CPU:Exynos 5430, 8 nhân
- RAM:2 GB
- Hệ điều hành:Android 5.1
- SIM:2 SIM 2 sóng
- Camera:16 MP, Quay phim FullHD 1080p@30fps
- Bộ nhớ trong:32 GB
- Thẻ nhớ ngoài:128 GB
- Dung lượng pin:3050 mAh',380,2,1,6,1,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'DT004',N'ĐT Samsung SM G920F (Galaxy S6) (Đen)',15590000,N'DT004.jpg',N'- Màn hình:Quad HD, 5.1", 1440 x 2560 pixels
- CPU:Exynos 7420, 8 nhân, 4x1.5GHz Cortex A53 & 4x2.1 GHz Cortex A57
- RAM:3GB
- Hệ điều hành:Android 5.0 (Lollipop)
- Camera chính:16 MP, Quay phim 4K 2160p@30fps
- Camera phụ:5 MP
- Bộ nhớ trong:32 GB
- Dung lượng pin:2550mAh',200,2,0,6,0,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'DT005',N'Samsung SM-N920C (Galaxy Note 5) (Vàng)',18090000,N'DT005.jpg',N'- Màn hình:cảm ứng Super AMOLED 5.7" Quad HD(1440x2560)
- CPU:Exynos 7420 Octa Core ( 2.1GHz Quad + 1.5GHz Quad)
- Ram:4GB
- Hệ điều hành:Android 5.1
- Bộ nhớ trong:32GB
- Thẻ nhớ ngoài:Không
- Camera trước:5MP
- Camera sau:16MP
- Kết nối:Wifi AC, Bluetooth, 3G, 4G,A GPS và GLONASS, NFC
- Pin:3000mAh
- Bảo hành:12 tháng (Phụ kiện bảo hành 6 tháng)',250,2,0,6,0,0,1)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'PK001',N'Tai nghe Bluetooth Elecom LBT-HS20MPWH',880000,N'PK001.jpg',N'- TênSP:Tai nghe Bluetooth v4.0
- Chức năng:Nghe nhạc và đàm thoại cho Smartphone
- Kết nối:Hỗ trợ kết nối với máy tính cá nhân, iPhone
- Thời gian chờ:120h
- Thời gian sử dụng:Liên tục gọi 6h, nghe nhạc 5h
- Sạc:Sạc micro USB, thời gian sạc 2h, hiển thị mức pin
- Bảo hành:12 tháng',300,3,0,5,1,0,3)

INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'PK002',N'Máy in Canon LBP 6030w',3290000,N'PK002.jpg',N'- TênSP:In Laser A4
- Độ phân giải:600x600 dpi
- Kết nối:USB2.0, Wifi 802.11b/g/n
- Bộ nhớ:32MB
- Tốc độ in:18 trang/ phút
- Khay giấy:150 tờ
- Mực:cartridge 325',300,3,0,7,1,0,1)
INSERT INTO SANPHAM(MA,TEN,DONGIABAN,HINHANH,MOTA,SOLUONG,LOAISANPHAM,SANPHAMMOI,NHASANXUAT,SANPHAMBANCHAY,DAXOA,MAKHUYENMAI) VALUES (N'PK003',N'Máy in Canon AIO MF3010 AE',3790000,N'PK003.jpg',N'- TênSP:In, copy Laser
- Scan  khổ:A4
- Độ phân giải:1.200x600 dpi
- Kết nối:USB2.0
- Bộ nhớ:64Mb
- Tốc độ in:18ppm
- Mực cartridge:325
- Bảo hành:12 tháng',320,3,0,7,1,0,2)

-- 6. Bảng trạng thái đơn hàng
INSERT INTO TRANGTHAIDONHANG(TINHTRANG,DAXOA) VALUES (N'ĐÃ GIAO',0)
INSERT INTO TRANGTHAIDONHANG(TINHTRANG,DAXOA) VALUES (N'ĐANG GIAO',0)
INSERT INTO TRANGTHAIDONHANG(TINHTRANG,DAXOA) VALUES (N'HỦY ĐƠN HÀNG',0)
-- 5. Bảng đơn hàng


--CHITIETDONHANG

--10. BẢNG LOẠI THÀNH VIÊN
INSERT INTO LOAITHANHVIEN VALUES (1,N'Người dùng',0)
INSERT INTO LOAITHANHVIEN VALUES (2,N'Quản lý',0)
--THANHVIEN




