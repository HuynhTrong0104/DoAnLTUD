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
using System.Windows.Forms.DataVisualization.Charting;

namespace CuaHangTienLoiCircleK
{
    public partial class frmThongKeSanPham : Form
    {
        public frmThongKeSanPham()
        {
            InitializeComponent();
        }

        private void frmDoanhThuSanPham_Load(object sender, EventArgs e)
        {
            dgvView.ReadOnly = true;
            dgvView.EditMode = DataGridViewEditMode.EditProgrammatically;
            DataTable dataTable = GetProductQuantities();    
            dgvView.DataSource = dataTable;
            BindChart(dataTable);
        }
        

        private DataTable GetProductQuantities()
        {
            string sqlcon = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCUAHANGTLCIRCLEKS;Integrated Security=True";
            DataTable dataTable = new DataTable();

            string query = @"
                            SELECT 
                                sp.TenSP AS [Tên Sản Phẩm], 
                                sl.SoLuong AS [Số Lượng]
                            FROM 
                                SanPham sp
                            JOIN 
                                NHAKHO sl
                            ON 
                                sp.MaSP = sl.MaSP";
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlcon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return dataTable;
        }


        private void BindChart(DataTable dataTable)
        {
            thongke.Series.Clear();

            Series series = thongke.Series.Add("Số Lượng");
            series.ChartType = SeriesChartType.Column;
            series.Points.DataBind(dataTable.DefaultView, "Tên Sản Phẩm", "Số Lượng", null);
        }

        private void frmDoanhThuSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Do you want to close?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }
    }
}
