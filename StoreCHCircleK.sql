---them nhan vien
Select * from NHANVIEN

 set dateformat dmy
 go
create proc ThemNV( @Manv nvarchar(10), @Tennv nvarchar(30), @NgaySinh date, @Diachi nvarchar(15),@SDTNhanVien char(15), @Phai char(5))
as
  insert into NHANVIEN(Manv,Tennv,NgaySinh,Diachi,SDTNhanVien,Phai)
  values(@Manv,@Tennv,@NgaySinh,@Diachi,@SDTNhanVien,@Phai)
go
     exec ThemNV N'NV06',N'Hung','13/11/2004',N'Thủ Đức','0987096433',N'Nam'
go
select* from NHANVIEN
go
-- lấy toàn bộ DsNhanvien
 create proc LayDSNhanVien
  as
  select*from NHANVIEN
  -- lấy nhà cung cap 
  go
   create proc LayDSNCC
  as
  select*from NHACUNGCAP
  --xóa sinh viên 
  go
  create proc XoaDSNhanVien(@Manv nvarchar(10))
  as
 delete from NHANVIEN
 where Manv = @Manv
 -- sửa nhân viên 
 GO
CREATE PROC SuaNhanVien
	@Manv nvarchar(10), 
	@Tennv nvarchar(30), 
	@NgaySinh date, 
	@Diachi nvarchar(15),
	@SDTNhanVien char(15), 
	@Phai char(5)
	
	as
	UPDATE NHANVIEN
	SET Tennv = @Tennv,
		NgaySinh = @NgaySinh,
		Diachi= @Diachi,
		SDTNhanVien = @SDTNhanVien,
		Phai = @Phai	
	WHERE Manv = @Manv
go

exec SuaNhanVien N'NV02',N'Ha','15/06/2004',N'Thủ Đức',0987096433,N'Nam'
go
select*from NHANVIEN
---Xoa Nhan vien
go
CREATE PROCEDURE XoaNhanVien @Manv nvarchar(10)
AS
delete from NHANVIEN
where  Manv = @Manv 
go


  ---thực thi nhan vien
     go
  Create Proc LayDSKhachHang
AS
   select * from KHACHHANG
   go

  --them du lieu KHACHHANG@
 go
Create Proc ThemKHACHHANG @MaKH nvarchar(10),@TenKH nvarchar(10),@DiaChiKH nvarchar(15), @SDTKH char(15) 
as
  insert into KHACHHANG
  values(@MaKH,@TenKH ,@DiaChiKH, @SDTKH )
  go
 
  exec ThemKHACHHANG N'KH06',N'ngoan',N'Vinh Long',0359874688
  go
  select * from KHACHHANG
--cập nhập nhân viên
go
Create Proc CapNhapKHACHHANG(@MaKH nvarchar(10),@TenKH nvarchar(10),@DiaChiKH nvarchar(15), @SDTKH char(15) )
as

update KHACHHANG set MaKH = @MaKH,TenKH = @TenKH,DiaChiKH = @DiaChiKH,SDTKH =@SDTKH where MaKH = @MaKH
go
exec CapNhapKHACHHANG N'KH06',N' Ha',N'Đồng Nai',0987096433
    select * from KHACHHANG
	--Xoa khach hang
go
CREATE PROCEDURE XoaKhachHang @MaKH nvarchar(10)
AS
delete from KHACHHANG
where  MaKH = @MaKH 
go
--- TÌM KHÁCH HÀNG 
	CREATE PROC TIMKHACHHANG_THEOMA @MaKH nvarchar(10)
	as
	SELECT * FROM KHACHHANG WHERE MaKH = @MaKH;
	go
	 EXEC TIMKHACHHANG_THEOMA N'KH04'



   ---thực thi Luong
   go
  Create Proc LayDanhSachLuong
AS
   select * from LUONG
   go

  --them du lieu Luong
 go
Create Proc ThemLUONG @MaLuong nvarchar(20), @Manv nvarchar(10) ,@PhuCap float,@TangCa int , @Thuong float,@NgaySinh date,@LuongCB float,@Luong float
as
  Insert into LUONG(MaLuong,Manv,PhuCap,TangCa,Thuong,NgaySinh,LuongCB,Luong)
  values(@MaLuong,@Manv,@PhuCap,@TangCa,@Thuong,@NgaySinh,@LuongCB,@Luong)
  go
 
  exec ThemLUONG N'NV01',N'NV02',25000,50000,100000,'23/11/2004',50000000,7300000
 
--cập nhập luong
go
Create Proc CapNhapLuong(@MaLuong nvarchar(20),@Manv nvarchar(10) ,@PhuCap float,@TangCa int , @Thuong float,@NgaySinh date,@LuongCB float,@Luong float)
as

update LUONG set Manv = @Manv,PhuCap = @PhuCap,TangCa = @TangCa,Thuong =@Thuong,NgaySinh = @NgaySinh,LuongCB=@LuongCB,Luong =@Luong where MaLuong = @MaLuong
go
exec CapNhapLuong N'1',N'NV01',25000,50000,200000,'23/11/2004',50000000,8000000
  --xoa luong
  	go
CREATE PROCEDURE XoaLuong @MaLuong nvarchar(20)
AS
delete from LUONG
where  MaLuong = @MaLuong 
go

 --- TÌM Luong
	CREATE PROC TIMLUONG_THEOMA  @MaLuong nvarchar(20)
	as
	SELECT * FROM LUONG WHERE MaLuong=@MaLuong
	go
	 EXEC TIMLUONG_THEOMA N'1'


  --Tạo thủ tục lấy dữ liệu từ bảng NCC
go
 Create Proc LDSNCC
 as
 select * from NHACUNGCAP
 go
 exec LDSNCC
 --Thêm dữ liệu NCC
 go
 Create Proc ThemNCC @Manhacungcap nvarchar(10),@Tennhacungcap nvarchar(20),@Diachi nvarchar(15),@Sodienthoai char(15)
 as
  Insert into NHACUNGCAP(Manhacungcap,Tennhacungcap,Diachi,Sodienthoai)
  values (@Manhacungcap,@Tennhacungcap,@Diachi,@Sodienthoai)
  go
  exec ThemNCC N'NCC06',N'Cty TNHH Anh Khoa',N'Bình Dương',0954848842
  --Cập nhật NCC
  go
  Create Proc CapNhapNCC @sManhacungcap nvarchar(10),@Tennhacungcap nvarchar(20),@Diachi nvarchar(15),@Sodienthoai char(15)
  as
  update NHACUNGCAP set 
  Tennhacungcap = @Tennhacungcap,
  Diachi=@Diachi,
  Sodienthoai= @Sodienthoai
  
  where Manhacungcap = @sManhacungcap 
  go
 
  exec CapNhapNCC N'NCC03',N'Cty TNHH Aba',N'Hà Tĩnh',0851231878
  -- xóa Nhà Cung cấp 
go
  CREATE PROC XoaNCC @sManhacungcap nvarchar(10)
as
	DELETE FROM NHACUNGCAP
	WHERE Manhacungcap = @sManhacungcap
GO
-- lấy toàn bộ nhà cung cấp 
CREATE PROC LayToanNCC
AS 
	SELECT * FROM NHACUNGCAP
GO

   --Tạo thủ tục lấy dữ liệu từ bảng sản phẩm

 Create Proc LDSSANPHAM
 as
 select * from SANPHAM
 go

  --Cập nhật SP
  go
  Create Proc CapNhapSP(@sMaSP nvarchar(10),@sTenSP nvarchar(10), @sGiaSP float,@Manhacungcap nvarchar(10))
  as
  update SANPHAM set TenSP = @sTenSP,
  GiaSP=@sGiaSP,
  Manhacungcap = @Manhacungcap where MaSP= @sMaSP
   go
 
   exec CapNhapSP N'SP2', N'Kẹo', 8000, N'NCC01'

 
      --Tạo thủ tục lấy dữ liệu từ bảng nhà kho
go
 Create Proc LDSNhaKho
 as
 select * from NHAKHO
 go
 exec LDSNhaKho

 --Thêm dữ liệu Nha kho
 go
 Create Proc ThemNhaKho(@sMaSP nvarchar(10) ,@MaCH nvarchar(10),@SoLuong int)
 as
  Insert into NHAKHO(MaSP,MaCH,SoLuong) 
  values (@sMaSP, @MaCH,@SoLuong)
  go
  exec ThemNhaKho N'SP2',N'CH01',35

  --Cập nhật Nhà kho
  go
    Create Proc CapNhapNhaKho @sMaSP nvarchar(10) ,@MaCH nvarchar(10),@SoLuong int
  as
  update NHAKHO set SoLuong = @SoLuong where MaSP = @sMaSP and MaCH = @MaCH
   go
   exec CapNhapNhaKho N'SP2',N'CH02',90

--xoa nha kho
	go
CREATE PROCEDURE XoaNhaKho @sMaSP nvarchar(10) ,@MaCH nvarchar(10)
AS
delete from NHAKHO
where  MaSP = @sMaSP and MaCH = @MaCH
go

   --Store thêm sản phẩm
go
CREATE PROCEDURE ThemSanPham @MaSP nvarchar(10), @TenSP nvarchar(10), @GiaSP float,@Manhacungcap nvarchar(10)
AS
Insert into dbo.SANPHAM
values (@MaSP, @TenSP,@GiaSP,@Manhacungcap)

go

select*from SANPHAM
--sua san pham 
go
    Create Proc SuaSanPham @MaSP nvarchar(10), @TenSP nvarchar(10), @GiaSP float,@Manhacungcap nvarchar(10)
  as
  update SANPHAM set TenSP = @TenSP, GiaSP = @GiaSP,Manhacungcap = @Manhacungcap  where MaSP = @MaSP
   go
-- Store xóa sản phẩm

CREATE PROCEDURE XoaSanPham @MaSP nvarchar(10)
AS
delete from SANPHAM
where  MaSP = @MaSP 
go
EXEC XoaSanPham @MaSP = N'SP7'


--Lay toan bo san pham 
go 
create proc LayDSSanPham
as
select*from SANPHAM
go
--drop PROCEDURE ThemSanPham
select * from SANPHAM

go

--- lay danh sach hoa don
go 
create proc LayDSHoaDon
as
select*from HOADON
go

--Thêm hóa đơn

create proc ThemHoaDon  @MaKH nvarchar(10),@MaNV nvarchar(10),@NgayXuat datetime,@SoTien float,@PhuongThucThanhToan nvarchar(100), @GiamGia float, @TongTien float
as
insert into HOADON (MaKH,Manv,NgayXuat,SoTien,PhuongThucThanhToan,GiamGia, TongTien)
output inserted.MaHD
values (@MaKH,@MaNV,@NgayXuat,@SoTien,@PhuongThucThanhToan,@GiamGia,@TongTien)
go

--Sua hoa don
--GO
--CREATE PROC SuaHoaDon
--	@MaKH nvarchar(10),
--	@MaNV nvarchar(10),
--	@NgayXuat datetime,
--	@TongTien float,
--	@PhuongThucThanhToan nvarchar(100),


--	UPDATE HOADON
--	SET MaKH = @MaKH,
--		Manv = @MaNV,
--		NgayXuat = @NgayXuat,
--		TongTien = @TongTien,
--		PhuongThucThanhToan = @PhuongThucThanhToan
--	WHERE MaHD = @MaHD 
--	go
	--Xoa hoa don

	go
  CREATE PROC XoaHoaDon @MaHD nvarchar(10)
as
	DELETE FROM HOADON
	WHERE MaHD = @MaHD
GO

--Thêm Cửa hàng Circle K

create proc ThemCHCIRCLEK @MaCH nvarchar(10), @DChi nvarchar(15), @GiayPhep nvarchar(30)
as
insert into CUAHANGCIRCLEK(MaCH,Diachi,Giayphep)
values (@MaCH,@DChi,@GiayPhep)
go

exec ThemCHCIRCLEK @MaCH=N'CH06',@DChi=N'Linh Trung',@GiayPhep = N'Có giấy phép'
go
	
select*from CUAHANGCIRCLEK
go

--Xóa cửa hàng 
GO
CREATE PROC XoaCHCircleK @MaCH nvarchar(10)
AS
	DELETE FROM CUAHANGCIRCLEK
	WHERE MaCH = @MaCH
GO
-- sửa cửa hàng 
CREATE PROC SuaCHCircleK
	@MaCH nvarchar(10),
	@DChi nvarchar(15),
	@GiayPhep nvarchar(30)
	
AS
	UPDATE CUAHANGCIRCLEK
	SET Diachi = @DChi,
		Giayphep = @GiayPhep	
	WHERE MaCH = @MaCH 
go
-- lấy toàn bộ cửa hàng 
go 
create proc LayDSCuaHangCircleK
as
select*from CUAHANGCIRCLEK

-- report Nhan Vien
go
Create proc LayBangNhanVien(@Manv nvarchar(10))
as
select * from NHANVIEN nv 
where nv.Manv = @Manv
go
exec LayBangNhanVien 'NV01'

--them chi tiet hoa don
go 
create proc ThemChiTietHoaDon @MaHD nvarchar(10),@MaSP nvarchar(10),@SoLuong int ,@ThanhTien float
as
insert into ChiTietHoaDon
values (@MaHD,@MaSP ,@SoLuong,@ThanhTien )
go
---San pham---	
	select * from HOADON
	go
	--delete from hoadon
	--delete from ChiTietHoaDon

select sp.MaSP,sp.TenSP,sp.GiaSP, cthd.SoLuong,cthd.ThanhTien, hd.GiamGia, hd.TongTien from SANPHAM sp 
join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP 
join HOADON hd on hd.MaHD =cthd.MaHD
where cthd.MaHD = 'DH00001'
go
select hd.MaHD, hd.NgayXuat, nv.Tennv from HOADON hd
join NHANVIEN nv on hd.Manv = nv.Manv
where hd.MaHD = 'DH00002'
go
select sp.TenSP, sp.GiaSP,cthd.SoLuong, cthd.ThanhTien  from ChiTietHoaDon cthd
join SANPHAM sp on cthd.MaSP = sp.MaSP
where cthd.MaHD = 'DH00003'
go

select sp.MaSP, sp.TenSP, nk.SoLuong,sp.GiaSP, ch.Diachi from SANPHAM sp 
                join NHAKHO nk on sp.MaSP = nk.MaSP 
                join CUAHANGCIRCLEK ch on nk.MaCH = ch.MaCH 
                where nk.MaCH = N'CH03' and sp.Manhacungcap = N'NCC01'

select *from NHAKHO

update NHAKHO set SoLuong = SoLuong - 2 
where NHAKHO.MaSP = N'SP1' and NHAKHO.MaCH = 'CH03'

update HOADON set Manv = 'NV01'

go

--report lấy khách hàng 
go
create proc LayDsSPKhachHangMua
@MaKH nvarchar(10),
@NgayThongKe date
as	
begin
select kh.MaKH,sp.MaSP,sp.TenSP,cthd.SoLuong,sp.GiaSP,cthd.ThanhTien, sum(hd.TongTien) as N'Tổng Thành Tiền' from SANPHAM sp
join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP
join HOADON hd on hd.MaHD = cthd.MaHD
join KHACHHANG kh on kh.MaKH = hd.MaKH
where kh.MaKH = 'KH01'
and Month(hd.NgayXuat) = MONTH(@NgayThongKe)
and year(hd.NgayXuat) = year(@NgayThongKe) 
group by kh.MaKH,sp.MaSP,sp.TenSP,cthd.SoLuong,sp.GiaSP,cthd.ThanhTien
end;
go
select kh.MaKH,sp.MaSP,sp.TenSP,cthd.SoLuong,sp.GiaSP,cthd.ThanhTien, sum(hd.TongTien) as N'Tổng Thành Tiền' from SANPHAM sp
join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP
join HOADON hd on hd.MaHD = cthd.MaHD
join KHACHHANG kh on kh.MaKH = hd.MaKH
where kh.MaKH = 'KH05'
and Month(hd.NgayXuat) = MONTH('02-14-2023')
and year(hd.NgayXuat) = year('02-14-2023') 
group by kh.MaKH,sp.MaSP,sp.TenSP,cthd.SoLuong,sp.GiaSP,cthd.ThanhTien


select * from ChiTietHoaDon

select year(CONVERT(date,'2023-12-14',23))
---Tim nhan vien
go
	CREATE PROCEDURE TimMaNhanVienBietLuong
    @Manv nvarchar(10)
AS
BEGIN
    SELECT LUONG.MaLuong, NHANVIEN.Manv, NHANVIEN.Tennv, NHANVIEN.NgaySinh, LUONG.Luong
    FROM NHANVIEN
    INNER JOIN LUONG ON NHANVIEN.Manv = LUONG.Manv
    WHERE NHANVIEN.Manv = @Manv;
END;
GO
--EXEC TimMaNhanVienBietLuong @Manv = N'NV01';

--Lấy hóa đơn
go
create proc LayHoaDon @MaHD nvarchar(10)
as
begin
select hd.MaHD, sp.MaSP,sp.TenSP,sp.GiaSP,cthd.SoLuong,cthd.ThanhTien from SANPHAM sp
join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP
join HOADON hd on hd.MaHD = cthd.MaHD 
where hd.MaHD = @MaHD
end;
go

select hd.MaHD, sp.MaSP,sp.TenSP,sp.GiaSP,cthd.SoLuong,cthd.ThanhTien from SANPHAM sp
join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP
join HOADON hd on hd.MaHD = cthd.MaHD 
where hd.MaHD = 'DH000012'