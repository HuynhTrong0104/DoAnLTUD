namespace CuaHangTienLoiCircleK
{
    partial class frmInHoaDonTheoMaHD
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
            this.btnTim = new System.Windows.Forms.Button();
            this.cryInHoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.InHoaDon1 = new CuaHangTienLoiCircleK.InHoaDon();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMaHD = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(443, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "In Hóa Đơn";
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(274, 56);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(98, 23);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm Kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // cryInHoaDon
            // 
            this.cryInHoaDon.ActiveViewIndex = 0;
            this.cryInHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryInHoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryInHoaDon.Location = new System.Drawing.Point(3, 111);
            this.cryInHoaDon.Name = "cryInHoaDon";
            this.cryInHoaDon.ReportSource = this.InHoaDon1;
            this.cryInHoaDon.Size = new System.Drawing.Size(1082, 535);
            this.cryInHoaDon.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = " Mã Hóa Đơn:";
            // 
            // cboMaHD
            // 
            this.cboMaHD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaHD.FormattingEnabled = true;
            this.cboMaHD.Location = new System.Drawing.Point(114, 60);
            this.cboMaHD.Name = "cboMaHD";
            this.cboMaHD.Size = new System.Drawing.Size(141, 21);
            this.cboMaHD.TabIndex = 5;
            // 
            // frmInHoaDonTheoMaHD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 647);
            this.Controls.Add(this.cboMaHD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cryInHoaDon);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.label1);
            this.Name = "frmInHoaDonTheoMaHD";
            this.Text = "InHoaDonTheoMaHD";
            this.Load += new System.EventHandler(this.frmInHoaDonTheoMaHD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTim;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer cryInHoaDon;
        private InHoaDon InHoaDon1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMaHD;
    }
}