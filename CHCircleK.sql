create database QLCHTLCIRCLEK
go
use QLCHTLCIRCLEK
--drop database QLCHTLCIRCLEK
go
create table NHACUNGCAP(
Manhacungcap nvarchar(10) not null primary key,  
Tennhacungcap nvarchar(20) , 
Diachi nvarchar(15) ,
Sodienthoai char(15))
go
create table SANPHAM(
MaSP nvarchar(10) not null primary key,
TenSP nvarchar(10),
GiaSP float ,
Manhacungcap nvarchar(10))
go


create table CUAHANGCIRCLEK(
MaCH nvarchar(10) not null primary key,
Diachi nvarchar(15),
Giayphep nvarchar(30))
go

create table NHANVIEN(
Manv nvarchar(10) not null primary key, 
Tennv nvarchar(30), 
NgaySinh date, 
Diachi nvarchar(15),
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
TenKH nvarchar(10),
DiaChiKH nvarchar(15), 
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
insert into NHANVIEN(Manv,Tennv,NgaySinh,Diachi,SDTNhanVien,Phai)
values (N'NV01',N'Ngọc Khánh','2004/03/23',N'Thủ Đức',0957896433,N'Nu'),
	   (N'NV02',N'Tơ Ngân','2003/09/12',N'Quãng Ngãi',0997896913,N'Nu'),
	   (N'NV03',N'Chú Dõ','2001/01/15',N'Cà Mau',0963258710,N'Nam'),
	   (N'NV04',N'Thị Diệu','1999/10/29',N'Bình Thuận',0932758692,N'Nu'),
	   (N'NV05',N'Trần Khánh','2000/02/22',N'Vũng Tàu',0952361235,N'Nam')
 select * from NHANVIEN
 go


--Thêm bảng Lương
insert into LUONG(MaLuong,Manv,PhuCap,TangCa,Thuong,NgaySinh,LuongCB,Luong)
values (N'1',N'NV01',25000,50000,100000,'12/11/2004',50000000,5850000),
	   (N'2',N'NV02',30000,50000,150000,'12/08/2003',50000000,7300000),
	   (N'3',N'NV03',85000,50000,263000,'01/06/1998',50000000,7300000),
	   (N'4',N'NV04',36000,50000,89000,'06/05/1999',50000000,8000000),
	   (N'5',N'NV05',30000,45000,90000,'09/12/2001',50000000,7000000)
select * from LUONG
go
--Thêm bảng Nhà Cung Cấp 
insert into NHACUNGCAP(Manhacungcap,Tennhacungcap,Diachi,Sodienthoai)
values (N'NCC01',N'HTX Anh Đào',N'Đà Lạt',0948569825),
	   (N'NCC02',N'Head & Shoulder',N'TPHCM',0938777397),
	   (N'NCC03',N'OSI',N'Thuận An',0934799825),
	   (N'NCC04',N'Cty TNHH San Hà',N'Lâm Đồng',0345894780),
	   (N'NCC05',N'Cty TNHH Omo',N'TPHCM',0948569825)
select * from NHACUNGCAP
go
--Thêm bảng Sản Phẩm
insert into SANPHAM(MaSP,TenSP,GiaSP,Manhacungcap)
values --(N'SP1', N'Bánh Osi', 5000,N'NCC01'),
	   --(N'SP2', N'Dầu Gội', 65000, N'NCC01'),
	   --(N'SP3', N'Thịt', 120000,N'NCC03'),
	   --(N'SP4', N'Rau Củ', 2000,N'NCC03'),
	   --(N'SP5', N'Bột Giặt', 180000,N'NCC05'),
	   --(N'SP6', N'Nước Ngọt', 10000, N'NCC02'),
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
insert into KHACHHANG(MaKH,TenKH,DiaChiKH,SDTKH)
values (N'KH01',N'Quí',N'Hooc Môn',0359874588),
	   (N'KH02',N'Phong',N'Quận 9',0327527480),
	   (N'KH03',N'Thuận',N'Linh Trung',045698752),
	   (N'KH04',N'Nhi',N'Bình Dương',0655893245),
	   (N'KH05',N'Thu',N'Bình Dương',0785441235)
select * from KHACHHANG
go

--Thêm Bảng Hóa Đơn
--mm/dd/yyyy
insert into HOADON(MaKH,Manv,NgayXuat,SoTien,PhuongThucThanhToan,GiamGia,TongTien)
values (N'KH02',N'NV02','11/07/2023 10:10:10', 23000, N'Ví điện tử',1,22000),
   	   (N'KH05',N'NV01','02/08/2023 15:10:35', 65000, N'Tiền mặt',25,36210),
	   (N'KH02',N'NV04','06/08/2021 5:30:55', 32900, N'Thẻ tín dụng',10,31200),
	   (N'KH04',N'NV03','12/06/2019 9:45:48', 63000, N'Tiền mặt',50,31500),
	   (N'KH01',N'NV04','04/02/2018 6:15:58', 87000, N'Ví điện tử',90,8700)
select * from HOADON
go


--Thêm Bảng Cửa hàng tiện lợi Circle K
insert into CUAHANGCIRCLEK(MaCH,Diachi,Giayphep)
values (N'CH01',N'Linh Trung',N'Có Giấy Phép'),
	   (N'CH02',N'Gò Vấp',N'Có Giấy Phép'),
	   (N'CH03',N'Thủ Đức',N'Có Giấy Phép'),
	   (N'CH04',N'Tân Bình',N'Có Giấy Phép'),
	   (N'CH05',N'Phú Nhuận',N'Có Giấy Phép')
select * from CUAHANGCIRCLEK
go
select * from sanpham
--Thêm Bảng Nhà Kho
insert into NHAKHO(MaSP,MaCH,SoLuong)
values (N'SP4',N'CH01',12),
	   (N'SP1',N'CH02',14),
	   (N'SP3',N'CH01',50),
	   (N'SP2',N'CH02',60)
select * from NHAKHO
go
-- Thêm chi tiết hóa đơn 
insert into ChiTietHoaDon(MaHD,MaSP,SoLuong,ThanhTien) 
values ('DH00001','SP1',45,87000),
		('DH00002','SP2',3,195000),
		('DH00002','SP4',45,89000)
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