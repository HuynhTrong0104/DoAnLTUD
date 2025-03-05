using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CuaHangTienLoiCircleK
{
    public partial class frmDangNhap : Form
    {
        string account = "admin";
        string pass = "admin";
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            PerformLogin();

        }

        private void PerformLogin()
        {
            // Kiểm tra các trường không được để trống
            if (string.IsNullOrWhiteSpace(txtTenTK.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTK.Focus(); // Đặt con trỏ vào TextBox
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus(); // Đặt con trỏ vào TextBox
                return;
            }

            // Logic đăng nhập

            if (txtTenTK.Text.Trim() == account.Trim() && txtMatKhau.Text.Trim() == pass.Trim())
            {
                Hide();
                frmTrangChu f = new frmTrangChu();
                f.FormClosed += (s, args) => this.Close();
                f.Show();
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenTK.Focus();
        }

        private void chkHienMK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHienMK.Checked)
            {
                txtMatKhau.PasswordChar = (char)0;
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void lkThoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void lkQuenPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Tk: admin", "Thông Báo");
            MessageBox.Show("Pass: admin", "Thông Báo");
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
