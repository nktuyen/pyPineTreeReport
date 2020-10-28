using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TradingAssistant
{
    public partial class BuyForm : Form
    {
        private Settings Settings { get; } = Settings.Instance;
        public CoPhieu Stock { get; set; } = null;
        private bool AutoCalc { get; set; } = true;
        private GiaoDichMua GiaMuaoDich { get; set; } = null;
        
        public BuyForm(CoPhieu stock=null)
        {
            InitializeComponent();
            Stock = stock;
        }
        private bool PreBuy()
        {
            if (txtKhoiLuongMua.Value <= 0)
            {
                MessageBox.Show("Khối lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKhoiLuongMua.Focus();
                return false;
            }
            GiaMuaoDich.KhoiLuongMua = (int)txtKhoiLuongMua.Value;


            if (txtGiaMua.TextLength <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }
            GiaMuaoDich.GiaMua = int.Parse(txtGiaMua.Text);
            if (GiaMuaoDich.GiaMua <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }

            GiaMuaoDich.PhiGiaoDichMua = int.Parse(txtPhiGiaMuaoDich.Text);
            
            return true;
        }

        private void BuyForm_Load(object sender, EventArgs e)
        {
            txtKhoiLuongMua.Minimum = 1;
            txtKhoiLuongMua.Maximum = int.MaxValue;
            txtKhoiLuongMua.Increment = 10;

            if(Stock != null)
            {
                txtMaCoPhieu.Text = Stock.MaCoPhieu;
                toolTip1.InitialDelay = 0;
                toolTip1.ShowAlways = true;
                toolTip1.IsBalloon = false;

                GiaMuaoDich = new GiaoDichMua();
                GiaMuaoDich.CoPhieu = Stock.ID;
                GiaMuaoDich.ThoiGianMua = DateTime.Today;
                dateTimePicker1.Value = GiaMuaoDich.ThoiGianMua;

                if (Stock.TenDoanhNghiep != string.Empty)
                {
                    toolTip1.SetToolTip(txtMaCoPhieu, Stock.TenDoanhNghiep);
                }
                else
                {
                    toolTip1.SetToolTip(txtMaCoPhieu, Stock.MaCoPhieu);
                }

                btnConfirm.Enabled = true;
                txtKhoiLuongMua.Enabled = true;
                txtGiaMua.Enabled = true;
                txtPhiGiaMuaoDich.Enabled = true;
                dateTimePicker1.Enabled = true;
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

            DateTime dt = DateTime.Today;
            string sTime = string.Format("{0:D2}/{1:D2}/{2:D4}", dt.Day, dt.Month, dt.Year);
            var cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichMua(CoPhieu,KhoiLuongMua,GiaMua,PhiGiaoDichMua,ThoiGianMua) VALUES({0},{1},{2},{3},\"{4}\")", Stock.ID, txtKhoiLuongMua.Value, txtGiaMua.Text, txtPhiGiaMuaoDich.Text, sTime), Settings.DBConnection);
            if(cmd.ExecuteNonQuery() <= 0)
            {
                MessageBox.Show(string.Format("Mua cổ phiếu \"{0}\" thất bại!", txtMaCoPhieu.Text), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void txtGiaMua_TextChanged(object sender, EventArgs e)
        {
            if (txtGiaMua.TextLength>0 && txtKhoiLuongMua.Value>0 && AutoCalc)
            {
                int iKL = (int)txtKhoiLuongMua.Value;
                int iGiaMua = int.Parse(txtGiaMua.Text);
                float fAmount = (float)iKL * (float)iGiaMua;
                float fFee = fAmount * Settings.System.PhiGiaoDichMua/100f;

                txtPhiGiaMuaoDich.Text = Math.Round(fFee).ToString();
            }
        }

        private void txtGiaMua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar!= 8)
            {
                e.Handled = true;
            }
        }

        private void txtPhiGiaMuaoDich_KeyDown(object sender, KeyEventArgs e)
        {
            AutoCalc = false;
        }

        private void txtPhiGiaMuaoDich_TextChanged(object sender, EventArgs e)
        {
            if (txtPhiGiaMuaoDich.TextLength <= 0)
            {
                AutoCalc = true;
            }
        }

        private void txtPhiGiaMuaoDich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GiaMuaoDich.ThoiGianMua = dateTimePicker1.Value;
            string sDateTime = string.Format("{0:D2}/{1:D2}/{2:D4}", GiaMuaoDich.ThoiGianMua.Day, GiaMuaoDich.ThoiGianMua.Month, GiaMuaoDich.ThoiGianMua.Year);
            txtThoiGianMuanGiaMuaoDich.Text = sDateTime;
        }
    }
}
