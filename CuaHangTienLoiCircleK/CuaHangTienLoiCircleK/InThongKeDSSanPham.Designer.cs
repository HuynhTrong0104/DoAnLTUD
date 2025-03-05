namespace CuaHangTienLoiCircleK
{
    partial class frmInThongKeDSSanPham
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
            this.cryView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.InThongKeSanPham1 = new CuaHangTienLoiCircleK.InThongKeSanPham();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(363, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống Kê Danh Sách Sản Phẩm";
            // 
            // cryView
            // 
            this.cryView.ActiveViewIndex = 0;
            this.cryView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cryView.Cursor = System.Windows.Forms.Cursors.Default;
            this.cryView.Location = new System.Drawing.Point(2, 64);
            this.cryView.Name = "cryView";
            this.cryView.ReportSource = this.InThongKeSanPham1;
            this.cryView.Size = new System.Drawing.Size(1109, 696);
            this.cryView.TabIndex = 1;
            // 
            // frmInThongKeDSSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 761);
            this.Controls.Add(this.cryView);
            this.Controls.Add(this.label1);
            this.Name = "frmInThongKeDSSanPham";
            this.Text = "InThongKeDSSanPham";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer cryView;
        private InThongKeSanPham InThongKeSanPham1;
    }
}