using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            txtHovaTen.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDangNhap frmDangNhap = new frmDangNhap();
            frmDangNhap.Show();
        }

        private void frmDangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            PerformSignIn();
        }
        private void PerformSignIn()
        {
          
            errorProvider1.Clear();

            // Lấy giá trị từ các trường nhập  
            string fullName = txtHovaTen.Text.Trim();
            string phoneNumber = mtxtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtMatKhau.Text.Trim();
            string confirmPassword = txtNhapLaiMK.Text.Trim();

            bool valid = true;

            // Kiểm tra họ và tên  
            if (string.IsNullOrWhiteSpace(fullName))
            {
                errorProvider1.SetError(txtHovaTen, "Họ và tên không được để trống.");
                valid = false;
            }

            // Kiểm tra số điện thoại  
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\(\+84\) \d{0,2} - \d{3} - \d{4}$"))
            {
                errorProvider1.SetError(mtxtPhone, "Số điện thoại không hợp lệ. Vui lòng nhập theo định dạng (+84) _-____.");
                valid = false;
            }

            // Kiểm tra email  
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtEmail, "Email không hợp lệ.");
                valid = false;
            }

            // Kiểm tra mật khẩu  
            if (string.IsNullOrWhiteSpace(password))
            {
                errorProvider1.SetError(txtMatKhau, "Mật khẩu không được để trống.");
                valid = false;
            }
            else if (password.Length < 6)
            {
                errorProvider1.SetError(txtMatKhau, "Mật khẩu cần ít nhất 6 ký tự.");
                valid = false;
            }

            // Kiểm tra nhập lại mật khẩu  
            if (password != confirmPassword)
            {
                errorProvider1.SetError(txtNhapLaiMK, "Mật khẩu không khớp.");
                valid = false;
            }

            // Nếu tất cả các trường hợp đều hợp lệ  
            if (valid)
            {
                // Thực hiện đăng ký  
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Có thể thêm mã thực hiện đăng ký vào cơ sở dữ liệu ở đây  
            }
        


    }
    }
}
