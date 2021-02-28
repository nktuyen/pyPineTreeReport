using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingAssistant
{
    public partial class PortfolioDetailForm : Form
    {
        public PortfolioItem PortfolioItem { get; set; } = null;

        public PortfolioDetailForm()
        {
            InitializeComponent();
        }

        private void PortfolioDetailForm_Load(object sender, EventArgs e)
        {
            if (this.PortfolioItem!=null)
            {
                this.Text = string.Format("{0} - Danh sách giao dịch mua", this.PortfolioItem.MaCoPhieu);
                LoadBuyTransactions();
            }
        }

        private void LoadBuyTransactions()
        {
            portfolioDetailLV.Items.Clear();
            if (this.PortfolioItem == null)
            {
                return;
            }

            int KL = 0;
            int GT = 0;
            int PhiGD = 0;

            foreach(GiaoDichMua giaoDich in this.PortfolioItem.DanhsachGiaoDichMua)
            {
                ListViewItem lvi = portfolioDetailLV.Items.Add(string.Format("{0}",portfolioDetailLV.Items.Count + 1));
                lvi.SubItems.Add(Utils.DateTimeToString(giaoDich.ThoiGianMua));
                lvi.SubItems.Add(giaoDich.KhoiLuongMua.ToString());
                lvi.SubItems.Add(giaoDich.GiaMua.ToString("n0"));
                lvi.SubItems.Add((giaoDich.KhoiLuongMua * giaoDich.GiaMua).ToString("n0"));
                lvi.SubItems.Add(giaoDich.PhiGiaoDichMua.ToString("n0"));
                lvi.SubItems.Add("0");
                lvi.SubItems.Add(giaoDich.LoaiLenh);

                KL += giaoDich.KhoiLuongMua;
                GT += (giaoDich.KhoiLuongMua * giaoDich.GiaMua);
                PhiGD += giaoDich.PhiGiaoDichMua;
            }

            StatusLabel.Text = string.Format("Tổng khối lượng: {0}  |  Tổng giá trị: {1} ₫  |  Tổng phí giao dịch: {2} ₫", KL, GT.ToString("n0"), PhiGD.ToString("n0"));
        }

        private void LoadSellTransactions(GiaoDichMua giaoDichMua)
        {
            if (this.PortfolioItem == null)
            {
                return;
            }
        }
    }
}
