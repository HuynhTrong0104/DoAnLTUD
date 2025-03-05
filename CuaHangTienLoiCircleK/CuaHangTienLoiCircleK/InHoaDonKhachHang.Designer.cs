namespace CuaHangTienLoiCircleK
{
    partial class frmInHoaDonKhachHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.InHoaDonKhachHang1 = new CuaHangTienLoiCircleK.InHoaDonKhachHang();
            this.cryInHoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(370, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh Sách Hóa Đơn Khách Hàng";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cryInHoaDon
            // 
            this.cryInHoaDon.ActiveViewIndex = 0;
            this.cryInHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryInHoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryInHoaDon.Location = new System.Drawing.Point(12, 77);
            this.cryInHoaDon.Name = "cryInHoaDon";
            this.cryInHoaDon.ReportSource = this.InHoaDonKhachHang1;
            this.cryInHoaDon.Size = new System.Drawing.Size(1130, 600);
            this.cryInHoaDon.TabIndex = 2;
            // 
            // frmInHoaDonKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 674);
            this.Controls.Add(this.cryInHoaDon);
            this.Controls.Add(this.label1);
            this.Name = "frmInHoaDonKhachHang";
            this.Text = "InHoaDonKhachHang";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private InHoaDonKhachHang InHoaDonKhachHang1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer cryInHoaDon;
    }
}