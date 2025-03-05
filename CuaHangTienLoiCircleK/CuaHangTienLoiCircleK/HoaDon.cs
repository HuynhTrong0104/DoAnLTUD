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
        private void KhoaTab(TabPage tabPage)
        {
            foreach (Control control in tabPage.Controls)
            {
                if (control is DataGridView dgv)
                {
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                }
                else if (control is TextBox txt)
                {
                    txt.ReadOnly = true;
                }
                else if (control is ComboBox cbo)
                {
                    cbo.Enabled = false;
                }
                // Thêm các loại control khác nếu cần
            }
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            foreach (TabPage tabPage in tabView.TabPages)
            {
                KhoaTab(tabPage);
            }
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
                dgv.ReadOnly = true;
                dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            for (int i = 0; i < dgvView.Rows.Count - 1; i++)
            {
                if (masp == dgvView.Rows[i].Cells["maSp"].Value.ToString())
                {
                    int soluong = int.Parse(dgvView.Rows[i].Cells["soLuong"].Value.ToString());
                    soluong++;
                    dgvView.Rows[i].Cells["soLuong"].Value = soluong;
                    tangchua = true;
                    break;
                }
            }
            if (tangchua == false)
            {
                string tensp = row.Cells["TenSP"].Value.ToString();
                string giasp = row.Cells["GiaSP"].Value.ToString();
                dgvView.Rows.Add(masp, tensp, giasp, 1, giasp);
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
            //dgvView.DataSource 
        }

        private void dgvHoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvView.Rows[e.RowIndex];
            int soLuong = int.Parse(row.Cells["soLuong"].Value.ToString());
            double donGia = double.Parse(row.Cells["donGia"].Value.ToString());
            row.Cells["thanhTien"].Value = donGia * soLuong;
            txtSoTien.Text = TinhTongTien().ToString();

        }
        public bool KiemTraDuLieuDauVao()
        {
            errorProvider1.Clear();

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(cboMaKH.Text))
            {
                errorProvider1.SetError(cboMaKH, "Vui lòng chọn Mã Khách Hàng.");
                isValid = false;

            }

            if (string.IsNullOrWhiteSpace(cboMaNV.Text))
            {
                errorProvider1.SetError(cboMaNV, "Vui lòng chọn Mã Nhân Viên.");
                isValid = false;
            }


            if (string.IsNullOrWhiteSpace(txtGiamGia.Text) || !double.TryParse(txtGiamGia.Text, out _))
            {
                errorProvider1.SetError(txtGiamGia, "Vui lòng nhập Giảm Giá hợp lệ.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(cboPTThanhToan.Text))
            {
                errorProvider1.SetError(cboPTThanhToan, "Vui lòng chọn Phương Thức Thanh Toán.");
                isValid = false;
            }

            if (dgvView.Rows.Count <= 1) // Chỉ có dòng trống
            {
                MessageBox.Show("Vui lòng thêm sản phẩm vào giỏ hàng trước khi thanh toán.", "Lỗi");
                isValid = false;
            }
            return isValid;
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieuDauVao())
                {
                    return;
                }
                double tong = TinhTongTien();

                // Mở kết nối một lần
                connection.Open();

                // Thực thi lệnh thêm hóa đơn
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThemHoaDon";

                cmd.Parameters.AddWithValue("@MaKH", cboMaKH.Text);
                cmd.Parameters.AddWithValue("@MaNV", cboMaNV.Text);
                cmd.Parameters.AddWithValue("@NgayXuat", DateTime.Now);
                cmd.Parameters.AddWithValue("@SoTien", Convert.ToDouble(txtSoTien.Text)); 
                cmd.Parameters.AddWithValue("@PhuongThucThanhToan", cboPTThanhToan.Text);
                cmd.Parameters.AddWithValue("@GiamGia", Convert.ToDouble(txtGiamGia.Text)); 
                cmd.Parameters.AddWithValue("@TongTien", Convert.ToDouble(txtTongTien.Text));

                var reader = cmd.ExecuteReader();
                reader.Read();
                string maHD = reader["MaHD"].ToString(); 
                reader.Close();

                // Thực thi lệnh thêm chi tiết hóa đơn
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống

                    var cmd2 = connection.CreateCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "ThemChiTietHoaDon";
                    cmd2.Parameters.AddWithValue("@MaHD", maHD);
                    cmd2.Parameters.AddWithValue("@MaSP", row.Cells["maSp"].Value.ToString());
                    cmd2.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(row.Cells["soLuong"].Value)); 
                    cmd2.Parameters.AddWithValue("@ThanhTien", Convert.ToDouble(row.Cells["thanhTien"].Value)); 
                    cmd2.ExecuteNonQuery();
                }

                // Cập nhật kho
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống

                    string maCH = cbCuaHang.SelectedValue.ToString();
                    int soLuong = Convert.ToInt32(row.Cells["soLuong"].Value); 
                    string maSP = row.Cells["maSp"].Value.ToString();

                    var cmd3 = connection.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "UPDATE NHAKHO SET SoLuong = SoLuong - @SoLuong " +
                                       "WHERE NHAKHO.MaSP = @MaSP AND NHAKHO.MaCH = @MaCH";

                    cmd3.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd3.Parameters.AddWithValue("@MaSP", maSP);
                    cmd3.Parameters.AddWithValue("@MaCH", maCH);

                    cmd3.ExecuteNonQuery();
                }

                MessageBox.Show("Thanh Toán Thành Công");

                // Làm mới DataGridView sau khi thanh toán
                var dgv = tabView.SelectedTab.Controls[0] as DataGridView;
                dgv.DataSource = LoadSanPhamTheoNCC(tabView.SelectedTab);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        //private double TinhTongTien()
        //{
        //    double tong = 0;

        //    for (int i = 0; i < dgvView.Rows.Count - 1; i++)
        //    {
        //        tong += double.Parse(dgvView.Rows[i].Cells["sotien"].Value.ToString());

        //    }
        //    return tong;
        //}

        //private void btnInHoaDon_Click(object sender, EventArgs e)
        //{
        //    frmInHoaDon frmInHoa = new frmInHoaDon(maHD, txtTongTien.Text, cbPTThanhToan.Text, txtGiamGia.Text, txtSauKhiGiamGia.Text, cbCuaHang.Text);
        //    frmInHoa.Show();
        //    ClearForm();
        //}

        private double TinhTongTien()
        {
            double tong = 0;

            foreach (DataGridViewRow row in dgvView.Rows)
            {
                if (row.IsNewRow) continue;

                var cellValue = row.Cells["thanhTien"].Value;
                if (cellValue != null && double.TryParse(cellValue.ToString(), out double soTien))
                {
                    tong += soTien;
                }
            }

            return tong;
        }



        private void txtGiamGia_Leave(object sender, EventArgs e)
        {
            try
            {
                double sotien = TinhTongTien();

                // Kiểm tra và xử lý giảm giá hợp lệ
                if (int.TryParse(txtGiamGia.Text, out int giamGia) && giamGia >= 0 && giamGia <= 100)
                {
                    double tongSauGiam = sotien * (100 - giamGia) / 100;
                    txtTongTien.Text = tongSauGiam.ToString(); // Hiển thị 2 chữ số thập phân
                }
                else
                {
                    MessageBox.Show("Giảm giá không hợp lệ. Vui lòng nhập giá trị từ 0 đến 100.", "Lỗi");
                    txtGiamGia.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ClearForm()
        {
            dgvView.Rows.Clear();
            txtGiamGia.Clear();
            txtTongTien.Clear();
            txtSoTien.Clear();
        }

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            frmInHoaDonKhachHang inhoadon = new frmInHoaDonKhachHang();
            inhoadon.Show();
        }
    }
}