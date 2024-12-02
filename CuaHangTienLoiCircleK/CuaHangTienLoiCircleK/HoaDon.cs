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
    public partial class frmHoaDon : Form
    {
        string maHD = "";
        public frmHoaDon()
        {
            InitializeComponent();
        }
        SqlConnection connection = Connect.KetNoi();

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            KhoiTaoKhachHangComboBox();
            KhoiTaoNhanVienComboBox();
            KhoiTaoPhuongThucThanhToanComboBox();
            KhoiTaoDChiTietHoaDon();
            KhoiTaoComboBoxMaCH();
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LDSNCC";
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string manhacc = reader["Manhacungcap"].ToString();
                string tennhacc = reader["Tennhacungcap"].ToString();
                TabPage tab = new TabPage();
                tab.Text = tennhacc;
                tab.Tag = manhacc;
                DataGridView dgv = new DataGridView();
                tab.Controls.Add(dgv);
                tab.Enter += Tab_Enter;
                tabView.TabPages.Add(tab);
            }

            connection.Close();

        }


        private void KhoiTaoKhachHangComboBox()
        {
            connection.Open();
            SqlCommand comn = connection.CreateCommand();
            comn.CommandType = CommandType.Text;
            comn.CommandText = $"select MaKH from KHACHHANG";
            var read = comn.ExecuteReader();
            while (read.Read())
            {
                cboMaKH.Items.Add(read.GetString(0));
            }
            connection.Close();
        }

        private void KhoiTaoPhuongThucThanhToanComboBox()
        {
            cboPTThanhToan.Items.Add("Tiền mặt");
            cboPTThanhToan.Items.Add("Ví điện tử");
            cboPTThanhToan.Items.Add("Thẻ tín dụng");
        }

        private void KhoiTaoComboBoxMaCH()
        {
            connection.Open();
            SqlCommand comn = connection.CreateCommand();
            comn.CommandType = CommandType.Text;
            comn.CommandText = $"select MaCH,Diachi from CUAHANGCIRCLEK";
            var read = comn.ExecuteReader();
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            while (read.Read())
            {
                string maCH = read["MaCH"].ToString();
                string diaChi = read["Diachi"].ToString();
                list.Add(new KeyValuePair<string, string>(maCH, diaChi));
            }
            cbCuaHang.DataSource = new BindingSource(list, null);
            cbCuaHang.DisplayMember = "Value";
            cbCuaHang.ValueMember = "Key";
            connection.Close();
        }

        private void KhoiTaoNhanVienComboBox()
        {
            connection.Open();
            SqlCommand comn = connection.CreateCommand();
            comn.CommandType = CommandType.Text;

            comn.CommandText = $"select Manv from NHANVIEN";
            var read = comn.ExecuteReader();
            while (read.Read())
            {
                cboMaNV.Items.Add(read.GetString(0));
            }
            connection.Close();
        }

        private void Tab_Enter(object sender, EventArgs e)
        {
            TabPage tab = sender as TabPage;

            tab.Width = tabView.Width;
            DataGridView dgv = tab.Controls[0] as DataGridView;
            dgv.Width = tab.Width;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.CellContentClick -= Dgv_CellContentClick;
            dgv.CellContentClick += Dgv_CellContentClick;
            dgv.DataSource = LoadSanPhamTheoNCC(tab);

        }

        private void Dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            var row = dgv.Rows[e.RowIndex];
            string masp = row.Cells["MaSP"].Value.ToString();
            bool tangchua = false;
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                if (masp == dgv.Rows[i].Cells["maSp"].Value.ToString())
                {
                    int soluong = int.Parse(dgv.Rows[i].Cells["soLuong"].Value.ToString());
                    soluong++;
                    dgv.Rows[i].Cells["soLuong"].Value = soluong;
                    tangchua = true;
                    break;
                }
            }
            if (tangchua == false)
            {
                string tensp = row.Cells["TenSP"].Value.ToString();
                string giasp = row.Cells["GiaSP"].Value.ToString();
                dgv.Rows.Add(masp, tensp, giasp, 1, giasp);
                //txtTongTien.Text = TinhTongTien().ToString();
            }
            int soLuong = int.Parse(row.Cells["soluong"].Value.ToString());
            soLuong--;
            row.Cells["soluong"].Value = soLuong;
        }

        private void KhoiTaoDChiTietHoaDon()
        {
            dgvView.Columns.Add("maSp", "Mã sản phẩm");
            dgvView.Columns.Add("tenSp", "Tên sản phẩm");
            dgvView.Columns.Add("donGia", "Đơn giá");
            dgvView.Columns.Add("soLuong", "Số lượng");
            dgvView.Columns.Add("thanhTien", "Thành tiền");
        }

        private DataTable LoadSanPhamTheoNCC(TabPage tab)
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select sp.MaSP, sp.TenSP, nk.SoLuong,sp.GiaSP, ch.Diachi from SANPHAM sp " +
                "join NHAKHO nk on sp.MaSP = nk.MaSP " +
                "join CUAHANGCIRCLEK ch on nk.MaCH = ch.MaCH " +
                $"where nk.MaCH = N'{cbCuaHang.SelectedValue}' and sp.Manhacungcap = N'{tab.Tag}'";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXoaChiTietDonHang_Click(object sender, EventArgs e)
        {
            var rowIndex = dgvView.SelectedCells[0].RowIndex;

            dgvView.Rows.RemoveAt(rowIndex);
        }

        private void dgvHoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvView.Rows[e.RowIndex];
            int soLuong = int.Parse(row.Cells["soLuong"].Value.ToString());
            double donGia = double.Parse(row.Cells["donGia"].Value.ToString());
            row.Cells["thanhTien"].Value = donGia * soLuong;
            txtTongTien.Text = TinhTongTien().ToString();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            double tong = TinhTongTien();
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ThemHoaDon";

            cmd.Parameters.AddWithValue("@MaKH", cboMaKH.Text);
            cmd.Parameters.AddWithValue("@MaNV", cboMaNV.Text);
            cmd.Parameters.AddWithValue("@NgayXuat", DateTime.Now);
            cmd.Parameters.AddWithValue("@TongTien", tong);
            cmd.Parameters.AddWithValue("@PhuongThucThanhToan", cboPTThanhToan.Text);
            cmd.Parameters.AddWithValue("@GiamGia", txtGiamGia.Text);
            cmd.Parameters.AddWithValue("@TienSauKhiGiam", txtTongTien.Text);

            var reader = cmd.ExecuteReader();
            reader.Read();
            maHD = reader["MaHD"].ToString();
            connection.Close();

            for (int i = 0; i < dgvView.Rows.Count - 1; i++)
            {
                connection.Open();
                var cmd2 = connection.CreateCommand();
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "ThemChiTietHoaDon";
                cmd2.Parameters.AddWithValue("@MaHD", maHD);
                cmd2.Parameters.AddWithValue("@MaSP", dgvView.Rows[i].Cells["maSp"].Value.ToString());
                cmd2.Parameters.AddWithValue("@SoLuong", dgvView.Rows[i].Cells["soLuong"].Value.ToString());
                cmd2.Parameters.AddWithValue("@ThanhTien", dgvView.Rows[i].Cells["thanhTien"].Value.ToString());
                cmd2.ExecuteNonQuery();
                connection.Close();
                string maCH = cbCuaHang.SelectedValue.ToString();
                string soLuong = dgvView.Rows[i].Cells["soLuong"].Value.ToString();
                string maSP = d.Rows[i].Cells["maSp"].Value.ToString();
                connection.Open();
                var cmd3 = connection.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = $"update NHAKHO set SoLuong = SoLuong - {soLuong} " +
                    $"where NHAKHO.MaSP = '{maSP}' and NHAKHO.MaCH = '{maCH}'";
                cmd3.ExecuteNonQuery();
                connection.Close();
            }
            MessageBox.Show("Thanh Toán Thành Công");
            var dgv = tabView.SelectedTab.Controls[0] as DataGridView;
            dgv.DataSource = LoadSanPhamTheoNCC(tabView.SelectedTab);

        }

        private double TinhTongTien()
        {
            double tong = 0;

            for (int i = 0; i < dgvView.Rows.Count - 1; i++)
            {
                tong += double.Parse(dgvView.Rows[i].Cells["thanhTien"].Value.ToString());

            }
            return tong;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            //frmInHoaDon frmInHoa = new frmInHoaDon(maHD, txtTongTien.Text, cbPTThanhToan.Text, txtGiamGia.Text, txtSauKhiGiamGia.Text, cbCuaHang.Text);
            //frmInHoa.Show();
            //ClearForm();
        }

        private void btnXoaChiTietDonHang_Leave(object sender, EventArgs e)
        {

        }

        private void txtGiamGia_Leave(object sender, EventArgs e)
        {
            double tongTien = TinhTongTien();
            int giamGia = int.Parse(txtGiamGia.Text);
            txtTongTien.Text = (tongTien * (100 - giamGia) / 100).ToString();

        }

        private void ClearForm()
        {
            dgvView.Rows.Clear();
            txtGiamGia.Clear();
            txtTongTien.Clear();
            txtTongTien.Clear();
        }
    }
}