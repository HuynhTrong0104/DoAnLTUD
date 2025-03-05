using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace CuaHangTienLoiCircleK
{
    public partial class frmInHoaDonKhachHang : Form
    {
        public frmInHoaDonKhachHang()
        {
            InitializeComponent();
        }

        //private void btnTim_Click(object sender, EventArgs e)
        //{
        //    // Tạo đối tượng để lấy thông tin báo cáo
        //    InHoaDonKhachHang laythongtin = new InHoaDonKhachHang();

        //    // Tạo đối tượng lưu các giá trị tham số
        //    ParameterValues para = new ParameterValues();

        //    // Tạo các đối tượng ParameterDiscreteValue
        //    ParameterDiscreteValue val = new ParameterDiscreteValue();
        //    //ParameterDiscreteValue val2 = new ParameterDiscreteValue();

        //    // Gán giá trị cho tham số
        //    //val.Value = txtMaHD.Text;  // Gán giá trị từ textbox vào tham số đầu tiên
        //    //val2.Value = txtTenKH.Text;  // Giả sử bạn có một textbox khác để lấy tên khách hàng

        //    // Thêm các giá trị tham số vào danh sách tham số
        //    para.Add(val);  // Thêm tham số MaHD
        //    //para.Add(val2); // Thêm tham số TenKH

        //    // Áp dụng các giá trị tham số cho báo cáo
        //    laythongtin.DataDefinition.ParameterFields["@MaHD"].ApplyCurrentValues(para);

        //    // Đặt nguồn dữ liệu cho CrystalReportViewer
        //    cryInHoaDon.ReportSource = laythongtin;
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
