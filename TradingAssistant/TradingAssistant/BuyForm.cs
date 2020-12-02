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
using System.Diagnostics;

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
            GiaoDich.KhoiLuongMua = (int)txtKhoiLuongMua.Value;


            if (txtGiaMua.TextLength <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }
            GiaoDich.GiaMua = int.Parse(txtGiaMua.Text);
            if (GiaoDich.GiaMua <= 0)
            {
                MessageBox.Show("Giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaMua.Focus();
                return false;
            }

            GiaoDich.PhiGiaoDichMua = int.Parse(txtPhiGiaMuaoDich.Text);
            
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
                GiaoDich.ThoiGianMua = DateTime.Today;
                dateTimePicker1.Value = GiaoDich.ThoiGianMua;

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
            string sTime = string.Format("{0:D2}/{1:D2}/{2:D4}-{3:D2}:{4:D2}:{5:D2}", dt.Day, dt.Month, dt.Year,dt.Hour, dt.Minute, dt.Second);
            var cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichMua(CoPhieu,KhoiLuongMua,GiaMua,PhiGiaoDichMua,ThoiGianMua,LenhMua) VALUES({0},{1},{2},{3},'{4}','{5}')", Stock.ID, txtKhoiLuongMua.Value, txtGiaMua.Text, txtPhiGiaMuaoDich.Text, sTime, string.Empty), Settings.DBConnection);
            if(cmd.ExecuteNonQuery() <= 0)
            {
                MessageBox.Show(string.Format("Mua cổ phiếu \"{0}\" thất bại!", txtMaCoPhieu.Text), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.GiaoDich != null) {
                cmd = new SQLiteCommand(string.Format("SELECT ID FROM GiaoDichMua WHERE rowid={0}", Settings.DBConnection.LastInsertRowId), Settings.DBConnection);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        this.GiaoDich.ID = reader.GetInt32(0);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                    }
                }
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
            GiaoDich.ThoiGianMua = dateTimePicker1.Value;
            string sDateTime = string.Format("{0:D2}/{1:D2}/{2:D4}", GiaoDich.ThoiGianMua.Day, GiaoDich.ThoiGianMua.Month, GiaoDich.ThoiGianMua.Year);
            txtThoiGianMuanGiaMuaoDich.Text = sDateTime;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
