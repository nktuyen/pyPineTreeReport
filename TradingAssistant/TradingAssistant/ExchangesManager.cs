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
    public partial class ExchangesManager : Form
    {
        private Settings Settings { get; } = Settings.Instance;
        private SanGiaMuaoDich Exchange { get; set; } = null;

        public ExchangesManager(SanGiaMuaoDich exch)
        {
            InitializeComponent();
            Exchange = exch;
        }

        private bool PreAdd()
        {
            if (txtMaSan.TextLength <= 0)
            {
                MessageBox.Show("Mã sàn GiaMuao dịch không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSan.Focus();
                return false;
            }

            return true;
        }

        private bool PreUpdate()
        {
            return true;
        }

        private void ExchangesManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ExchangesManager_Load(object sender, EventArgs e)
        {
            if(Exchange != null)
            {
                this.Text = "Cập nhật thông tin sàn GiaMuao dịch";
                btnUpdateExch.Visible = true;
                btnAddExch.Visible = false;
                this.AcceptButton = btnUpdateExch;

                txtID.ForeColor = Color.Black;
                txtID.Text = Exchange.ID.ToString();

                txtMaSan.Text = Exchange.MaSan;
                txtMaSan.ReadOnly = true;

                txtTenSan.Text = Exchange.TenSan;

                txtTrangWeb.Text = Exchange.TrangWeb;
            }
            else
            {
                this.Text = "Thêm sàn GiaMuao dịch";
                btnUpdateExch.Visible = false;
                btnAddExch.Visible = true;
                AcceptButton = btnAddExch;

                txtID.ForeColor = Color.Gray;
                txtID.Text = "Không xác định";

                txtMaSan.ReadOnly = false;
            }

            btnAddExch.Enabled = false;
            btnUpdateExch.Enabled = false;
        }

        private void txtMaSan_TextChanged(object sender, EventArgs e)
        {
            btnAddExch.Enabled = btnUpdateExch.Enabled = true;
        }

        private void txtTenSan_TextChanged(object sender, EventArgs e)
        {
            btnAddExch.Enabled = btnUpdateExch.Enabled = true;
        }

        private void txtTrangWeb_TextChanged(object sender, EventArgs e)
        {
            btnAddExch.Enabled = btnUpdateExch.Enabled = true;
        }

        private void btnAddExch_Click(object sender, EventArgs e)
        {
            if (!PreAdd())
            {
                return;
            }
        }

        private void btnUpdateExch_Click(object sender, EventArgs e)
        {
            if(!PreUpdate()){
                return;
            }
        }
    }
}
