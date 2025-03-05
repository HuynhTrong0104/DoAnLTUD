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
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        private SqlConnection conn = Connect.KetNoi();
        DataTable dataTable;

        private DataTable LoaiNCC()
        {
            dataTable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            SqlCommand cmd = new SqlCommand("LDSNCC", conn);
            cmd.CommandText = "LDSNCC";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);// loi
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
            txtMaNhaCungCap.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            mtxtSDT.Text = dgvView.Rows[dong].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
            txtNhaCungCap.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNcc = txtMaNhaCungCap.Text.Trim();
            string  diaChi= txtDiaChi.Text.Trim();
            string  sdt = mtxtSDT.Text.Trim();
            string  TenNCC = txtNhaCungCap.Text.Trim();

            if (string.IsNullOrEmpty(maNcc) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(TenNCC))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection conn = Connect.KetNoi();
                conn.Open();
                SqlCommand cmd = new SqlCommand("ThemNCC", conn);
                cmd.CommandText = "ThemNCC";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paMaNCC = new SqlParameter("@Manhacungcap", txtMaNhaCungCap.Text);
                cmd.Parameters.Add(paMaNCC);
                SqlParameter paSDT = new SqlParameter("@Sodienthoai", mtxtSDT.Text);
                cmd.Parameters.Add(paSDT);
                SqlParameter paDiaChi = new SqlParameter("@Diachi", txtDiaChi.Text);
                cmd.Parameters.Add(paDiaChi);
                SqlParameter paNhaCC = new SqlParameter("@Tennhacungcap", txtNhaCungCap.Text);
                cmd.Parameters.Add(paNhaCC);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Them thanh cong");
                }
                else
                {
                    MessageBox.Show("Them that bai");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);

            }
            dgvView.DataSource = LoaiNCC();
            txtNhaCungCap.Clear();
            txtDiaChi.Clear();
            txtMaNhaCungCap.Clear();
            txtMaNhaCungCap.Focus();
            mtxtSDT.Clear();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dgvView.SelectedCells[0].RowIndex;
                var maNCC = dgvView.Rows[row].Cells[0].Value.ToString().Trim();
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "XoaNCC";
                cmd.Parameters.AddWithValue("@sManhacungcap", maNCC);
                cmd.ExecuteNonQuery();
                conn.Close();
                dgvView.DataSource = LoaiNCC();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Cần phải xóa sản phẩm", "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaNhaCungCap.Text) ||
                string.IsNullOrWhiteSpace(txtNhaCungCap.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(mtxtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kết nối cơ sở dữ liệu
                using (SqlConnection conn = Connect.KetNoi())
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CapNhapNCC";

                    // Thêm tham số
                    cmd.Parameters.AddWithValue("@sManhacungcap", txtMaNhaCungCap.Text);
                    cmd.Parameters.AddWithValue("@Tennhacungcap", txtNhaCungCap.Text);
                    cmd.Parameters.AddWithValue("@Sodienthoai", mtxtSDT.Text);
                    cmd.Parameters.AddWithValue("@Diachi", txtDiaChi.Text);

                    // Thực thi stored procedure
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhà cung cấp để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Làm mới dữ liệu
                dgvView.DataSource = LoaiNCC();

                // Xóa thông tin nhập liệu
                txtMaNhaCungCap.Clear();
                txtNhaCungCap.Clear();
                txtDiaChi.Clear();
                mtxtSDT.Clear();
                txtMaNhaCungCap.Focus();
            }
        }


        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvView.DataSource = LoaiNCC();
        }

        private void frmNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
