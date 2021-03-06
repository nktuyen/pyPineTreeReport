﻿using System;
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
    public partial class StockManager : Form
    {
        public ChungKhoan Stock { get; set; } = null;
        private Settings Settings { get; } = Settings.Instance;
        public bool UpdateMode { get; set; } = false;
        public StockManager(ChungKhoan stock)
        {
            InitializeComponent();
            this.Stock = stock;
        }

        private bool IsStockExist(string code)
        {
            if (null == Settings.DBConnection|| Settings.DBConnection.State!= ConnectionState.Open)
            {
                return false;
            }
            var cmd = new SQLiteCommand(string.Format("SELECT MaChungKhoan FROM ChungKhoan WHERE MaChungKhoan=\"{0}\"", code.ToUpper()), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            return reader.HasRows;
        }

        private bool PreAdd()
        {
            if (txtMaChungKhoan.TextLength <= 0)
            {
                MessageBox.Show("Mã chứng khoáng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaChungKhoan.Focus();
                return false;
            }

            if (IsStockExist(txtMaChungKhoan.Text))
            {
                MessageBox.Show(string.Format("Mã chứng khoán {0} đã tồn tại!\nVui lòng nhập mã khác", txtMaChungKhoan.Text), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaChungKhoan.Focus();
                return false;
            }
            return true;
        }

        private bool PreUpdate()
        {
            return true;
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            if (!PreAdd())
            {
                return;
            }
            var cmd = new SQLiteCommand(string.Format("INSERT INTO ChungKhoan(MaChungKhoan,TenDoanhNghiep,SanNiemYet,KhoiLuongNiemYet,KhoiLuongLuuHanh,NgayNiemYet) VALUES(\"{0}\",\"{1}\",{2},{3},{4},\"{5}\")", txtMaChungKhoan.Text, txtTenDoanhNghiep.Text, cbbSanNiemYet.SelectedIndex, txtKhoiLuongNiemYet.Value, txtKhoiLuongLuuHanh.Value, txtNgayNiemYet.Text.Replace("/","")), Settings.DBConnection);
            int rows = cmd.ExecuteNonQuery();
            if(rows <= 0)
            {
                MessageBox.Show("Không thể thêm chứng khoán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void InitExchangesList()
        {
            cbbSanNiemYet.DataSource = Settings.DanhSachSanGiaoDich;
            cbbSanNiemYet.DisplayMember = "MaSan";
        }
        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (!PreUpdate())
            {
                return;
            }

            var cmd = new SQLiteCommand(string.Format("UPDATE ChungKhoan SET TenDoanhNghiep=\"{0}\",SanNiemYet={1},KhoiLuongNiemYet={2},KhoiLuongLuuHanh={3},NgayNiemYet=\"{4}\" WHERE ID={5}", txtTenDoanhNghiep.Text, cbbSanNiemYet.SelectedIndex, txtKhoiLuongNiemYet.Value, txtKhoiLuongLuuHanh.Value, txtNgayNiemYet.Text.Replace("/", ""), txtStockID.Text), Settings.DBConnection);
            int rows = cmd.ExecuteNonQuery();
            if (rows <= 0)
            {
                MessageBox.Show("Không thể thêm chứng khoán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            this.DialogResult = DialogResult.OK;
        }

        private void StockManager_Load(object sender, EventArgs e)
        {
            InitExchangesList();

            if (null != this.Stock) //Edit
            {
                btnAddStock.Visible = false;
                btnUpdateStock.Visible = true;
                this.Text = "Cập nhật thông tin chứng khoán";
                this.AcceptButton = btnUpdateStock;

                txtStockID.Text = this.Stock.ID.ToString();
                txtStockID.ForeColor = Color.Black;

                txtMaChungKhoan.Text = Stock.MaChungkhoan;
                txtMaChungKhoan.ReadOnly = true;

                txtTenDoanhNghiep.Text = Stock.TenDoanhNghiep;

                cbbSanNiemYet.SelectedIndex = Stock.SanNiemYet;

                txtKhoiLuongNiemYet.Maximum = int.MaxValue;
                txtKhoiLuongNiemYet.Value = Stock.KhoiLuongNiemYet;

                txtKhoiLuongLuuHanh.Maximum = int.MaxValue;
                txtKhoiLuongLuuHanh.Value = Stock.KhoiLuongLuuHanh;

                stockDatePicker.Value = Stock.NgayNiemYet;
            }
            else
            {
                btnAddStock.Visible = true;
                btnUpdateStock.Visible = false;
                this.Text = "Thêm chứng khoán";
                this.AcceptButton = btnAddStock;

                txtStockID.ForeColor = Color.Gray;
                txtStockID.Text = "Không xác định";

                txtMaChungKhoan.ReadOnly = false;

                txtKhoiLuongNiemYet.Maximum = int.MaxValue;

                txtKhoiLuongLuuHanh.Maximum = int.MaxValue;

                stockDatePicker.Value = DateTime.Today;
            }

            btnAddStock.Enabled = false;
            btnUpdateStock.Enabled = false;
        }

        private void btnAddExchanges_Click(object sender, EventArgs e)
        {
            ExchangesManager form = new ExchangesManager(null);
            if(form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                InitExchangesList();
            }
        }

        private void btnEditExchange_Click(object sender, EventArgs e)
        {
            SanGiaoDich exch = cbbSanNiemYet.SelectedItem as SanGiaoDich;
            ExchangesManager form = new ExchangesManager(exch);

            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                InitExchangesList();
            }
        }

        private void btnDelExchange_Click(object sender, EventArgs e)
        {

        }

        private void cbbSanNiemYet_SelectedIndexChanged(object sender, EventArgs e)
        {
            SanGiaoDich exch = cbbSanNiemYet.SelectedItem as SanGiaoDich;
            btnDelExchange.Enabled = btnEditExchange.Enabled = (exch != null && exch.ID > 0);
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void StockManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtMaChungKhoan_TextChanged(object sender, EventArgs e)
        {
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void txtTenDoanhNghiep_TextChanged(object sender, EventArgs e)
        {
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void txtKhoiLuongNiemYet_ValueChanged(object sender, EventArgs e)
        {
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void txtKhoiLuongLuuHanh_ValueChanged(object sender, EventArgs e)
        {
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void txtNgayNiemYet_TextChanged(object sender, EventArgs e)
        {
            btnAddStock.Enabled = btnUpdateStock.Enabled = true;
        }

        private void stockDatePicker_ValueChanged(object sender, EventArgs e)
        {
            txtNgayNiemYet.Text = string.Format("{0:D2}/{1:D2}/{2:D4}", stockDatePicker.Value.Day, stockDatePicker.Value.Month, stockDatePicker.Value.Year);
        }
    }
}
