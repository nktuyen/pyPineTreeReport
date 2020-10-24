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
        private GiaoDichMua GiaoDich { get; set; } = null;
        
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
            GiaoDich.KhoiLuong = (int)txtKhoiLuongMua.Value;


            if (txtGiaMua.TextLength <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }
            GiaoDich.Gia = int.Parse(txtGiaMua.Text);
            if (GiaoDich.Gia <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }

            GiaoDich.PhiGiaoDich = int.Parse(txtPhiGiaoDich.Text);
            
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

                GiaoDich = new GiaoDichMua();
                GiaoDich.CoPhieu = Stock.ID;
                GiaoDich.NgayGiaoDich = DateTime.Today;
                dateTimePicker1.Value = GiaoDich.NgayGiaoDich;

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
                txtPhiGiaoDich.Enabled = true;
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
            var cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichMua(CoPhieu,KhoiLuongMua,Gia,PhiGiaoDich,NgayGiaoDich) VALUES({0},{1},{2},{3},\"{4}\")", Stock.ID, txtKhoiLuongMua.Value, txtGiaMua.Text, txtPhiGiaoDich.Text, sTime), Settings.DBConnection);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GiaoDich.NgayGiaoDich = dateTimePicker1.Value;
            string sDateTime = string.Format("{0:D2}/{1:D2}/{2:D4}", GiaoDich.NgayGiaoDich.Day, GiaoDich.NgayGiaoDich.Month, GiaoDich.NgayGiaoDich.Year);
            txtThoiGianGiaoDich.Text = sDateTime;
        }
    }
}
