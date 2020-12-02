namespace TradingAssistant
{
    partial class SettingForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhiGiaoDichMua = new System.Windows.Forms.TextBox();
            this.txtPhiGiaoDichBan = new System.Windows.Forms.TextBox();
            this.txtThueTNCN = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbBaoGomPhiMua = new System.Windows.Forms.CheckBox();
            this.chbBaoGomPhiBan = new System.Windows.Forms.CheckBox();
            this.chbBaoGomThuThuNhap = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtThueTNCN);
            this.groupBox1.Controls.Add(this.txtPhiGiaoDichBan);
            this.groupBox1.Controls.Add(this.txtPhiGiaoDichMua);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thuế - Phí";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Thuế thu nhập(%):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Phí giao dịch bán(%):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Phí giao dịch mua(%):";
            // 
            // txtPhiGiaoDichMua
            // 
            this.txtPhiGiaoDichMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhiGiaoDichMua.Location = new System.Drawing.Point(148, 17);
            this.txtPhiGiaoDichMua.Name = "txtPhiGiaoDichMua";
            this.txtPhiGiaoDichMua.Size = new System.Drawing.Size(100, 20);
            this.txtPhiGiaoDichMua.TabIndex = 6;
            // 
            // txtPhiGiaoDichBan
            // 
            this.txtPhiGiaoDichBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhiGiaoDichBan.Location = new System.Drawing.Point(148, 42);
            this.txtPhiGiaoDichBan.Name = "txtPhiGiaoDichBan";
            this.txtPhiGiaoDichBan.Size = new System.Drawing.Size(100, 20);
            this.txtPhiGiaoDichBan.TabIndex = 7;
            // 
            // txtThueTNCN
            // 
            this.txtThueTNCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThueTNCN.Location = new System.Drawing.Point(148, 67);
            this.txtThueTNCN.Name = "txtThueTNCN";
            this.txtThueTNCN.Size = new System.Drawing.Size(100, 20);
            this.txtThueTNCN.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbBaoGomThuThuNhap);
            this.groupBox2.Controls.Add(this.chbBaoGomPhiBan);
            this.groupBox2.Controls.Add(this.chbBaoGomPhiMua);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tính toán";
            // 
            // chbBaoGomPhiMua
            // 
            this.chbBaoGomPhiMua.AutoSize = true;
            this.chbBaoGomPhiMua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbBaoGomPhiMua.Location = new System.Drawing.Point(35, 20);
            this.chbBaoGomPhiMua.Name = "chbBaoGomPhiMua";
            this.chbBaoGomPhiMua.Size = new System.Drawing.Size(156, 17);
            this.chbBaoGomPhiMua.TabIndex = 0;
            this.chbBaoGomPhiMua.Text = "Bao gồm phí giao dịch mua";
            this.chbBaoGomPhiMua.UseVisualStyleBackColor = true;
            // 
            // chbBaoGomPhiBan
            // 
            this.chbBaoGomPhiBan.AutoSize = true;
            this.chbBaoGomPhiBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbBaoGomPhiBan.Location = new System.Drawing.Point(35, 40);
            this.chbBaoGomPhiBan.Name = "chbBaoGomPhiBan";
            this.chbBaoGomPhiBan.Size = new System.Drawing.Size(154, 17);
            this.chbBaoGomPhiBan.TabIndex = 1;
            this.chbBaoGomPhiBan.Text = "Bao gồm phí giao dịch bán";
            this.chbBaoGomPhiBan.UseVisualStyleBackColor = true;
            // 
            // chbBaoGomThuThuNhap
            // 
            this.chbBaoGomThuThuNhap.AutoSize = true;
            this.chbBaoGomThuThuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbBaoGomThuThuNhap.Location = new System.Drawing.Point(35, 60);
            this.chbBaoGomThuThuNhap.Name = "chbBaoGomThuThuNhap";
            this.chbBaoGomThuThuNhap.Size = new System.Drawing.Size(131, 17);
            this.chbBaoGomThuThuNhap.TabIndex = 2;
            this.chbBaoGomThuThuNhap.Text = "Bao gồm thế thu nhập";
            this.chbBaoGomThuThuNhap.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(128, 207);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "Lưu";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SettingForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 237);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thiết lập hệ thống";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtThueTNCN;
        private System.Windows.Forms.TextBox txtPhiGiaoDichBan;
        private System.Windows.Forms.TextBox txtPhiGiaoDichMua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbBaoGomThuThuNhap;
        private System.Windows.Forms.CheckBox chbBaoGomPhiBan;
        private System.Windows.Forms.CheckBox chbBaoGomPhiMua;
        private System.Windows.Forms.Button OKButton;
    }
}