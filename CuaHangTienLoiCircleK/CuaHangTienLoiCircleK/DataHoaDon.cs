using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangTienLoiCircleK
{
    internal class DataHoaDon
    {
        SqlConnection sqlconn;
        string s = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCUAHANGTLCIRCLEKS;Integrated Security=True";
        public DataHoaDon()
        {
            sqlconn = new SqlConnection(s);
            try
            {
                sqlconn.Open();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int connectDB()
        {
            int crk = 0;
            sqlconn = new SqlConnection(s);
            try
            {
                sqlconn.Open();
                crk = 1;
            }
            catch (Exception ex)
            {
                crk = 0;
                sqlconn.Close();
                throw ex;
            }
            return crk;
        }

        public DataTable LayDSHoaDon()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter;
            string sQL = "DANHSACHHOADON";
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

        public int themHoaDon(string maHD, string maKH, string maNV, DateTime ngayXuat)
        {
            int KQ = -1;
            string tenHoaDon = "ThemHoaDon";
            SqlCommand sqlCommand = new SqlCommand(tenHoaDon, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaHD", maHD);
            sqlCommand.Parameters.AddWithValue("@MaKH", maKH);
            sqlCommand.Parameters.AddWithValue("@MaNV", maNV);
            sqlCommand.Parameters.AddWithValue("@NgayXuat", ngayXuat);
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

        public int suaHoaDon(string maHD, string maKH, string maNV, DateTime ngayXuat)
        {
            int KQ = -1;
            string tenHoaDon = "SUAHOADON";
            SqlCommand sqlCommand = new SqlCommand(tenHoaDon, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaHD", maHD);
            sqlCommand.Parameters.AddWithValue("@MaKH", maKH);
            sqlCommand.Parameters.AddWithValue("@MaNV", maNV);
            sqlCommand.Parameters.AddWithValue("@NgayXuat", ngayXuat);
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

        public int xoaHoaDon(string maHD)
        {
            int result = -1;
            string procedureName = "XOAHOADON";
            SqlCommand sqlCommand = new SqlCommand(procedureName, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaHD", maHD);

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

        public DataTable timMaHoaDon(string maHD)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter;
            string sQL = "TIMHOADON_THEOMA";
            SqlCommand sqlCommand = new SqlCommand(sQL, sqlconn);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@MaHD", maHD);

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
    }
}
