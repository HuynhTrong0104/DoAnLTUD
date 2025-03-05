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
    public partial class frmHeThongCuaHang : Form
    {

        private SqlConnection connection = Connect.KetNoi();
        DataTable dataTable;
        public frmHeThongCuaHang()
        {
            InitializeComponent();
        }

        private void frmHeThongCuaHang_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvView.DataSource = LoadCuaHang();
        }

        private DataTable LoadCuaHang()
        {
            dataTable = new DataTable();
            SqlConnection conn = Connect.KetNoi();
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LayDSCuaHangCircleK";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvView_Click(object sender, EventArgs e)
        {
            int dong = dgvView.CurrentCell.RowIndex;
            txtMaCH.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            txtDiaChi.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
            txtGiayPhep.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo lệnh SQL sử dụng tham số
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CUAHANGCIRCLEK WHERE MaCH = @MaCH";
                cmd.Parameters.AddWithValue("@MaCH", txtMaCH.Text);

                // Kiểm tra MaCH tồn tại
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Mã cửa hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đóng reader và kết nối sau khi kiểm tra
                reader.Close();
                connection.Close();

                // Thêm mới
                using (var conn = Connect.KetNoi())
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ThemCHCIRCLEK";
                    cmd.Parameters.Clear(); // Xóa tham số cũ
                    cmd.Parameters.AddWithValue("@MaCH", txtMaCH.Text);
                    cmd.Parameters.AddWithValue("@DChi", txtDiaChi.Text);
                    cmd.Parameters.AddWithValue("@GiayPhep", txtGiayPhep.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Thông báo thành công
                MessageBox.Show("Thêm cửa hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại dữ liệu lên DataGridView
                dgvView.DataSource = LoadCuaHang();
            }
            catch (SqlException ex)
            {
                // Ngoại lệ liên quan đến SQL
                MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Ngoại lệ chung
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu không nhập mã cửa hàng
                string maCH = txtMaCH.Text.Trim();
                if (string.IsNullOrEmpty(maCH))
                {
                    MessageBox.Show("Vui lòng nhập hoặc chọn Mã cửa hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kết nối và thực thi lệnh xóa
                connection.Open();
                SqlCommand cmd = new SqlCommand("XoaCHCircleK", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaCH", maCH);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa cửa hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy cửa hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                // Lỗi SQL
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Lỗi chung
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            // Cập nhật lại DataGridView
            dgvView.DataSource = LoadCuaHang();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu các trường dữ liệu bị trống
                if (string.IsNullOrWhiteSpace(txtMaCH.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtGiayPhep.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở kết nối
                connection.Open();

                // Tạo lệnh SQL
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SuaCHCircleK";

                // Thêm tham số
                cmd.Parameters.AddWithValue("@MaCH", txtMaCH.Text);
                cmd.Parameters.AddWithValue("@DChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@GiayPhep", txtGiayPhep.Text);

                // Thực thi lệnh và kiểm tra kết quả
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thông tin cửa hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy cửa hàng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi liên quan đến SQL
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo đóng kết nối
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            // Cập nhật lại DataGridView
            dgvView.DataSource = LoadCuaHang();
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCH.Focus();
            txtMaCH.Clear();
            txtDiaChi.Clear();
            txtGiayPhep.Clear();
            dgvView.DataSource = LoadCuaHang();

        }

        private void frmHeThongCuaHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
    
}
