namespace TradingAssistant
{
    partial class PortfolioDetailForm
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
            this.portfolioDetailLV = new System.Windows.Forms.ListView();
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCommand = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // portfolioDetailLV
            // 
            this.portfolioDetailLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portfolioDetailLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNo,
            this.colDate,
            this.colCount,
            this.colPrice,
            this.colAmount,
            this.colFee,
            this.colTax,
            this.colCommand});
            this.portfolioDetailLV.FullRowSelect = true;
            this.portfolioDetailLV.GridLines = true;
            this.portfolioDetailLV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.portfolioDetailLV.HideSelection = false;
            this.portfolioDetailLV.Location = new System.Drawing.Point(4, 4);
            this.portfolioDetailLV.Name = "portfolioDetailLV";
            this.portfolioDetailLV.ShowItemToolTips = true;
            this.portfolioDetailLV.Size = new System.Drawing.Size(715, 311);
            this.portfolioDetailLV.TabIndex = 2;
            this.portfolioDetailLV.UseCompatibleStateImageBehavior = false;
            this.portfolioDetailLV.View = System.Windows.Forms.View.Details;
            // 
            // colNo
            // 
            this.colNo.Text = "#";
            this.colNo.Width = 30;
            // 
            // colDate
            // 
            this.colDate.Text = "Ngày giao dịch";
            this.colDate.Width = 110;
            // 
            // colCount
            // 
            this.colCount.Text = "Khối lượng";
            this.colCount.Width = 70;
            // 
            // colPrice
            // 
            this.colPrice.Text = "Giá";
            this.colPrice.Width = 110;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Thành tiền";
            this.colAmount.Width = 110;
            // 
            // colFee
            // 
            this.colFee.Text = "Phí";
            this.colFee.Width = 110;
            // 
            // colTax
            // 
            this.colTax.Text = "Thuế";
            this.colTax.Width = 110;
            // 
            // colCommand
            // 
            this.colCommand.Text = "Lệnh";
            this.colCommand.Width = 40;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.Location = new System.Drawing.Point(6, 317);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(712, 13);
            this.StatusLabel.TabIndex = 3;
            // 
            // PortfolioDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 332);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.portfolioDetailLV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PortfolioDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.PortfolioDetailForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView portfolioDetailLV;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colFee;
        private System.Windows.Forms.ColumnHeader colTax;
        private System.Windows.Forms.ColumnHeader colCommand;
        private System.Windows.Forms.Label StatusLabel;
    }
}