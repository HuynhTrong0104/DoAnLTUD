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
    public partial class frmTraCuuHoaDon : Form
    {
        SqlConnection connection = Connect.KetNoi();

        public frmTraCuuHoaDon()
        {
            InitializeComponent();
        }
        private void KhoiTaoDgvChiTietHoaDon()
        {
            dgvTraCuuHoaDon.Columns.Add("maSp", "Mã sản phẩm");
            dgvTraCuuHoaDon.Columns.Add("tenSp", "Tên sản phẩm");
            dgvTraCuuHoaDon.Columns.Add("donGia", "Đơn giá");
            dgvTraCuuHoaDon.Columns.Add("soLuong", "Số lượng");
            dgvTraCuuHoaDon.Columns.Add("thanhTien", "Thành tiền");
        }

        private void frmTraCuuHoaDon_Load(object sender, EventArgs e)
        {
            dgvTraCuuHoaDon.ReadOnly = true;
            dgvTraCuuHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
            KhoiTaoDgvChiTietHoaDon();
        }
        

        private void btnTimtiep_Click(object sender, EventArgs e)
        {
            txtMaHD.Clear();
            txtThanhTien.Clear();
            dgvTraCuuHoaDon.Rows.Clear();
            txtGiamGia.Clear();
            txtSauKhiGiamGia.Clear();
        }

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            dgvTraCuuHoaDon.Rows.Clear();
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Sử dụng tham số để tránh SQL Injection
                cmd.CommandText = "select sp.MaSP,sp.TenSP,sp.GiaSP, cthd.SoLuong,cthd.ThanhTien, hd.GiamGia, hd.TongTien from SANPHAM sp " +
                "join ChiTietHoaDon cthd on sp.MaSP = cthd.MaSP " +
                "join HOADON hd on hd.MaHD =cthd.MaHD " +
                $"where cthd.MaHD = '{txtMaHD.Text}'";

                // Sử dụng tham số
                cmd.Parameters.AddWithValue("@MaHD", txtMaHD.Text);

                var reader = cmd.ExecuteReader();
                int giamGia = 0;
                float tiensauGiam = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string masp = reader["MaSP"].ToString();
                        string tensp = reader["TenSP"].ToString();
                        string giasp = reader["GiaSP"].ToString();
                        string soluong = reader["SoLuong"].ToString();
                        string thanhtien = reader["ThanhTien"].ToString();
                        giamGia = int.Parse(reader["GiamGia"].ToString());
                        tiensauGiam = float.Parse(reader["TongTien"].ToString());

                        // Thêm dòng vào DataGridView
                        dgvTraCuuHoaDon.Rows.Add(masp, tensp, giasp, soluong, thanhtien, giamGia, tiensauGiam);
                    }
                }
                else
                {
                    // Hiển thị thông báo nếu không có kết quả
                    MessageBox.Show("Không tìm thấy hóa đơn với mã: " + txtMaHD.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Tính tổng tiền và hiển thị vào các TextBox
                txtThanhTien.Text = TinhTongTien().ToString();
                txtGiamGia.Text = giamGia.ToString();
                txtSauKhiGiamGia.Text = tiensauGiam.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                connection.Close();
            }
        }
        private double TinhTongTien()
        {
            double tong = 0;
            for (int i = 0; i < dgvTraCuuHoaDon.Rows.Count - 1; i++)
            {
                tong += double.Parse(dgvTraCuuHoaDon.Rows[i].Cells["thanhTien"].Value.ToString());

            }
            return tong;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTraCuuHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
