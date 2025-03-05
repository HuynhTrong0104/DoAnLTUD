using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    public partial class frmNhanVien : Form
    {
        DBKetNoi kn = new DBKetNoi();
        private SqlConnection connection = Connect.KetNoi();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
                string Manv = txtMaNV.Text;
                string Tennv = txtTenNV.Text;
                string NgaySinh = dtpNgaySinh.Value.Date.ToString("MM/dd/yyyy");
                string Diachi = txtDiaChi.Text;
                string SDTNhanVien = mtxtSDT.Text;
            if (string.IsNullOrEmpty(Manv) || string.IsNullOrEmpty(Tennv) || string.IsNullOrEmpty(NgaySinh)
                || string.IsNullOrEmpty(Diachi) || string.IsNullOrEmpty(SDTNhanVien))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string Phai;
                if (rdNam.Checked == true)
                {
                    Phai = rdNam.Text;
                }
                else
                {
                    Phai = rdNu.Text;
                }

                if (kn.ThemNhanVien(Manv, Tennv, NgaySinh, Diachi, SDTNhanVien, Phai) >= 0)
                {
                    MessageBox.Show("Thêm Thành Công","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Question);
                    dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");

                }
                else
                {
                    MessageBox.Show("Thêm Thất Bại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            
        }

        private void dgvView_Click(object sender, EventArgs e)
        {
            int dong = dgvView.CurrentCell.RowIndex;
            txtMaNV.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            txtTenNV.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvView.Rows[dong].Cells[3].Value.ToString();
            mtxtSDT.Text = dgvView.Rows[dong].Cells[4].Value.ToString();
            if (dgvView.Rows[dong].Cells[5].Value.ToString().Trim() == "Nam")
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi người dùng xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên có mã {maNV}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes)
            {
                return; // Người dùng từ chối xóa
            }

            try
            {
                // Mở kết nối
                using (SqlConnection connection = Connect.KetNoi())
                {
                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "XoaDSNhanVien";
                        cmd.Parameters.AddWithValue("@Manv", maNV);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Làm mới DataGridView
                dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
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


        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập
            string Manv = txtMaNV.Text.Trim();
            string Tennv = txtTenNV.Text.Trim();
            string NgaySinh = dtpNgaySinh.Value.Date.ToString("MM/dd/yyyy");
            string Diachi = txtDiaChi.Text.Trim();
            string SDTNhanVien = mtxtSDT.Text.Trim();
            string Phai = rdNam.Checked ? rdNam.Text : rdNu.Text;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(Manv) || string.IsNullOrEmpty(Tennv) || string.IsNullOrEmpty(NgaySinh) ||
                string.IsNullOrEmpty(Diachi) || string.IsNullOrEmpty(SDTNhanVien))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kết nối và thực thi stored procedure
                using (SqlConnection connection = Connect.KetNoi())
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SuaNhanVien";
                        cmd.Parameters.AddWithValue("@Manv", Manv);
                        cmd.Parameters.AddWithValue("@Tennv", Tennv);
                        cmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                        cmd.Parameters.AddWithValue("@Diachi", Diachi);
                        cmd.Parameters.AddWithValue("@SDTNhanVien", SDTNhanVien);
                        cmd.Parameters.AddWithValue("@Phai", Phai);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Cập nhật lại dữ liệu hiển thị trên DataGridView
                dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
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


        private void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
