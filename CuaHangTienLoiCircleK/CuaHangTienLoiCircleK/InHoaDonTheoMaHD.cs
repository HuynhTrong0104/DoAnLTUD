using CrystalDecisions.Shared;
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
    public partial class frmInHoaDonTheoMaHD : Form
    {
        public frmInHoaDonTheoMaHD()
        {
            InitializeComponent();
        }


        private void btnTim_Click(object sender, EventArgs e)
        {
            InHoaDon layThongTin = new InHoaDon();

            ParameterValues para = new ParameterValues();

            ParameterDiscreteValue val = new ParameterDiscreteValue();

            val.Value = cboMaHD.Text;

            para.Add(val);

            layThongTin.DataDefinition.ParameterFields["@MaHD"].ApplyCurrentValues(para);

            cryInHoaDon.ReportSource = layThongTin;
        }
        SqlConnection connection = Connect.KetNoi();

        private void frmInHoaDonTheoMaHD_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand comn = connection.CreateCommand();
            comn.CommandType = CommandType.Text;
            comn.CommandText = $"select MaHD from HOADON";
            var read = comn.ExecuteReader();
            while (read.Read())
            {
                cboMaHD.Items.Add(read.GetString(0));
            }
            connection.Close();
        }
    }
}
