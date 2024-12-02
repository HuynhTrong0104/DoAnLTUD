using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace CuaHangTienLoiCircleK
{ 
    
    internal class LuongVaKhoHang
    {
        SqlConnection conn;
        string s = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCHTLCIRCLEK;Integrated Security=True;Encrypt=False";
        public LuongVaKhoHang()
        {
            conn = new SqlConnection(s);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
        public int connectDB()
        {
            int Thuan = 0;
            conn = new SqlConnection(s);
            try
            {
                conn.Open();
                Thuan = 1;
            }
            catch (Exception ex)
            {
                Thuan = 0;
                conn.Close();
                throw ex;
            }
            return Thuan;
        }

        public DataTable LayDSLuong()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter;
            string sQL = "LayDanhSachLuong";
            SqlCommand sqlCommand = new SqlCommand(sQL, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dataTable;
        }

        public DataTable LayDSKhoHang()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter;
            string sQL = "DANHSACH_NHAKHO";
            SqlCommand sqlCommand = new SqlCommand(sQL, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dataTable;
        }

        public int ThemLuong(string MaLuong, string Manv, decimal PhuCap, decimal TangCa, decimal Thuong, DateTime NgaySinh, decimal LuongCB, decimal Luong)
        {
            int KQ = -1;
            using (SqlConnection sqlconn = new SqlConnection(s))
            {
                SqlCommand sqlCommand = new SqlCommand("ThemLUONG", sqlconn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@MaLuong", MaLuong);
                sqlCommand.Parameters.AddWithValue("@Manv", Manv);
                sqlCommand.Parameters.AddWithValue("@PhuCap", PhuCap);
                sqlCommand.Parameters.AddWithValue("@TangCa", TangCa);
                sqlCommand.Parameters.AddWithValue("@Thuong", Thuong);
                sqlCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                sqlCommand.Parameters.AddWithValue("@LuongCB", LuongCB);
                sqlCommand.Parameters.AddWithValue("@Luong", Luong);
                sqlconn.Open();
                KQ = sqlCommand.ExecuteNonQuery();
            } // The connection is automatically closed here
            return KQ;

        }

        public int SuaLuong(string MaLuong, string Manv, decimal PhuCap, decimal TangCa, decimal Thuong, DateTime NgaySinh, decimal LuongCB, decimal Luong)
        {
            int KQ = -1;
            using (SqlConnection sqlconn = new SqlConnection(s)) // Make sure 's' is the connection string
            {
                string tenHoaDon = "CapNhapLuong"; // Correct stored procedure name
                SqlCommand sqlCommand = new SqlCommand(tenHoaDon, sqlconn);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@MaLuong", MaLuong);
                sqlCommand.Parameters.AddWithValue("@Manv", Manv);
                sqlCommand.Parameters.AddWithValue("@PhuCap", PhuCap);
                sqlCommand.Parameters.AddWithValue("@TangCa", TangCa);
                sqlCommand.Parameters.AddWithValue("@Thuong", Thuong);
                sqlCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);

                sqlCommand.Parameters.AddWithValue("@LuongCB", LuongCB);
                sqlCommand.Parameters.AddWithValue("@Luong", Luong);


                try
                {
                    sqlconn.Open();
                    KQ = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception here
                    throw;
                }
            } // The connection is automatically closed here
            return KQ;
        }

        public int XoaLuong(string MaLuong)
        {
            int result = -1;
            using (SqlConnection sqlconn = new SqlConnection(s)) // Make sure 's' is the connection string
            {
                string procedureName = "XoaLuong";
                SqlCommand sqlCommand = new SqlCommand(procedureName, sqlconn);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@MaLuong", MaLuong);
                try
                {
                    sqlconn.Open();
                    result = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } // The connection is automatically closed here
            return result;
        }

        public int themKho(string MaNKho, string MaSP, string SLtonkho, string TenSP, DateTime HSD)
        {
            int KQ = -1;
            string tenHoaDon = "THEM_NHAKHO";
            SqlCommand sqlCommand = new SqlCommand(tenHoaDon, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaNKho", MaNKho);
            sqlCommand.Parameters.AddWithValue("@MaSP", MaSP);
            sqlCommand.Parameters.AddWithValue("@SLtonkho", SLtonkho);
            sqlCommand.Parameters.AddWithValue("@TenSP", TenSP);
            sqlCommand.Parameters.AddWithValue("@HSD", HSD);
            try
            {
                KQ = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return KQ;
        }

        public int suaKho(string MaNKho, string MaSP, string SLtonkho, string TenSP, DateTime HSD)
        {
            int KQ = -1;
            string tenHoaDon = "SUA_NHAKHO";
            SqlCommand sqlCommand = new SqlCommand(tenHoaDon, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaNKho", MaNKho);
            sqlCommand.Parameters.AddWithValue("@MaSP", MaSP);
            sqlCommand.Parameters.AddWithValue("@SLtonkho", SLtonkho);
            sqlCommand.Parameters.AddWithValue("@TenSP", TenSP);
            sqlCommand.Parameters.AddWithValue("@HSD", HSD);
            try
            {
                KQ = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return KQ;
        }

        public int XoaKho(string MaNKho)
        {
            int result = -1;
            string procedureName = "Xoa_NhaKho";
            SqlCommand sqlCommand = new SqlCommand(procedureName, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaNKho", MaNKho);

            try
            {
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public DataTable Tim_NhaKho(string MaNKho)
        {
            DataTable dataTable = new DataTable();
            string procedureName = "Tim_NhaKho";
            SqlCommand sqlCommand = new SqlCommand(procedureName, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaNKho", MaNKho);

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Consider logging the exception details here
                throw;
            }

            return dataTable;
        }

        public DataTable Tim_Luong(string MaLuong)
        {
            DataTable dataTable = new DataTable();
            string procedureName = "Tim_Luong";
            SqlCommand sqlCommand = new SqlCommand(procedureName, conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaLuong", MaLuong);

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Consider logging the exception details here
                throw;
            }

            return dataTable;
        }

        public DataTable layDanhSachSanPham()
        {
            DataTable data = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("LDSSANPHAM", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            dataAdapter.Fill(data);
            return data;
        }
        public DataTable layDanhSachNhanVien()
        {
            DataTable data = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("LayDSNhanVien", conn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            dataAdapter.Fill(data);
            return data;
        }

        public bool KiemTraMaLuongTonTai(string maLuong)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM LUONG WHERE MaLuong = @MaLuong", conn);
            sqlCommand.Parameters.AddWithValue("@MaLuong", maLuong);
            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            return count > 0;
        }
        public bool KiemTraMaNhaKhoTonTai(string MaNKho)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM NHAKHO WHERE MaNKho = @MaNKho", conn);
            sqlCommand.Parameters.AddWithValue("@MaNKho", MaNKho);
            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            return count > 0;
        }
    }
}
