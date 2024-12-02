using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    public partial class frmHeThongCuaHang : Form
    {

        private SqlConnection connection = Connect.KetNoi();
        DataTable dataTable;
        public frmHeThongCuaHang()
        {
            InitializeComponent();
        }

        private void frmHeThongCuaHang_Load(object sender, EventArgs e)
        {
            dgvView.DataSource = LoadCuaHang();
        }

        private DataTable LoadCuaHang()
        {
            dataTable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LayDSCuaHangCircleK";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvView_Click(object sender, EventArgs e)
        {
            int dong = dgvView.CurrentCell.RowIndex;
            txtMaCH.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            txtDiaChi.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
            txtGiayPhep.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from CUAHANGCIRCLEK where MaCH = '{txtMaCH.Text}'";
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("MaCH đã tồn tại");
                connection.Close();
            }
            else
            {
                connection.Close();
                var conn = Connect.KetNoi();
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThemCHCIRCLEK";
                cmd.Parameters.AddWithValue("@MaCH", txtMaCH.Text);
                cmd.Parameters.AddWithValue("@DChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@GiayPhep", txtGiayPhep.Text);

                cmd.ExecuteNonQuery();
                conn.Close();
                dgvView.DataSource = LoadCuaHang();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                string maCH = txtMaCH.Text.Trim();
                connection.Open();
                SqlCommand cmd = new SqlCommand("XoaCHCircleK", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "XoaCHCircleK";
                //cmd.Parameters.AddWithValue("@MaSP", maSP);
                SqlParameter paMaSP = new SqlParameter("@MaCH", maCH);
                cmd.Parameters.Add(paMaSP);
                //cmd.ExecuteNonQuery();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
                connection.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Cần Phải chọn Cữa Hàng để xóa !", "Thông báo lỗi");
            }
            dgvView.DataSource = LoadCuaHang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuaCHCircleK";
            cmd.Parameters.AddWithValue("@MaCH", txtMaCH.Text);
            cmd.Parameters.AddWithValue("@DChi", txtDiaChi.Text);
            cmd.Parameters.AddWithValue("@GiayPhep", txtGiayPhep.Text);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            cmd.ExecuteNonQuery();
            connection.Close();
            dgvView.DataSource = LoadCuaHang();

        }
    }
    
}
