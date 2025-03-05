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
    
    public partial class frmKhachHang : Form
    {
        DaTaKhachHang DaTaKhachHang = new DaTaKhachHang();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu 
                string maKhachHang = txtMaKH.Text.Trim();
                string tenKhach = txtTenKH.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string sdt = mtxtPhone.Text.Trim();

                // Kiểm tra trường nào bị bỏ trống
                if (string.IsNullOrEmpty(maKhachHang) || string.IsNullOrEmpty(tenKhach) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int result = DaTaKhachHang.ThemKhachHang(maKhachHang, tenKhach, diaChi, sdt);

                if (result > 0)
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // làm mới
                    txtMaKH.Clear();
                    txtTenKH.Clear();
                    txtDiaChi.Clear();
                    mtxtPhone.Clear();

                    // Cập nhật lại DataGridView
                    dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
                }
                else
                {
                    MessageBox.Show("Không thể thêm khách hàng. Vui lòng kiểm tra lại thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                // Lỗi liên quan đến cơ sở dữ liệu
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Lỗi chung
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            if (dong >= 0)
            {
                txtMaKH.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
                txtTenKH.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
                txtDiaChi.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
                mtxtPhone.Text = dgvView.Rows[dong].Cells[3].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKhach = txtMaKH.Text.Trim();

            // Kiểm tra mã khách hàng 
            if (string.IsNullOrEmpty(maKhach))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi trước khi xóa
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // thực thi
                    int result = DaTaKhachHang.XoaKH(maKhach);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();

                        txtMaKH.Clear();
                        txtTenKH.Clear();
                        txtDiaChi.Clear();
                        mtxtPhone.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    // Xử lý lỗi liên quan đến SQL
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Xử lý các lỗi chung khác
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhachHang = txtMaKH.Text.Trim();
                string tenKhach = txtTenKH.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string sdt = mtxtPhone.Text.Trim();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maKhachHang) || string.IsNullOrEmpty(tenKhach) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int result = DaTaKhachHang.SuaKhachHang(maKhachHang, tenKhach, diaChi, sdt);

                // Kiểm tra kết quả trả về
                if (result > 0)
                {
                    MessageBox.Show("Sửa thông tin khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtMaKH.Clear();
                    txtTenKH.Clear();
                    txtDiaChi.Clear();
                    mtxtPhone.Clear();

                    dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để sửa. Vui lòng kiểm tra lại mã khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi liên quan đến cơ sở dữ liệu
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maHD = txtMaKH.Text.Trim();
            try
            {
                DataTable searchResults = DaTaKhachHang.TimKhachHangTheoMa(maHD);
                dgvView.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi liên quan đến cơ sở dữ liệu
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Focus();
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            mtxtPhone.Clear();
            dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();

        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
