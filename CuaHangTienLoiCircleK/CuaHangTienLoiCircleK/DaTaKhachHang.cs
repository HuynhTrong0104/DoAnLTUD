using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    internal class DaTaKhachHang
    {
        SqlConnection sqlconn;
        string s = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCHTLCIRCLEK;Integrated Security=True;Encrypt=False";
        public DaTaKhachHang()
        {
            sqlconn = new SqlConnection(s);
            try
            {
                sqlconn.Open();
            }
            catch(Exception ex) {
            
                sqlconn.Close();
                throw  ex;
            }
        }

        public DataTable LayDSKhachHang()
        {
            DataTable dataTable = new DataTable ();
            SqlDataAdapter dataAdapter;
            string sQL = "LayDSKhachHang";
            SqlCommand sqlCommand = new SqlCommand(sQL, sqlconn);
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

        public int ThemKhachHang(string maKH, string tenKhachHang, string diachi, string sdt)
        {
            int KQ = 1;
            string themKhachHang = "ThemKHACHHANG";
            SqlCommand sqlCommand = new SqlCommand(themKhachHang, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaKH", maKH);
            sqlCommand.Parameters.AddWithValue("@TenKH", tenKhachHang);
            sqlCommand.Parameters.AddWithValue("@DiaChiKH", diachi);
            sqlCommand.Parameters.AddWithValue("@SDTKH", sdt);
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

        public int SuaKhachHang(string maKH, string tenKhachHang, string diachi, string sdt)
        {
            int KQ = 1;
            string sua = "CapNhapKHACHHANG";
            SqlCommand sqlCommand = new SqlCommand(sua, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaKH", maKH);
            sqlCommand.Parameters.AddWithValue("@TenKH", tenKhachHang);
            sqlCommand.Parameters.AddWithValue("@DiaChiKH", diachi);
            sqlCommand.Parameters.AddWithValue("@SDTKH", sdt);
            try
            {
                KQ = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //throw ex;
            }
            return KQ;
        }

        public int XoaKH(string maKH)
        {
            int KQ = -1;
            string tenno = "XoaKhachHang";
            SqlCommand sqlCommand = new SqlCommand(tenno, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaKH", maKH);
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

        public DataTable TimKhachHangTheoMa(string maKH)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Đảm bảo kết nối đã được mở
                if (sqlconn.State != ConnectionState.Open)
                {
                    sqlconn.Open();
                }

                // Cấu hình SqlCommand
                string tenno = "TIMKHACHHANG_THEOMA";
                using (SqlCommand sqlCommand = new SqlCommand(tenno, sqlconn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@MaKH", maKH);

                    // Sử dụng SqlDataAdapter để đổ dữ liệu vào DataTable
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }

                // Kiểm tra nếu không có dữ liệu trong DataTable
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng với mã khách hàng: " + maKH, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Đã xảy ra lỗi khi truy vấn khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng kết nối nếu nó còn mở
                if (sqlconn.State == ConnectionState.Open)
                {
                    sqlconn.Close();
                }
            }

            return dataTable;
        }


    }
}
