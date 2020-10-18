namespace TradingAssistant
{
    partial class ExchangesManager
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMaSan = new System.Windows.Forms.TextBox();
            this.txtTenSan = new System.Windows.Forms.TextBox();
            this.txtTrangWeb = new System.Windows.Forms.TextBox();
            this.btnAddExch = new System.Windows.Forms.Button();
            this.btnUpdateExch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã sàn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên sàn:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Website:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(74, 6);
            this.txtID.MaxLength = 32;
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 5;
            // 
            // txtMaSan
            // 
            this.txtMaSan.Location = new System.Drawing.Point(74, 37);
            this.txtMaSan.MaxLength = 255;
            this.txtMaSan.Name = "txtMaSan";
            this.txtMaSan.Size = new System.Drawing.Size(100, 20);
            this.txtMaSan.TabIndex = 6;
            this.txtMaSan.TextChanged += new System.EventHandler(this.txtMaSan_TextChanged);
            // 
            // txtTenSan
            // 
            this.txtTenSan.Location = new System.Drawing.Point(74, 68);
            this.txtTenSan.MaxLength = 255;
            this.txtTenSan.Name = "txtTenSan";
            this.txtTenSan.Size = new System.Drawing.Size(201, 20);
            this.txtTenSan.TabIndex = 7;
            this.txtTenSan.TextChanged += new System.EventHandler(this.txtTenSan_TextChanged);
            // 
            // txtTrangWeb
            // 
            this.txtTrangWeb.Location = new System.Drawing.Point(74, 99);
            this.txtTrangWeb.MaxLength = 255;
            this.txtTrangWeb.Name = "txtTrangWeb";
            this.txtTrangWeb.Size = new System.Drawing.Size(202, 20);
            this.txtTrangWeb.TabIndex = 8;
            this.txtTrangWeb.TextChanged += new System.EventHandler(this.txtTrangWeb_TextChanged);
            // 
            // btnAddExch
            // 
            this.btnAddExch.Enabled = false;
            this.btnAddExch.Location = new System.Drawing.Point(110, 129);
            this.btnAddExch.Name = "btnAddExch";
            this.btnAddExch.Size = new System.Drawing.Size(75, 23);
            this.btnAddExch.TabIndex = 9;
            this.btnAddExch.Text = "Thêm";
            this.btnAddExch.UseVisualStyleBackColor = true;
            this.btnAddExch.Click += new System.EventHandler(this.btnAddExch_Click);
            // 
            // btnUpdateExch
            // 
            this.btnUpdateExch.Enabled = false;
            this.btnUpdateExch.Location = new System.Drawing.Point(110, 129);
            this.btnUpdateExch.Name = "btnUpdateExch";
            this.btnUpdateExch.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateExch.TabIndex = 9;
            this.btnUpdateExch.Text = "Cập nhật";
            this.btnUpdateExch.UseVisualStyleBackColor = true;
            this.btnUpdateExch.Click += new System.EventHandler(this.btnUpdateExch_Click);
            // 
            // ExchangesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 162);
            this.Controls.Add(this.txtTrangWeb);
            this.Controls.Add(this.txtTenSan);
            this.Controls.Add(this.txtMaSan);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateExch);
            this.Controls.Add(this.btnAddExch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "ExchangesManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ExchangesManager_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExchangesManager_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMaSan;
        private System.Windows.Forms.TextBox txtTenSan;
        private System.Windows.Forms.TextBox txtTrangWeb;
        private System.Windows.Forms.Button btnAddExch;
        private System.Windows.Forms.Button btnUpdateExch;
    }
}