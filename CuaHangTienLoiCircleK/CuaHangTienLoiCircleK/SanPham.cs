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
    public partial class frmSanPham : Form
    {
        private SqlConnection connection = Connect.KetNoi();
        DataTable dataTable;
        public frmSanPham()
        {
            InitializeComponent();
        }
        private DataTable LoadSanPham()
        {
            dataTable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            //(loi)    conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LDSSANPHAM";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dgvView.DataSource = LoadSanPham();
            LoaiNCC();
        }

        private void LoaiNCC()
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from NHACUNGCAP";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string maNCC = reader.GetString(0);
                cboNhaCungCap.Items.Add(maNCC);
            }
            connection.Close();
        }

        private void dgvView_Click(object sender, EventArgs e)
        {
            int dong = dgvView.CurrentCell.RowIndex;
            txtMaSP.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            txtTenSP.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
            txtGiaSP.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
            cboNhaCungCap.Text = dgvView.Rows[dong].Cells[3].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select*from SANPHAM where MaSP = '{txtMaSP.Text}'";
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("MaSP đã tồn tại");
                connection.Close();
            }
            else
            {
                connection.Close();
                var conn = Connect.KetNoi();
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThemSanPham";
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@GiaSP", txtGiaSP.Text);
                cmd.Parameters.AddWithValue("@Manhacungcap", cboNhaCungCap.Text);

                cmd.ExecuteNonQuery();

                conn.Close();
                dgvView.DataSource = LoadSanPham();
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                string maSP = txtMaSP.Text.Trim();
                connection.Open();
                SqlCommand cmd = new SqlCommand("XoaSanPham", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "XoaSanPham";
                //cmd.Parameters.AddWithValue("@MaSP", maSP);
                SqlParameter paMaSP = new SqlParameter("@MaSP", maSP);
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

                MessageBox.Show("Cần Phải chọn sản phẩm để xóa !", "Thông báo lỗi");
            }
            dgvView.DataSource = LoadSanPham();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuaSanPham";
            cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
            cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
            cmd.Parameters.AddWithValue("@GiaSP", txtGiaSP.Text);
            cmd.Parameters.AddWithValue("@Manhacungcap", cboNhaCungCap.Text);
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
            dgvView.DataSource = LoadSanPham();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();
            txtGiaSP.Clear();
            txtTenSP.Clear();
            //cboNhaCungCap.Items.
            txtMaSP.Focus();
        }
    }
}
