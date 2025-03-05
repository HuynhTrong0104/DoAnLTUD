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
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
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
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) ||
                string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(txtGiaSP.Text) ||
                string.IsNullOrWhiteSpace(cboNhaCungCap.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra xem MaSP đã tồn tại chưa
                using (SqlConnection connection = Connect.KetNoi())
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT COUNT(*) FROM SANPHAM WHERE MaSP = @MaSP";
                        cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Mã sản phẩm đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // Thêm sản phẩm mới
                using (SqlConnection connection = Connect.KetNoi())
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ThemSanPham";

                        cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                        cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text);
                        cmd.Parameters.AddWithValue("@GiaSP", txtGiaSP.Text);
                        cmd.Parameters.AddWithValue("@Manhacungcap", cboNhaCungCap.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Thêm sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Làm mới DataGridView
                dgvView.DataSource = LoadSanPham();

                // Xóa thông tin nhập
                txtMaSP.Clear();
                txtTenSP.Clear();
                txtGiaSP.Clear();
                cboNhaCungCap.SelectedIndex = -1;
                txtMaSP.Focus();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
