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
    public partial class frmKhoHang : Form
    {
        LuongVaKhoHang nhakho = new LuongVaKhoHang();
        public frmKhoHang()
        {
            InitializeComponent();

        }
        private SqlConnection conn = Connect.KetNoi();
        DataTable datatable;

        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvView.DataSource = LoadNhaKho();
            LoadCuaHang();
            LoadSanPham();
        }
        private DataTable LoadNhaKho()
        {
            datatable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LDSNhaKho";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(datatable);
            conn.Close();
            return datatable;
        }

        private void LoadSanPham()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from sanpham";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string maSP = reader.GetString(0);
                cboMaSP.Items.Add(maSP);
            }
            conn.Close();
        }
        private void LoadCuaHang()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from cuahangcirclek";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string maCH = reader.GetString(0);
                cboMaCuaHang.Items.Add(maCH);
            }
            conn.Close();
        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Lấy chỉ số hàng được chọn
                var rowIndex = dgvView.SelectedCells[0].RowIndex;
                var row = dgvView.Rows[rowIndex];

                // Gán giá trị vào TextBox và ComboBox
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString().Trim();
                string masp = row.Cells["MaSP"].Value.ToString().Trim();
                string mach = row.Cells["MaCH"].Value.ToString().Trim();

                // Sử dụng using để quản lý kết nối
                using (SqlConnection conn = new SqlConnection("Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCHTLCIRCLEK;Integrated Security=True;Encrypt=False")) // Thay `connectionString` bằng chuỗi kết nối của bạn
                {
                    conn.Open();

                    // Lấy thông tin sản phẩm
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM SANPHAM WHERE MaSP = @MaSP", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", masp);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cboMaSP.Text = reader.GetString(0); // Gán giá trị vào combobox
                            }
                        }
                    }

                    // Lấy thông tin cửa hàng
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM CUAHANGCIRCLEK WHERE MaCH = @MaCH", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCH", mach);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cboMaCuaHang.Text = reader.GetString(0); // Gán giá trị vào combobox
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn == null)
                {
                    MessageBox.Show("Kết nối cơ sở dữ liệu chưa được thiết lập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở kết nối nếu chưa mở
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                // Tạo đối tượng SqlCommand và thiết lập các thuộc tính
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ThemNhaKho";

                    // Thêm tham số vào thủ tục lưu trữ
                    cmd.Parameters.AddWithValue("@sMaSP", cboMaSP.Text);
                    cmd.Parameters.AddWithValue("@MaCH", cboMaCuaHang.Text);

                    // Xử lý và kiểm tra giá trị nhập từ TextBox
                    if (int.TryParse(txtSoLuong.Text, out int soLuong))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    }
                    else
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Thực thi câu lệnh
                    cmd.ExecuteNonQuery();

                    // Thông báo thành công
                    //conn.Close();
                    MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại DataGridView
                    dgvView.DataSource = LoadNhaKho();
                }
            }
            catch (SqlException ex)
            {
                // Lỗi liên quan đến SQL
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Lỗi khác
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực thi
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ hàng được chọn
                var row = dgvView.SelectedCells[0].RowIndex;
                var maSP = dgvView.Rows[row].Cells["MaSP"].Value?.ToString().Trim();
                var maCH = dgvView.Rows[row].Cells["MaCH"].Value?.ToString().Trim();


                // Thực hiện kết nối và xóa dữ liệu
                using (conn)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "XoaNhaKho";
                        cmd.Parameters.AddWithValue("@sMaSP", maSP);
                        cmd.Parameters.AddWithValue("@MaCH", maCH);

                        int rowsAffected = cmd.ExecuteNonQuery(); // Số dòng bị ảnh hưởng

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Cập nhật lại DataGridView
                dgvView.DataSource = LoadNhaKho();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực thi
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); // Mở kết nối
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CapNhapNhaKho";
                cmd.Parameters.AddWithValue("@sMaSP", cboMaSP.Text);
                cmd.Parameters.AddWithValue("@MaCH", cboMaCuaHang.Text);
                cmd.Parameters.AddWithValue("@SoLuong", txtSoLuong.Text);

                cmd.ExecuteNonQuery(); 
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvView.DataSource = LoadNhaKho();
            }
            catch (SqlException sqlEx)
            {
                // Xử lý lỗi từ SQL Server
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối dù có lỗi hay không
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        private void frmKhoHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
