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
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
            //MainFrom();
        }
    


        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }    

        private void cửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHeThongCuaHang frmHeThongCuaHang = new frmHeThongCuaHang();
            frmHeThongCuaHang.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = MessageBox.Show("Do You Want To Close?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(DialogResult == DialogResult.Yes) 
            {
                e.Cancel = false;
            }else e.Cancel = true;
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhaCungCap frmNhaCungCap = new frmNhaCungCap();
           // frmNhaCungCap.MdiParent = this;
            frmNhaCungCap.Show(); 
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSanPham frmSanPham = new frmSanPham();
            frmSanPham.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhanVien frmNhanVien = new frmNhanVien();
            frmNhanVien.Show();
        }

        private void nhàKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhoHang frmKhoHang = new frmKhoHang();
            frmKhoHang.Show();
        }

        private void lươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLuongNhanVien frmLuongNhanVien = new frmLuongNhanVien();
            frmLuongNhanVien.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang frmKhachHang = new frmKhachHang();
            frmKhachHang.Show();
        }

        private void hóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmHoaDon frmHoaDon = new frmHoaDon();
            frmHoaDon.Show();
        }

        private void traCứuHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTraCuuHoaDon frmTraCuuHoaDon = new frmTraCuuHoaDon();
            frmTraCuuHoaDon.Show();
        }

        private void doanhThuSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoanhThuSanPham frmDoanhThuSanPham = new frmDoanhThuSanPham();
            frmDoanhThuSanPham.Show();
        }

        private void kháchHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDoanhThuKhachHang frmDoanhThuKhachHang = new frmDoanhThuKhachHang();
            frmDoanhThuKhachHang.Show();
        }

        private void inNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInNhanVien frmInNhanVien = new frmInNhanVien();
            frmInNhanVien.Show();
        }

        private void inKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInKhachHang frmInKhachHang = new frmInKhachHang();
            frmInKhachHang.Show();
        }

        private void inHóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInHoaDon frmInHoaDon = new frmInHoaDon();
            frmInHoaDon.Show();
        }
    }
}
