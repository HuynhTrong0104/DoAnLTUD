using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangTienLoiCircleK
{
    public partial class frmNhanVien : Form
    {
        DBKetNoi kn = new DBKetNoi();
        private SqlConnection connection = Connect.KetNoi();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
                string Manv = txtMaNV.Text;
                string Tennv = txtTenNV.Text;
                string NgaySinh = dtpNgaySinh.Value.Date.ToString("MM/dd/yyyy");
                string Diachi = txtDiaChi.Text;
                string SDTNhanVien = mtxtSDT.Text;

                string Phai;
                if (rdNam.Checked == true)
                {
                    Phai = rdNam.Text;
                }
                else
                {
                    Phai = rdNu.Text;
                }

                if (kn.ThemNhanVien(Manv, Tennv, NgaySinh, Diachi, SDTNhanVien, Phai) >= 0)
                {
                    MessageBox.Show("Thêm Thành Công");
                    dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");

                }
                else
                {
                    MessageBox.Show("Thêm Thất Bại");
                }
            
        }

        private void dgvView_Click(object sender, EventArgs e)
        {
            int dong = dgvView.CurrentCell.RowIndex;
            txtMaNV.Text = dgvView.Rows[dong].Cells[0].Value.ToString();
            txtTenNV.Text = dgvView.Rows[dong].Cells[1].Value.ToString();
            dtpNgaySinh.Text = dgvView.Rows[dong].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvView.Rows[dong].Cells[3].Value.ToString();
            mtxtSDT.Text = dgvView.Rows[dong].Cells[4].Value.ToString();
            if (dgvView.Rows[dong].Cells[5].Value.ToString().Trim() == "Nam")
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "XoaDSNhanVien";
            cmd.Parameters.AddWithValue("@Manv", maNV);
            cmd.ExecuteNonQuery();
            connection.Close(); 
            dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string Manv = txtMaNV.Text;
            string Tennv = txtTenNV.Text;
            string NgaySinh = dtpNgaySinh.Value.Date.ToString("MM/dd/yyyy");
            string Diachi = txtDiaChi.Text;
            string SDTNhanVien = mtxtSDT.Text.Trim();

            string Phai;
            if (rdNam.Checked == true)
            {
                Phai = rdNam.Text;
            }
            else
            {
                Phai = rdNu.Text;
            }

            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SuaNhanVien";
            cmd.Parameters.AddWithValue("@Manv", Manv);
            cmd.Parameters.AddWithValue("@Tennv", Tennv);
            cmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
            cmd.Parameters.AddWithValue("@Diachi", Diachi);
            cmd.Parameters.AddWithValue("@SDTNhanVien", SDTNhanVien);
            cmd.Parameters.AddWithValue("@Phai", Phai);


            cmd.ExecuteNonQuery();
            connection.Close();
            dgvView.DataSource = kn.LayDSNhanVien("LayDSNhanVien");
        }
    }
}
