create database QLCUAHANGTLCIRCLEKS
go
use QLCUAHANGTLCIRCLEKS
--drop database QLCHTLCIRCLEK
go
create table NHACUNGCAP(
Manhacungcap nvarchar(10) not null primary key,  
Tennhacungcap nvarchar(30) , 
Diachi nvarchar(30) ,
Sodienthoai char(15))
go
create table SANPHAM(
MaSP nvarchar(10) not null primary key,
TenSP nvarchar(30),
GiaSP float ,
Manhacungcap nvarchar(10))
go


create table CUAHANGCIRCLEK(
MaCH nvarchar(10) not null primary key,
Diachi nvarchar(30),
Giayphep nvarchar(30))
go

create table NHANVIEN(
Manv nvarchar(10) not null primary key, 
Tennv nvarchar(30), 
NgaySinh date, 
Diachi nvarchar(30),
SDTNhanVien char(15), 
Phai char(5))
go

create table HOADON(
id int identity,
    PreFix nvarchar(3) NOT NULL default 'DH',
    MaHD  AS (PreFix + RIGHT('0000' + CAST(Id AS VARCHAR(7)), 7)) PERSISTED PRIMARY KEY,
MaKH nvarchar(10) ,
Manv nvarchar(10) ,
NgayXuat datetime,
SoTien float,
PhuongThucThanhToan nvarchar(100),
GiamGia float,
TongTien float)
go

create table KHACHHANG(
MaKH nvarchar(10) not null primary key,
TenKH nvarchar(30),
DiaChiKH nvarchar(30), 
SDTKH char(15) )
go

create table LUONG(
MaLuong nvarchar(20) not null primary key,
Manv nvarchar(10) not null,
PhuCap float,TangCa int , 
Thuong float,
NgaySinh date,
LuongCB float,
Luong float)
go

create table ChiTietHoaDon(
MaHD nvarchar(10),
MaSP nvarchar(10) not null,
SoLuong int ,
ThanhTien float, 
primary key(MaHD,MaSP))
go

create table NHAKHO(
MaSP nvarchar(10) not null,
MaCH nvarchar(10) not null ,
SoLuong int,
primary key(MaSP,MaCH)
)
go


---tạo ràng buộc
----Khóa ngọai

alter table SANPHAM
add constraint fk_sv_NHACUNGCAP  foreign key (Manhacungcap) references NHACUNGCAP(Manhacungcap) 
go

alter table NHAKHO
add constraint kk_SanPham FOREIGN KEY (MaCH) REFERENCES CUAHANGCIRCLEK(MaCH)
go

alter table HOADON
add constraint fk_sv_khachhang  FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
go

alter table NHAKHO
add constraint NK_CH_SanPham  FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
go

alter table HOADON
add constraint fk_sv_NhanVien  FOREIGN KEY (Manv) REFERENCES NHANVIEN(Manv)
go


alter table ChiTietHoaDon
add constraint fk_sv_HoaDon  FOREIGN KEY (MaHD) REFERENCES HOADON(MaHD)
go

alter table ChiTietHoaDon
add constraint fk_sv_ChiTiet   FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
go


alter table LUONG
add constraint L_NV_Manv  foreign key (Manv) references NHANVIEN(Manv)
go

------ tạo ràng buộc kiểm tra nhà kho
alter table NhaKho
add check (SoLuong >=0)

--- Thêm bảng-----
set dateformat dmy
go
--Thêm Bảng Nhân Viên
INSERT INTO NHANVIEN(Manv, Tennv, NgaySinh, Diachi, SDTNhanVien, Phai)  
VALUES   
    (N'NV01', N'Nguyễn Ngọc Khánh', '2004-03-23', N'Thủ Đức', '0957896433', N'Nu'),  
    (N'NV02', N'Tôn Thị Ngân', '2003-09-12', N'Quãng Ngãi', '0997896913', N'Nu'),  
    (N'NV03', N'Trần Văn Dõ', '2001-01-15', N'Cà Mau', '0963258710', N'Nam'),  
    (N'NV04', N'Nguyễn Thị Diệu', '1999-10-29', N'Bình Thuận', '0932758692', N'Nu'),  
    (N'NV05', N'Nguyễn Trần Khánh', '2000-02-22', N'Vũng Tàu', '0952361235', N'Nam');
 select * from NHANVIEN
 go


--Thêm bảng Lương
INSERT INTO LUONG(MaLuong,Manv,PhuCap,TangCa,Thuong,NgaySinh,LuongCB,Luong)
VALUES (N'1',N'NV01',25000,50000,100000,'12/11/2004',5000000,5850000),
	   (N'2',N'NV02',30000,50000,150000,'12/08/2003',5000000,7300000),
	   (N'3',N'NV03',85000,50000,263000,'01/06/1998',5000000,7300000),
	   (N'4',N'NV04',36000,50000,89000,'06/05/1999',5000000,8000000),
	   (N'5',N'NV05',30000,45000,90000,'09/12/2001',5000000,7000000)
select * from LUONG
go
--Thêm bảng Nhà Cung Cấp 
INSERT INTO NHACUNGCAP(Manhacungcap,Tennhacungcap,Diachi,Sodienthoai)
VALUES (N'NCC01',N'HTX Anh Đào',N'Đà Lạt',0948569825),
	   (N'NCC02',N'Head & Shoulder',N'TPHCM',0938777397),
	   (N'NCC03',N'OSI',N'Thuận An',0934799825),
	   (N'NCC04',N'Cty TNHH San Hà',N'Lâm Đồng',0345894780),
	   (N'NCC05',N'Cty TNHH Omo',N'TPHCM',0948569825)
select * from NHACUNGCAP
go
--Thêm bảng Sản Phẩm
INSERT INTO SANPHAM(MaSP,TenSP,GiaSP,Manhacungcap)
VALUES (N'SP1', N'Bánh Osi', 5000,N'NCC01'),
	   (N'SP2', N'Dầu Gội', 65000, N'NCC01'),
	   (N'SP3', N'Thịt', 120000,N'NCC03'),
	   (N'SP4', N'Rau Củ', 2000,N'NCC03'),
	   (N'SP5', N'Bột Giặt', 180000,N'NCC05'),
	   (N'SP6', N'Nước Ngọt', 10000, N'NCC02'),
	   (N'SP7', N'Sữa Hộp', 30000, N'NCC02'),
	   (N'SP8', N'Mì Gói', 5000, N'NCC01'),
       (N'SP9', N'Bánh Mì', 2000, N'NCC01'),
       (N'SP10', N'Trứng Gà', 20000, N'NCC03'),
       (N'SP11', N'Thịt Bò', 250000, N'NCC03'),
       (N'SP12', N'NcRửaChén', 15000, N'NCC04'),
       (N'SP13', N'BimBim', 35000, N'NCC05'),
       (N'SP14', N'Dầu Ăn', 45000, N'NCC05'),
       (N'SP15', N'Nước Mắm', 20000, N'NCC02'),
       (N'SP16', N'Cháo Gói', 7000, N'NCC01'),
       (N'SP17', N'Mứt Dâu', 50000, N'NCC02'),
       (N'SP18', N'NướcHoa', 12000, N'NCC02'),
       (N'SP19', N'BánhQuy', 25000, N'NCC01'),
       (N'SP20', N'KemLạnh', 10000, N'NCC03'),
       (N'SP21', N'Thịt Lợn', 150000, N'NCC03'),
       (N'SP22', N'Rau Xanh', 3000, N'NCC03'),
       (N'SP23', N'Cà Phê', 80000, N'NCC04'),
       (N'SP24', N'Trái Cây', 20000, N'NCC05'),
       (N'SP25', N'Phở Gói', 7000, N'NCC01')
select * from SANPHAM
go
delete from SANPHAM
--Thêm Bảng Khách Hàng
INSERT INTO KHACHHANG (MaKH, TenKH, DiaChiKH, SDTKH) VALUES  
    (N'KH01', N'Nguyễn Văn Quí', N'Hooc Môn', N'0359874588'),  
    (N'KH02', N'Trần Thị Phong', N'Quận 9', N'0327527480'),  
    (N'KH03', N'Nguyễn Minh Thuận', N'Linh Trung', N'045698752'),  
    (N'KH04', N'Nguyễn Thị Nhi', N'Bình Dương', N'0655893245'),  
    (N'KH05', N'Phạm Thị Thu', N'Bình Dương', N'0785441235'),  
    (N'KH06', N'Nguyễn Văn An', N'Thủ Đức', N'0901234567'),  
    (N'KH07', N'Trần Quốc Khánh', N'Gò Vấp', N'0912345678'),  
    (N'KH08', N'Lê Thị Hoa', N'Hóc Môn', N'0933456789'),  
    (N'KH09', N'Võ Văn Bình', N'Tân Bình', N'0944567890'),  
    (N'KH10', N'Ngô Thị Hương', N'Bình Thạnh', N'0965678901'),  
    (N'KH11', N'Đỗ Văn Minh', N'Tây Ninh', N'0976789012'),  
    (N'KH12', N'Hồ Thị Mai', N'Di An', N'0987890123'),  
    (N'KH13', N'Nguyễn Quốc Duy', N'Sóc Trăng', N'0998901234'),  
    (N'KH14', N'Phạm Văn Tuấn', N'Đồng Nai', N'0911234567'),  
    (N'KH15', N'Trần Thị Thanh', N'Biên Hòa', N'0922345678'),  
    (N'KH16', N'Nguyễn Hữu Phúc', N'Long An', N'0933456789'),  
    (N'KH17', N'Vũ Văn Khải', N'Hà Nội', N'0944567890'),  
    (N'KH18', N'Gia Bảo', N'Hải Phòng', N'0955678901'),  
    (N'KH19', N'Dương Thị Tâm', N'Huế', N'0966789012'),  
    (N'KH20', N'Lê Văn Sơn', N'Nha Trang', N'0977890123');
select * from KHACHHANG
go

--Thêm Bảng Hóa Đơn
--mm/dd/yyyy
INSERT INTO HOADON (MaKH, Manv, NgayXuat, SoTien, PhuongThucThanhToan, GiamGia, TongTien) VALUES  
    (N'KH01', N'NV01', '2024-01-01 10:00:00', 100000, N'Tiền mặt', 5, 95000),  
    (N'KH02', N'NV02', '2024-01-02 14:30:00', 150000, N'Thẻ tín dụng', 15, 127500),  
    (N'KH03', N'NV03', '2024-01-03 09:15:00', 200000, N'Ví điện tử', 10, 180000),  
    (N'KH04', N'NV01', '2024-01-04 11:45:00', 250000, N'Tiền mặt', 20, 200000),  
    (N'KH05', N'NV02', '2024-01-05 16:30:30', 120000, N'Thẻ tín dụng', 5, 114000),  
    (N'KH06', N'NV03', '2024-01-06 08:20:45', 300000, N'Ví điện tử', 25, 225000),  
    (N'KH07', N'NV01', '2024-01-07 12:55:15', 130000, N'Tiền mặt', 10, 117000),  
    (N'KH08', N'NV04', '2024-01-08 10:30:00', 210000, N'Thẻ tín dụng', 30, 147000),  
    (N'KH09', N'NV03', '2024-01-09 14:00:00', 180000, N'Ví điện tử', 15, 153000),  
    (N'KH10', N'NV01', '2024-01-10 09:45:00', 90000, N'Tiền mặt', 0, 90000),  
    (N'KH11', N'NV02', '2024-01-11 17:20:00', 250000, N'Thẻ tín dụng', 10, 225000),  
    (N'KH12', N'NV03', '2024-01-12 11:00:00', 120000, N'Ví điện tử', 5, 114000),  
    (N'KH13', N'NV01', '2024-01-13 14:05:00', 450000, N'Tiền mặt', 100000, 350000),  
    (N'KH14', N'NV02', '2024-01-14 15:15:00', 280000, N'Thẻ tín dụng', 30, 196000),  
    (N'KH15', N'NV03', '2024-01-15 13:45:00', 350000, N'Ví điện tử', 20, 280000),  
    (N'KH16', N'NV05', '2024-01-16 12:30:00', 600000, N'Tiền mặt', 30, 420000),  
    (N'KH17', N'NV02', '2024-01-17 09:00:00', 380000, N'Thẻ tín dụng', 15, 323000),  
    (N'KH18', N'NV03', '2024-01-18 10:10:00', 500000, N'Ví điện tử', 50, 400000),  
    (N'KH19', N'NV01', '2024-01-19 11:30:00', 70000, N'Tiền mặt', 0, 70000),  
    (N'KH20', N'NV02', '2024-01-20 16:40:00', 240000, N'Thẻ tín dụng', 20, 192000);
select * from HOADON
go


--Thêm Bảng Cửa hàng tiện lợi Circle K
INSERT INTO CUAHANGCIRCLEK(MaCH,Diachi,Giayphep)
VALUES (N'CH01',N'Linh Trung',N'Có Giấy Phép'),
	   (N'CH02',N'Gò Vấp',N'Có Giấy Phép'),
	   (N'CH03',N'Thủ Đức',N'Có Giấy Phép'),
	   (N'CH04',N'Tân Bình',N'Có Giấy Phép'),
	   (N'CH05',N'Phú Nhuận',N'Có Giấy Phép')
select * from CUAHANGCIRCLEK
go
select * from sanpham
--Thêm Bảng Nhà Kho
INSERT INTO NHAKHO(MaSP, MaCH, SoLuong)  
VALUES   
    (N'SP1', N'CH01', 20),  
    (N'SP2', N'CH02', 45),  
    (N'SP3', N'CH03', 30),  
    (N'SP4', N'CH04', 5),  
    (N'SP5', N'CH05', 15),  
    (N'SP6', N'CH01', 25),  
    (N'SP7', N'CH02', 10),  
    (N'SP8', N'CH03', 50),  
    (N'SP9', N'CH04', 8),  
    (N'SP10', N'CH05', 12),  
    (N'SP11', N'CH01', 7),  
    (N'SP12', N'CH02', 20),  
    (N'SP13', N'CH03', 18),  
    (N'SP14', N'CH04', 13),  
    (N'SP15', N'CH05', 22),  
    (N'SP16', N'CH01', 11),  
    (N'SP17', N'CH02', 33),  
    (N'SP18', N'CH03', 27),  
    (N'SP19', N'CH04', 14),  
    (N'SP20', N'CH05', 5),  
    (N'SP21', N'CH01', 9),  
    (N'SP22', N'CH02', 2),  
    (N'SP23', N'CH03', 19),  
    (N'SP24', N'CH04', 30),  
    (N'SP25', N'CH05', 12);
select * from NHAKHO
go
-- Thêm chi tiết hóa đơn 
INSERT INTO ChiTietHoaDon(MaHD, MaSP, SoLuong, ThanhTien)   
VALUES   
    ('DH01', 'SP1', 45, 225000),   -- Sản phẩm Bánh Osi  
    ('DH01', 'SP2', 3, 195000),    -- Sản phẩm Dầu Gội  
    ('DH01', 'SP4', 10, 20000),    -- Sản phẩm Rau Củ  
    ('DH02', 'SP3', 5, 600000),    -- Sản phẩm Thịt  
    ('DH02', 'SP5', 2, 360000),    -- Sản phẩm Bột Giặt  
    ('DH02', 'SP6', 15, 150000),   -- Sản phẩm Nước Ngọt  
    ('DH03', 'SP7', 1, 30000),     -- Sản phẩm Sữa Hộp  
    ('DH03', 'SP8', 4, 20000),     -- Sản phẩm Mì Gói  
    ('DH03', 'SP9', 10, 20000),    -- Sản phẩm Bánh Mì  
    ('DH04', 'SP10', 12, 240000),  -- Sản phẩm Trứng Gà  
    ('DH04', 'SP11', 3, 750000),   -- Sản phẩm Thịt Bò  
    ('DH04', 'SP12', 20, 300000);   -- Sản phẩm Nc Rửa Chén
select * from ChiTietHoaDon

go
delete from ChiTietHoaDon
select * from HOADON

go
select sp.MaSP, sp.TenSP, nk.SoLuong,sp.GiaSP, ch.Diachi from SANPHAM sp
join NHAKHO nk on sp.MaSP = nk.MaSP
join CUAHANGCIRCLEK ch on nk.MaCH = ch.MaCH
where nk.MaCH = N'CH01' and sp.Manhacungcap = N'NCC03'
go
select TenSP from SANPHAM
join ChiTietHoaDon on SANPHAM.MaSP = ChiTietHoaDon.MaSP
where ChiTietHoaDon.MaHD = 'DH00001'