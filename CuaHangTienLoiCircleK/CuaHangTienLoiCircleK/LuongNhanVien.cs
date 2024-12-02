using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    public partial class frmLuongNhanVien : Form
    {
        LuongVaKhoHang luong = new LuongVaKhoHang();
        public frmLuongNhanVien()
        {
            InitializeComponent();
            txtPhuCap.TextChanged += tinhToanTongLuong;
            txtTangCa.TextChanged += tinhToanTongLuong;
            txtThuong.TextChanged += tinhToanTongLuong;
            txtLuongCoBan.TextChanged += tinhToanTongLuong;

            //này để ràng buộc 
            txtPhuCap.Validating += txtLuong_Validating;
            txtTangCa.Validating += txtLuong_Validating;
            txtThuong.Validating += txtLuong_Validating;
            txtLuongCoBan.Validating += txtLuong_Validating;
        }

        private void frmLuongNhanVien_Load(object sender, EventArgs e)
        {
            dgvView.DataSource = luong.LayDSLuong();

            DataTable dtKhachHang = luong.layDanhSachNhanVien();
            cboMaNV.DataSource = dtKhachHang;
            cboMaNV.DisplayMember = "Manv"; // Tên cột hiển thị tên khách hàng
            cboMaNV.ValueMember = "Manv"; // Tên cột chứa mã khách hàng
            cboMaNV.SelectedIndexChanged += cboMaNV_SelectedIndexChanged;
        }
        private void txtLuong_Validating(object sender, CancelEventArgs e)
        {
            TextBox baoLoi = sender as TextBox;
            if (!decimal.TryParse(baoLoi.Text, out _))
            {
                errorProvider1.SetError(baoLoi, "Phải nhập số");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(baoLoi, "");
            }
        }
        private void tinhToanTongLuong(object sender, EventArgs e)
        {
            // Kiểm tra và chuyển đổi txtLuongPhuCap
            bool isPhuCapValid = decimal.TryParse(txtPhuCap.Text, out decimal phuCap);
            if (!isPhuCapValid) phuCap = 0;

            // Làm tương tự cho các TextBox khác
            bool isTangCaValid = decimal.TryParse(txtTangCa.Text, out decimal tangCa);
            if (!isTangCaValid) tangCa = 0;

            bool isThuongValid = decimal.TryParse(txtThuong.Text, out decimal thuong);
            if (!isThuongValid) thuong = 0;

            bool isLuongCBValid = decimal.TryParse(txtLuongCoBan.Text, out decimal luongCB);
            if (!isLuongCBValid) luongCB = 0;

            // Tính toán tổng lương nếu tất cả các giá trị đều hợp lệ
            if (isPhuCapValid && isTangCaValid && isThuongValid && isLuongCBValid)
            {
                decimal tongLuong = phuCap + tangCa + thuong + luongCB;
                txtTongLuong.Text = tongLuong.ToString();
            }
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có nhân viên nào được chọn không
            if (cboMaNV.SelectedIndex != -1)
            {
                // Lấy DataRowView hiện tại được chọn trong ComboBox
                DataRowView drv = (DataRowView)cboMaNV.SelectedItem;

                if (drv["Tennv"] != DBNull.Value)
                {
                    // Đặt tên nhân viên cho TextBox txtTenNhanVien
                    txtTenNhanVien.Text = drv["Tennv"].ToString();
                }
                else
                {
                    // Nếu không có tên, hãy xóa TextBox
                    txtTenNhanVien.Text = string.Empty;
                }

                // hiện thị đúng với ngày sinh mà mình đã chọn ra tên 
                if (drv["NgaySinh"] != DBNull.Value)
                {
                    DateTime ngaySinh = (DateTime)drv["NgaySinh"];
                    dtpNgaySinh.Value = ngaySinh;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaLuong = txtMaLuong.Text;
                if (luong.KiemTraMaLuongTonTai(MaLuong))
                {
                    MessageBox.Show("Mã lương đã tồn tại. Vui lòng nhập mã lương khác.");
                    return;
                }

                string Manv = ((DataRowView)cboMaNV.SelectedItem)["Manv"].ToString();
                DateTime NgaySinh = dtpNgaySinh.Value;
                string Tennv = txtTenNhanVien.Text;

                decimal phuCap = string.IsNullOrEmpty(txtPhuCap.Text) ? 0 : Convert.ToDecimal(txtPhuCap.Text);
                decimal tangCa = string.IsNullOrEmpty(txtTangCa.Text) ? 0 : Convert.ToDecimal(txtTangCa.Text);
                decimal thuong = string.IsNullOrEmpty(txtThuong.Text) ? 0 : Convert.ToDecimal(txtThuong.Text);
                decimal luongCB = string.IsNullOrEmpty(txtLuongCoBan.Text) ? 0 : Convert.ToDecimal(txtLuongCoBan.Text);

                decimal tongLuong = phuCap + tangCa + thuong + luongCB;
                txtTongLuong.Text = tongLuong.ToString();

                // Call ThemLuong with the correct number and type of arguments
                if (luong.ThemLuong(MaLuong, Manv, phuCap, tangCa, thuong, NgaySinh, luongCB, tongLuong) >= 0)
                {
                    MessageBox.Show("Thêm thành công!");
                    // Clear the inputs and refresh the DataGridView
                    txtMaLuong.Clear();
                    txtPhuCap.Clear();
                    txtTangCa.Clear();
                    txtThuong.Clear();
                    txtLuongCoBan.Clear();
                    txtTongLuong.Clear();
                    dgvView.DataSource = luong.LayDSLuong();
                }
                else
                {
                    MessageBox.Show("Thêm không thành công!");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi định dạng: " + ex.Message); // Bắt lỗi định dạng số
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message); // Bắt lỗi chung
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string MaLuong = txtMaLuong.Text;
                if (string.IsNullOrEmpty(MaLuong))
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi để sửa.");
                    return;
                }

                string Manv = ((DataRowView)cboMaNV.SelectedItem)["Manv"].ToString();
                DateTime NgaySinh = dtpNgaySinh.Value;
                decimal phuCap = string.IsNullOrEmpty(txtPhuCap.Text) ? 0 : Convert.ToDecimal(txtPhuCap.Text);
                decimal tangCa = string.IsNullOrEmpty(txtTangCa.Text) ? 0 : Convert.ToDecimal(txtTangCa.Text);
                decimal thuong = string.IsNullOrEmpty(txtThuong.Text) ? 0 : Convert.ToDecimal(txtThuong.Text);
                decimal luongCB = string.IsNullOrEmpty(txtLuongCoBan.Text) ? 0 : Convert.ToDecimal(txtLuongCoBan.Text);
                decimal tongLuong = phuCap + tangCa + thuong + luongCB;

                int result = luong.SuaLuong(MaLuong, Manv, phuCap, tangCa, thuong, NgaySinh, luongCB, tongLuong);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật lương thành công!");
                    dgvView.DataSource = luong.LayDSLuong(); // Refresh the DataGridView
                }
                else
                {
                    MessageBox.Show("Cập nhật lương không thành công.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Lỗi định dạng: " + ex.Message); // Bắt lỗi định dạng số
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message); // Bắt lỗi chung
            }
        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvView.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgvView.Rows[e.RowIndex];

            try
            {
                txtMaLuong.Text = row.Cells[0].Value?.ToString() ?? "";
                string selectedManv = row.Cells[1].Value?.ToString() ?? "";
                cboMaNV.SelectedValue = selectedManv;
                txtPhuCap.Text = row.Cells[2].Value?.ToString() ?? "";
                txtTangCa.Text = row.Cells[3].Value?.ToString() ?? "";
                txtThuong.Text = row.Cells[4].Value?.ToString() ?? "";

                if (row.Cells[5].Value != DBNull.Value && row.Cells[5].Value is DateTime)
                {
                    dtpNgaySinh.Value = (DateTime)row.Cells[5].Value;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }


                DataRowView drv = (DataRowView)cboMaNV.SelectedItem;
                txtTenNhanVien.Text = drv["Tennv"].ToString();

                txtLuongCoBan.Text = row.Cells[6].Value?.ToString() ?? "";
                txtTongLuong.Text = row.Cells[7].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaLuong = txtMaLuong.Text;

            if (string.IsNullOrEmpty(MaLuong))
            {
                MessageBox.Show("Vui lòng chọn hoá đơn để xóa.");
                return;
            }
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hoá đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = luong.XoaLuong(MaLuong);
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa hoá đơn thành công!");
                        dgvView.DataSource = luong.LayDSLuong(); // Refresh the DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Xóa hoá đơn không thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
