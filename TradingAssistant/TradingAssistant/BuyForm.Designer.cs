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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaCoPhieu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKhoiLuongMua = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGiaMua = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhiGiaMuaoDich = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtThoiGianMuanGiaMuaoDich = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongMua)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã cổ phiếu:";
            // 
            // txtMaCoPhieu
            // 
            this.txtMaCoPhieu.Location = new System.Drawing.Point(78, 9);
            this.txtMaCoPhieu.Name = "txtMaCoPhieu";
            this.txtMaCoPhieu.ReadOnly = true;
            this.txtMaCoPhieu.Size = new System.Drawing.Size(100, 20);
            this.txtMaCoPhieu.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số lượng:";
            // 
            // txtKhoiLuongMua
            // 
            this.txtKhoiLuongMua.Enabled = false;
            this.txtKhoiLuongMua.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtKhoiLuongMua.Location = new System.Drawing.Point(78, 40);
            this.txtKhoiLuongMua.Name = "txtKhoiLuongMua";
            this.txtKhoiLuongMua.Size = new System.Drawing.Size(100, 20);
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
            this.label3.Location = new System.Drawing.Point(51, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giá:";
            // 
            // txtGiaMua
            // 
            this.txtGiaMua.Enabled = false;
            this.txtGiaMua.Location = new System.Drawing.Point(78, 71);
            this.txtGiaMua.Name = "txtGiaMua";
            this.txtGiaMua.Size = new System.Drawing.Size(100, 20);
            this.txtGiaMua.TabIndex = 5;
            this.txtGiaMua.TextChanged += new System.EventHandler(this.txtGiaMua_TextChanged);
            this.txtGiaMua.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaMua_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Phí:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtPhiGiaMuaoDich
            // 
            this.txtPhiGiaMuaoDich.Enabled = false;
            this.txtPhiGiaMuaoDich.Location = new System.Drawing.Point(78, 102);
            this.txtPhiGiaMuaoDich.Name = "txtPhiGiaMuaoDich";
            this.txtPhiGiaMuaoDich.Size = new System.Drawing.Size(100, 20);
            this.txtPhiGiaMuaoDich.TabIndex = 7;
            this.txtPhiGiaMuaoDich.Text = "0";
            this.txtPhiGiaMuaoDich.TextChanged += new System.EventHandler(this.txtPhiGiaMuaoDich_TextChanged);
            this.txtPhiGiaMuaoDich.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhiGiaMuaoDich_KeyDown);
            this.txtPhiGiaMuaoDich.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhiGiaMuaoDich_KeyPress);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(78, 164);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(177, 133);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(17, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Thời gian:";
            // 
            // txtThoiGianMuanGiaMuaoDich
            // 
            this.txtThoiGianMuanGiaMuaoDich.Location = new System.Drawing.Point(78, 133);
            this.txtThoiGianMuanGiaMuaoDich.Name = "txtThoiGianMuanGiaMuaoDich";
            this.txtThoiGianMuanGiaMuaoDich.ReadOnly = true;
            this.txtThoiGianMuanGiaMuaoDich.Size = new System.Drawing.Size(100, 20);
            this.txtThoiGianMuanGiaMuaoDich.TabIndex = 11;
            // 
            // BuyForm
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 201);
            this.Controls.Add(this.txtThoiGianMuanGiaMuaoDich);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPhiGiaMuaoDich);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGiaMua);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKhoiLuongMua);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaCoPhieu);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "BuyForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mua cổ phiếu";
            this.Load += new System.EventHandler(this.BuyForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuyForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongMua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaCoPhieu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtKhoiLuongMua;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGiaMua;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhiGiaMuaoDich;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtThoiGianMuanGiaMuaoDich;
    }
}