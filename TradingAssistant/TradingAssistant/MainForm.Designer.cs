namespace TradingAssistant
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.stockTabPage = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.stockCodeListView = new System.Windows.Forms.ListView();
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExchange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.investCatagoryTagPage = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tệpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.thiếtLậpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buyStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.editStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDataFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.stockTabPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.stockTabPage);
            this.tabControl1.Controls.Add(this.investCatagoryTagPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 397);
            this.tabControl1.TabIndex = 0;
            // 
            // stockTabPage
            // 
            this.stockTabPage.Controls.Add(this.button2);
            this.stockTabPage.Controls.Add(this.button1);
            this.stockTabPage.Controls.Add(this.textBox2);
            this.stockTabPage.Controls.Add(this.label2);
            this.stockTabPage.Controls.Add(this.label1);
            this.stockTabPage.Controls.Add(this.textBox1);
            this.stockTabPage.Controls.Add(this.stockCodeListView);
            this.stockTabPage.Location = new System.Drawing.Point(4, 22);
            this.stockTabPage.Name = "stockTabPage";
            this.stockTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.stockTabPage.Size = new System.Drawing.Size(776, 371);
            this.stockTabPage.TabIndex = 0;
            this.stockTabPage.Text = "Chứng khoán";
            this.stockTabPage.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(727, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Thêm";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(675, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Tìm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(265, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(404, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên công ty:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã chứng khoán:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(101, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 1;
            // 
            // stockCodeListView
            // 
            this.stockCodeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stockCodeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNo,
            this.colCode,
            this.colCompany,
            this.colExchange,
            this.colDate});
            this.stockCodeListView.FullRowSelect = true;
            this.stockCodeListView.GridLines = true;
            this.stockCodeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.stockCodeListView.Location = new System.Drawing.Point(3, 36);
            this.stockCodeListView.Name = "stockCodeListView";
            this.stockCodeListView.Size = new System.Drawing.Size(770, 332);
            this.stockCodeListView.TabIndex = 0;
            this.stockCodeListView.UseCompatibleStateImageBehavior = false;
            this.stockCodeListView.View = System.Windows.Forms.View.Details;
            this.stockCodeListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.stockCodeListView_MouseDoubleClick);
            this.stockCodeListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stockCodeListView_MouseDown);
            this.stockCodeListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stockCodeListView_MouseUp);
            // 
            // colNo
            // 
            this.colNo.Text = "#";
            this.colNo.Width = 30;
            // 
            // colCode
            // 
            this.colCode.Text = "Mã";
            // 
            // colCompany
            // 
            this.colCompany.Text = "Tên doanh nghiệp";
            this.colCompany.Width = 400;
            // 
            // colExchange
            // 
            this.colExchange.Text = "Sàn niêm yết";
            this.colExchange.Width = 120;
            // 
            // colDate
            // 
            this.colDate.Text = "Ngày niêm yết";
            this.colDate.Width = 120;
            // 
            // investCatagoryTagPage
            // 
            this.investCatagoryTagPage.Location = new System.Drawing.Point(4, 22);
            this.investCatagoryTagPage.Name = "investCatagoryTagPage";
            this.investCatagoryTagPage.Size = new System.Drawing.Size(776, 371);
            this.investCatagoryTagPage.TabIndex = 1;
            this.investCatagoryTagPage.Text = "Danh mục đầu tư";
            this.investCatagoryTagPage.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tệpToolStripMenuItem,
            this.thiếtLậpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tệpToolStripMenuItem
            // 
            this.tệpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewFile,
            this.mnuOpenFile,
            this.closeDataFileMenuItem,
            this.toolStripMenuItem1,
            this.settingsMenuItem,
            this.toolStripSeparator1,
            this.mnuExit});
            this.tệpToolStripMenuItem.Name = "tệpToolStripMenuItem";
            this.tệpToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.tệpToolStripMenuItem.Text = "&Hệ thống";
            // 
            // mnuNewFile
            // 
            this.mnuNewFile.Name = "mnuNewFile";
            this.mnuNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewFile.Size = new System.Drawing.Size(219, 22);
            this.mnuNewFile.Text = "Tạo mới tệp dữ liệu";
            this.mnuNewFile.Click += new System.EventHandler(this.mnuNewFile_Click);
            // 
            // mnuOpenFile
            // 
            this.mnuOpenFile.Name = "mnuOpenFile";
            this.mnuOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenFile.Size = new System.Drawing.Size(219, 22);
            this.mnuOpenFile.Text = "Chọn tệp dữ liệu";
            this.mnuOpenFile.Click += new System.EventHandler(this.mnuOpenFile_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 6);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(219, 22);
            this.settingsMenuItem.Text = "Thiết lập";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(219, 22);
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // thiếtLậpToolStripMenuItem
            // 
            this.thiếtLậpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuHelp});
            this.thiếtLậpToolStripMenuItem.Name = "thiếtLậpToolStripMenuItem";
            this.thiếtLậpToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.thiếtLậpToolStripMenuItem.Text = "Trợ &giúp";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.mnuAbout.Size = new System.Drawing.Size(199, 22);
            this.mnuAbout.Text = "Thông tin";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuHelp.Size = new System.Drawing.Size(199, 22);
            this.mnuHelp.Text = "Hướng dẫn sử dụng";
            this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStockMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 26);
            // 
            // addStockMenuItem
            // 
            this.addStockMenuItem.Name = "addStockMenuItem";
            this.addStockMenuItem.Size = new System.Drawing.Size(177, 22);
            this.addStockMenuItem.Text = "Thêm chứng khoán";
            this.addStockMenuItem.Click += new System.EventHandler(this.addStockMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buyStockMenuItem,
            this.toolStripSeparator2,
            this.editStockMenuItem,
            this.deleteStockMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(146, 76);
            // 
            // buyStockMenuItem
            // 
            this.buyStockMenuItem.Name = "buyStockMenuItem";
            this.buyStockMenuItem.Size = new System.Drawing.Size(145, 22);
            this.buyStockMenuItem.Text = "Mua";
            this.buyStockMenuItem.Click += new System.EventHandler(this.buyStockMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // editStockMenuItem
            // 
            this.editStockMenuItem.Name = "editStockMenuItem";
            this.editStockMenuItem.Size = new System.Drawing.Size(145, 22);
            this.editStockMenuItem.Text = "Sửa thông tin";
            this.editStockMenuItem.Click += new System.EventHandler(this.editStockMenuItem_Click);
            // 
            // deleteStockMenuItem
            // 
            this.deleteStockMenuItem.Name = "deleteStockMenuItem";
            this.deleteStockMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteStockMenuItem.Text = "Xóa";
            this.deleteStockMenuItem.Click += new System.EventHandler(this.deleteStockMenuItem_Click);
            // 
            // closeDataFileMenuItem
            // 
            this.closeDataFileMenuItem.Enabled = false;
            this.closeDataFileMenuItem.Name = "closeDataFileMenuItem";
            this.closeDataFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.closeDataFileMenuItem.Size = new System.Drawing.Size(219, 22);
            this.closeDataFileMenuItem.Text = "Đóng tệp dữ liệu";
            this.closeDataFileMenuItem.Click += new System.EventHandler(this.closeDataFileMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 460);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TradingAssistant";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.stockTabPage.ResumeLayout(false);
            this.stockTabPage.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage stockTabPage;
        private System.Windows.Forms.ListView stockCodeListView;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colCompany;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tệpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem thiếtLậpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addStockMenuItem;
        private System.Windows.Forms.TabPage investCatagoryTagPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem editStockMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStockMenuItem;
        private System.Windows.Forms.ColumnHeader colExchange;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem buyStockMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeDataFileMenuItem;
    }
}

