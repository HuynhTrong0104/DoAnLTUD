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
    
    public partial class frmKhachHang : Form
    {
        DaTaKhachHang DaTaKhachHang = new DaTaKhachHang();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {            
            string maKhachHang = txtMaKH.Text;
            string tenkhach = txtTenKH.Text;
            string diaChi = txtDiaChi.Text;
            string sdt = mtxtPhone.Text;
            if (DaTaKhachHang.ThemKhachHang(maKhachHang, tenkhach, diaChi, sdt) >= 0)
            {
                MessageBox.Show("Thêm thành công !!");
                txtMaKH.Clear();
                txtTenKH.Clear();
                txtDiaChi.Clear();
                mtxtPhone.Clear();
                dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();              
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
            string maKhach = txtMaKH.Text;

            if (string.IsNullOrEmpty(maKhach))
            {
                MessageBox.Show("Vui lòng chọn hoá đơn để xóa.");
                return;
            }
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa hoá đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int result = DaTaKhachHang.XoaKH(maKhach);
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa hoá đơn thành công!");
                        dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKhachHang = txtMaKH.Text;
            string tenkhach = txtTenKH.Text;
            string diaChi = txtDiaChi.Text;
            string sdt = mtxtPhone.Text;
            if (DaTaKhachHang.SuaKhachHang(maKhachHang, tenkhach, diaChi, sdt) >= 0)
            {
                MessageBox.Show("Sửa thành công!!");
                txtMaKH.Clear();
                txtTenKH.Clear();
                txtDiaChi.Clear();
                mtxtPhone.Clear();
                dgvView.DataSource = DaTaKhachHang.LayDSKhachHang();
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maHD = txtMaKH.Text;
            try
            {
                DataTable searchResults = DaTaKhachHang.TimKhachHangTheoMa(maHD);
                dgvView.DataSource = searchResults;
            }
            catch (Exception ex)
            {
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
    }
}
