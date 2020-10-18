namespace TradingAssistant
{
    partial class BuyForm
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
            this.txtMaChungKhoan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKhoiLuongMua = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGiaMua = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhiGiaoDich = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongMua)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã chứng khoán:";
            // 
            // txtMaChungKhoan
            // 
            this.txtMaChungKhoan.Location = new System.Drawing.Point(101, 9);
            this.txtMaChungKhoan.Name = "txtMaChungKhoan";
            this.txtMaChungKhoan.ReadOnly = true;
            this.txtMaChungKhoan.Size = new System.Drawing.Size(88, 20);
            this.txtMaChungKhoan.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số lượng:";
            // 
            // txtKhoiLuongMua
            // 
            this.txtKhoiLuongMua.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtKhoiLuongMua.Location = new System.Drawing.Point(101, 41);
            this.txtKhoiLuongMua.Name = "txtKhoiLuongMua";
            this.txtKhoiLuongMua.Size = new System.Drawing.Size(88, 20);
            this.txtKhoiLuongMua.TabIndex = 3;
            this.txtKhoiLuongMua.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giá:";
            // 
            // txtGiaMua
            // 
            this.txtGiaMua.Location = new System.Drawing.Point(101, 72);
            this.txtGiaMua.Name = "txtGiaMua";
            this.txtGiaMua.Size = new System.Drawing.Size(88, 20);
            this.txtGiaMua.TabIndex = 5;
            this.txtGiaMua.TextChanged += new System.EventHandler(this.txtGiaMua_TextChanged);
            this.txtGiaMua.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaMua_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Phí:";
            // 
            // txtPhiGiaoDich
            // 
            this.txtPhiGiaoDich.Location = new System.Drawing.Point(101, 103);
            this.txtPhiGiaoDich.Name = "txtPhiGiaoDich";
            this.txtPhiGiaoDich.Size = new System.Drawing.Size(88, 20);
            this.txtPhiGiaoDich.TabIndex = 7;
            this.txtPhiGiaoDich.TextChanged += new System.EventHandler(this.txtPhiGiaoDich_TextChanged);
            this.txtPhiGiaoDich.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhiGiaoDich_KeyDown);
            this.txtPhiGiaoDich.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhiGiaoDich_KeyPress);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(101, 130);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // BuyForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 162);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPhiGiaoDich);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGiaMua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKhoiLuongMua);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaChungKhoan);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "BuyForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mua";
            this.Load += new System.EventHandler(this.BuyForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuyForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongMua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaChungKhoan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtKhoiLuongMua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGiaMua;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhiGiaoDich;
        private System.Windows.Forms.Button btnConfirm;
    }
}