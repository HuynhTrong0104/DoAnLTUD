namespace CuaHangTienLoiCircleK
{
    partial class frmTraCuuHoaDon
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
            this.txtSauKhiGiamGia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTimtiep = new System.Windows.Forms.Button();
            this.btnTraCuu = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.dgvTraCuuHoaDon = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSauKhiGiamGia
            // 
            this.txtSauKhiGiamGia.Enabled = false;
            this.txtSauKhiGiamGia.Location = new System.Drawing.Point(662, 293);
            this.txtSauKhiGiamGia.Name = "txtSauKhiGiamGia";
            this.txtSauKhiGiamGia.Size = new System.Drawing.Size(251, 26);
            this.txtSauKhiGiamGia.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(420, 293);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(236, 19);
            this.label7.TabIndex = 36;
            this.label7.Text = "Tổng tiền khi đã áp dụng mã giảm giá:";
            // 
            // txtGiamGia
            // 
            this.txtGiamGia.Enabled = false;
            this.txtGiamGia.Location = new System.Drawing.Point(129, 201);
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.Size = new System.Drawing.Size(184, 26);
            this.txtGiamGia.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(34, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 34;
            this.label5.Text = "Giảm Giá";
            // 
            // btnTimtiep
            // 
            this.btnTimtiep.BackColor = System.Drawing.Color.Transparent;
            this.btnTimtiep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimtiep.Location = new System.Drawing.Point(265, 122);
            this.btnTimtiep.Name = "btnTimtiep";
            this.btnTimtiep.Size = new System.Drawing.Size(98, 41);
            this.btnTimtiep.TabIndex = 33;
            this.btnTimtiep.Text = "Clear";
            this.btnTimtiep.UseVisualStyleBackColor = false;
            this.btnTimtiep.Click += new System.EventHandler(this.btnTimtiep_Click);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.BackColor = System.Drawing.Color.Transparent;
            this.btnTraCuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraCuu.Location = new System.Drawing.Point(129, 122);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(84, 41);
            this.btnTraCuu.TabIndex = 32;
            this.btnTraCuu.Text = "Tra Cứu";
            this.btnTraCuu.UseVisualStyleBackColor = false;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTongTien.Location = new System.Drawing.Point(552, 251);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(104, 19);
            this.lblTongTien.TabIndex = 31;
            this.lblTongTien.Text = "Tổng thành tiền:";
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Enabled = false;
            this.txtThanhTien.Location = new System.Drawing.Point(662, 248);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.Size = new System.Drawing.Size(251, 26);
            this.txtThanhTien.TabIndex = 30;
            // 
            // dgvTraCuuHoaDon
            // 
            this.dgvTraCuuHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTraCuuHoaDon.BackgroundColor = System.Drawing.Color.IndianRed;
            this.dgvTraCuuHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraCuuHoaDon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTraCuuHoaDon.Location = new System.Drawing.Point(0, 332);
            this.dgvTraCuuHoaDon.Name = "dgvTraCuuHoaDon";
            this.dgvTraCuuHoaDon.RowHeadersWidth = 51;
            this.dgvTraCuuHoaDon.RowTemplate.Height = 24;
            this.dgvTraCuuHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTraCuuHoaDon.Size = new System.Drawing.Size(1166, 168);
            this.dgvTraCuuHoaDon.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(34, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 19);
            this.label1.TabIndex = 28;
            this.label1.Text = "Mã Hóa Đơn:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(129, 65);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(234, 26);
            this.txtMaHD.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(442, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 33);
            this.label2.TabIndex = 38;
            this.label2.Text = "Tra Cứu Hóa Đơn";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Firebrick;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1056, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 28);
            this.button1.TabIndex = 39;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::CuaHangTienLoiCircleK.Properties.Resources.logo1;
            this.pictureBox2.Location = new System.Drawing.Point(1058, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(96, 116);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 45;
            this.pictureBox2.TabStop = false;
            // 
            // frmTraCuuHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(1166, 500);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSauKhiGiamGia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtGiamGia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnTimtiep);
            this.Controls.Add(this.btnTraCuu);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.txtThanhTien);
            this.Controls.Add(this.dgvTraCuuHoaDon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaHD);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTraCuuHoaDon";
            this.Text = "TraCuuHoaDon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTraCuuHoaDon_FormClosing);
            this.Load += new System.EventHandler(this.frmTraCuuHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSauKhiGiamGia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTimtiep;
        private System.Windows.Forms.Button btnTraCuu;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.DataGridView dgvTraCuuHoaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}