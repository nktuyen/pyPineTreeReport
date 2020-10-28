namespace TradingAssistant
{
    partial class StockManager
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
            this.txtStockID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaCoPhieu = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenDoanhNghiep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbSanNiemYet = new System.Windows.Forms.ComboBox();
            this.btnAddExchanges = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKhoiLuongNiemYet = new System.Windows.Forms.NumericUpDown();
            this.txtKhoiLuongLuuHanh = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.stockDatePicker = new System.Windows.Forms.DateTimePicker();
            this.txtNgayNiemYet = new System.Windows.Forms.TextBox();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.btnUpdateStock = new System.Windows.Forms.Button();
            this.btnEditExchange = new System.Windows.Forms.Button();
            this.btnDelExchange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongNiemYet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongLuuHanh)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // txtStockID
            // 
            this.txtStockID.BackColor = System.Drawing.SystemColors.Window;
            this.txtStockID.Location = new System.Drawing.Point(140, 14);
            this.txtStockID.MaxLength = 20;
            this.txtStockID.Name = "txtStockID";
            this.txtStockID.ReadOnly = true;
            this.txtStockID.Size = new System.Drawing.Size(100, 20);
            this.txtStockID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã cổ phiếu:";
            // 
            // txtMaCoPhieu
            // 
            this.txtMaCoPhieu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaCoPhieu.Location = new System.Drawing.Point(140, 43);
            this.txtMaCoPhieu.MaxLength = 255;
            this.txtMaCoPhieu.Name = "txtMaCoPhieu";
            this.txtMaCoPhieu.Size = new System.Drawing.Size(100, 20);
            this.txtMaCoPhieu.TabIndex = 3;
            this.txtMaCoPhieu.TextChanged += new System.EventHandler(this.txtMaCoPhieu_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên doanh nghiệp:";
            // 
            // txtTenDoanhNghiep
            // 
            this.txtTenDoanhNghiep.Location = new System.Drawing.Point(140, 72);
            this.txtTenDoanhNghiep.MaxLength = 255;
            this.txtTenDoanhNghiep.Name = "txtTenDoanhNghiep";
            this.txtTenDoanhNghiep.Size = new System.Drawing.Size(222, 20);
            this.txtTenDoanhNghiep.TabIndex = 5;
            this.txtTenDoanhNghiep.TextChanged += new System.EventHandler(this.txtTenDoanhNghiep_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sàn niêm yết:";
            // 
            // cbbSanNiemYet
            // 
            this.cbbSanNiemYet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSanNiemYet.FormattingEnabled = true;
            this.cbbSanNiemYet.Location = new System.Drawing.Point(140, 101);
            this.cbbSanNiemYet.Name = "cbbSanNiemYet";
            this.cbbSanNiemYet.Size = new System.Drawing.Size(68, 21);
            this.cbbSanNiemYet.TabIndex = 7;
            this.cbbSanNiemYet.SelectedIndexChanged += new System.EventHandler(this.cbbSanNiemYet_SelectedIndexChanged);
            // 
            // btnAddExchanges
            // 
            this.btnAddExchanges.Location = new System.Drawing.Point(210, 100);
            this.btnAddExchanges.Name = "btnAddExchanges";
            this.btnAddExchanges.Size = new System.Drawing.Size(50, 23);
            this.btnAddExchanges.TabIndex = 8;
            this.btnAddExchanges.Text = "Thêm";
            this.btnAddExchanges.UseVisualStyleBackColor = true;
            this.btnAddExchanges.Click += new System.EventHandler(this.btnAddExchanges_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Khối lượng niêm yết:";
            // 
            // txtKhoiLuongNiemYet
            // 
            this.txtKhoiLuongNiemYet.Location = new System.Drawing.Point(140, 131);
            this.txtKhoiLuongNiemYet.Name = "txtKhoiLuongNiemYet";
            this.txtKhoiLuongNiemYet.Size = new System.Drawing.Size(120, 20);
            this.txtKhoiLuongNiemYet.TabIndex = 10;
            this.txtKhoiLuongNiemYet.TabStop = false;
            this.txtKhoiLuongNiemYet.ValueChanged += new System.EventHandler(this.txtKhoiLuongNiemYet_ValueChanged);
            // 
            // txtKhoiLuongLuuHanh
            // 
            this.txtKhoiLuongLuuHanh.Location = new System.Drawing.Point(140, 160);
            this.txtKhoiLuongLuuHanh.Name = "txtKhoiLuongLuuHanh";
            this.txtKhoiLuongLuuHanh.Size = new System.Drawing.Size(120, 20);
            this.txtKhoiLuongLuuHanh.TabIndex = 11;
            this.txtKhoiLuongLuuHanh.ValueChanged += new System.EventHandler(this.txtKhoiLuongLuuHanh_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Khối lượng lưu hành:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Ngày niêm yết:";
            // 
            // stockDatePicker
            // 
            this.stockDatePicker.Location = new System.Drawing.Point(259, 189);
            this.stockDatePicker.Name = "stockDatePicker";
            this.stockDatePicker.Size = new System.Drawing.Size(18, 20);
            this.stockDatePicker.TabIndex = 14;
            this.stockDatePicker.ValueChanged += new System.EventHandler(this.stockDatePicker_ValueChanged);
            // 
            // txtNgayNiemYet
            // 
            this.txtNgayNiemYet.BackColor = System.Drawing.Color.White;
            this.txtNgayNiemYet.Location = new System.Drawing.Point(140, 189);
            this.txtNgayNiemYet.Name = "txtNgayNiemYet";
            this.txtNgayNiemYet.ReadOnly = true;
            this.txtNgayNiemYet.Size = new System.Drawing.Size(120, 20);
            this.txtNgayNiemYet.TabIndex = 15;
            this.txtNgayNiemYet.TextChanged += new System.EventHandler(this.txtNgayNiemYet_TextChanged);
            // 
            // btnAddStock
            // 
            this.btnAddStock.Enabled = false;
            this.btnAddStock.Location = new System.Drawing.Point(171, 226);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(75, 23);
            this.btnAddStock.TabIndex = 16;
            this.btnAddStock.Text = "Thêm";
            this.btnAddStock.UseVisualStyleBackColor = true;
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // btnUpdateStock
            // 
            this.btnUpdateStock.Enabled = false;
            this.btnUpdateStock.Location = new System.Drawing.Point(171, 226);
            this.btnUpdateStock.Name = "btnUpdateStock";
            this.btnUpdateStock.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateStock.TabIndex = 17;
            this.btnUpdateStock.Text = "Cập nhật";
            this.btnUpdateStock.UseVisualStyleBackColor = true;
            this.btnUpdateStock.Visible = false;
            this.btnUpdateStock.Click += new System.EventHandler(this.btnUpdateStock_Click);
            // 
            // btnEditExchange
            // 
            this.btnEditExchange.Enabled = false;
            this.btnEditExchange.Location = new System.Drawing.Point(261, 100);
            this.btnEditExchange.Name = "btnEditExchange";
            this.btnEditExchange.Size = new System.Drawing.Size(50, 23);
            this.btnEditExchange.TabIndex = 8;
            this.btnEditExchange.Text = "Sửa";
            this.btnEditExchange.UseVisualStyleBackColor = true;
            this.btnEditExchange.Click += new System.EventHandler(this.btnEditExchange_Click);
            // 
            // btnDelExchange
            // 
            this.btnDelExchange.Enabled = false;
            this.btnDelExchange.Location = new System.Drawing.Point(312, 100);
            this.btnDelExchange.Name = "btnDelExchange";
            this.btnDelExchange.Size = new System.Drawing.Size(50, 23);
            this.btnDelExchange.TabIndex = 8;
            this.btnDelExchange.Text = "Xóa";
            this.btnDelExchange.UseVisualStyleBackColor = true;
            this.btnDelExchange.Click += new System.EventHandler(this.btnDelExchange_Click);
            // 
            // StockManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.txtNgayNiemYet);
            this.Controls.Add(this.stockDatePicker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtKhoiLuongLuuHanh);
            this.Controls.Add(this.txtKhoiLuongNiemYet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDelExchange);
            this.Controls.Add(this.btnEditExchange);
            this.Controls.Add(this.btnAddExchanges);
            this.Controls.Add(this.cbbSanNiemYet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTenDoanhNghiep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaCoPhieu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStockID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddStock);
            this.Controls.Add(this.btnUpdateStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "StockManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.StockManager_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StockManager_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongNiemYet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhoiLuongLuuHanh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStockID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaCoPhieu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenDoanhNghiep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbSanNiemYet;
        private System.Windows.Forms.Button btnAddExchanges;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown txtKhoiLuongNiemYet;
        private System.Windows.Forms.NumericUpDown txtKhoiLuongLuuHanh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker stockDatePicker;
        private System.Windows.Forms.TextBox txtNgayNiemYet;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.Button btnUpdateStock;
        private System.Windows.Forms.Button btnEditExchange;
        private System.Windows.Forms.Button btnDelExchange;
    }
}