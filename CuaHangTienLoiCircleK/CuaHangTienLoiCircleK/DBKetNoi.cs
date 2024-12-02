using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace CuaHangTienLoiCircleK
{
    internal class DBKetNoi
    {
        private SqlConnection conn;
        private SqlDataAdapter da;
        private SqlCommand cmd;
        private DataTable dt;

        public SqlConnection KetNoi()
        {
            string sqlcon = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCHTLCIRCLEK;Integrated Security=True;Encrypt=False";
            return new SqlConnection(sqlcon);  
        }
        public DBKetNoi() { }


        public DataTable LayDSNhanVien(string store)
        {
            dt = new DataTable();
            try
            {
                conn = KetNoi();
                // con.Open();
                // sqlcommand
                conn.Open();// mo ket noi
                // khai bao command 
                cmd = new SqlCommand();
                cmd.CommandText = store;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                // khai bao table
                da = new SqlDataAdapter(cmd);
                da.Fill(dt); // fill du lieu cho bang
                             //  dgvSinhvien.DataSource = dtsv; // datasource cho dgv 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
            finally
            {
                   conn.Close();
            }
            return dt;
        }

        public DataTable LayDSNhaCungCap(string store)
        {
            dt = new DataTable();
            try
            {
                conn = KetNoi();
                // con.Open();
                // sqlcommand
                conn.Open();// mo ket noi
                // khai bao command 
                cmd = new SqlCommand();
                cmd.CommandText = store;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                // khai bao table
                da = new SqlDataAdapter(cmd);
                da.Fill(dt); // fill du lieu cho bang
                             //  dgvSinhvien.DataSource = dtsv; // datasource cho dgv 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;

        }

        public int ThemNhanVien(string Manv, string Tennv, string NgaySinh, string Diachi, string SDTNhanVien, string Phai)
        {
            int ketqua = -1;
            conn.Open();
            string tenNV = "ThemNV";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = tenNV;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@Manv", Manv);
            cmd.Parameters.AddWithValue("@Tennv", Tennv);
            cmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            cmd.Parameters.AddWithValue("@Diachi", Diachi);
            cmd.Parameters.AddWithValue("@SDTNhanVien", SDTNhanVien);
            cmd.Parameters.AddWithValue("@Phai", Phai);


            try
            {
                ketqua = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Loi " + ex.Message);
            }
            conn.Close();
            return ketqua;

        }
    }
}
