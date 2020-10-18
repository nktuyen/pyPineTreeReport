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
    public partial class BuyForm : Form
    {
        private Settings Settings { get; } = Settings.Instance;
        public ChungKhoan Stock { get; set; } = null;
        private bool AutoCalc { get; set; } = true;
        public BuyForm(ChungKhoan stock=null)
        {
            InitializeComponent();
            Stock = stock;
        }
        private bool PreBuy()
        {
            return true;
        }

        private void BuyForm_Load(object sender, EventArgs e)
        {
            txtKhoiLuongMua.Minimum = 1;
            txtKhoiLuongMua.Maximum = int.MaxValue;
            txtKhoiLuongMua.Increment = 10;

            if(Stock != null)
            {
                txtMaChungKhoan.Text = Stock.MaChungkhoan;

                btnConfirm.Enabled = true;
            }
        }

        private void BuyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!PreBuy())
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaMua.TextLength>0 && txtKhoiLuongMua.Value>0 && AutoCalc)
            {
                int iKL = (int)txtKhoiLuongMua.Value;
                int iGia = int.Parse(txtGiaMua.Text);
                float fAmount = (float)iKL * (float)iGia;
                float fFee = fAmount * Settings.System.PhiGiaoDichMua/100f;

                txtPhiGiaoDich.Text = Math.Round(fFee).ToString();
            }
        }

        private void txtGiaMua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar!= 8)
            {
                e.Handled = true;
            }
        }

        private void txtPhiGiaoDich_KeyDown(object sender, KeyEventArgs e)
        {
            AutoCalc = false;
        }

        private void txtPhiGiaoDich_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiGiaoDich.TextLength <= 0)
            {
                AutoCalc = true;
            }
        }

        private void txtPhiGiaoDich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
