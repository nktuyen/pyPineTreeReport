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
using System.Resources;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace TradingAssistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Settings = Settings.Instance;
        }

        private Settings Settings { get; } = Settings.Instance;
        private ListViewHitTestInfo StockListHitTestInfo { get; set; } = null;

        private string BuildConnectionString(string dbFile)
        {
            return string.Format("Data Source={0}; Mode=ReadWrite", dbFile);
        }

        private void InitExchangesList()
        {
            Settings.DanhSachSanGiaoDich.Clear();

            if (null != Settings.DBConnection && Settings.DBConnection.State == ConnectionState.Open)
            {
                var cmd = new SQLiteCommand("SELECT * FROM San ORDER BY ID", Settings.DBConnection);
                SQLiteDataReader reader = cmd.ExecuteReader();
                int ID = 0;
                string code = string.Empty;
                string name = string.Empty;
                string website = string.Empty;
                while (reader.Read())
                {
                    ID = reader.GetInt32(0);
                    code = reader.GetString(1);
                    try
                    {
                        name = reader.GetString(2);
                    }
                    catch(Exception ex)
                    {
                        Debug.Print(ex.Message);
                        name = string.Empty;
                    }

                    try
                    {
                        website = reader.GetString(3);
                    }
                    catch(Exception ex)
                    {
                        Debug.Print(ex.Message);
                        website = string.Empty;
                    }

                    SanGiaoDich exch = new SanGiaoDich();
                    exch.ID = ID;
                    exch.MaSan = code;
                    exch.TenSan = name;
                    exch.TrangWeb = website;

                    Settings.DanhSachSanGiaoDich.Add(exch);
                }
            }

            Settings.DanhSachSanGiaoDich.Insert(0, new SanGiaoDich(0, "---"));
        }

        private void InitSystem()
        {
            if (Settings.System == null)
            {
                Settings.System = new HeThong();
            }
            if (Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return;
            }

            var cmd = new SQLiteCommand("SELECT PhiGiaoDichMua,PhiGiaoDichBan,PhiUngTruocTienBan FROM HeThong", Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int iTmp = 0;
            float fTemp = 0;
            string sTemp = string.Empty;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    fTemp = reader.GetFloat(0);
                    Settings.System.PhiGiaoDichMua = fTemp;

                    fTemp = reader.GetFloat(1);
                    Settings.System.PhiGiaoDichBan = fTemp;

                    fTemp = reader.GetFloat(2);
                    Settings.System.PhiUngTruocTienBan = fTemp;

                    break;
                }
            }
        }

        private void InitStockList()
        {
            if (Settings.System == null)
            {
                InitSystem();
            }

            if (Settings.DanhSachSanGiaoDich.Count <= 0)
            {
                InitExchangesList();
            }
            
            stockCodeListView.Items.Clear();
            if(Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return;
            }
            var cmd = new SQLiteCommand("SELECT * FROM ChungKhoan ORDER BY MaChungKhoan", Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int ID = 0;
            string code = string.Empty;
            string name = string.Empty;
            int exchange = 0;
            int listed = 0;
            int shared = 0;
            int year = 0;
            int month = 0;
            int day = 0;
            string temp = string.Empty;
            while (reader.Read())
            {
                ID = reader.GetInt32(0);
                code = reader.GetString(1);
                name = reader.GetString(2);
                exchange = reader.GetInt32(3);
                listed = reader.GetInt32(4);
                shared = reader.GetInt32(5);
                temp = reader.GetString(6);
                day = int.Parse(temp.Substring(0, 2));
                month = int.Parse(temp.Substring(2, 2));
                year = int.Parse(temp.Substring(4, 4));

                ChungKhoan stock = new ChungKhoan();
                stock.ID = ID;
                stock.MaChungkhoan = code;
                stock.TenDoanhNghiep = name;
                stock.SanNiemYet = exchange;
                stock.KhoiLuongNiemYet = listed;
                stock.KhoiLuongLuuHanh = shared;
                stock.NgayNiemYet = new DateTime(year, month, day);

                ListViewItem item = stockCodeListView.Items.Add(ID.ToString());
                item.Tag = stock;
                item.SubItems.Add(code);
                item.SubItems.Add(name);
                item.SubItems.Add(Settings.DanhSachSanGiaoDich[stock.SanNiemYet].MaSan);
                item.SubItems.Add(string.Format("{0:D2}/{1:D2}/{2:D4}", stock.NgayNiemYet.Day, stock.NgayNiemYet.Month, stock.NgayNiemYet.Year));
            }
        }

        private void mnuNewFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.OverwritePrompt = true;
            dlg.FileName = "Stock.db";
            dlg.Filter = "Tất cả các tệp tin(*.*)|*.*";
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if(dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string filePath = dlg.FileName;
            BinaryWriter writer = null;
            try
            {
                writer = new BinaryWriter(File.OpenWrite(filePath));
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
                MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(writer != null)
            {
                try
                {
                    writer.Write(TradingAssistant.Properties.Resources.Stock);
                    writer.Flush();
                    SQLiteConnection conn = new SQLiteConnection(BuildConnectionString(dlg.FileName));
                    if (null == conn)
                    {
                        MessageBox.Show("Không thể mở tệp dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Settings.DBConnection != null)
                    {
                        Settings.DBConnection.Close();
                    }
                    Settings.DataFile = dlg.FileName;
                    Settings.DBConnection = conn;
                    closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;

                    InitStockList();
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                    MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        private void mnuOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            dlg.CheckFileExists = true;
            dlg.ShowReadOnly = false;
            dlg.Title = "Chọn tệp dữ liệu";
            dlg.Filter = "Tất cả các tệp tin(*.*)|*.*";
            if(dlg.ShowDialog()!= DialogResult.OK)
            {
                return;
            }
            SQLiteConnection conn = new SQLiteConnection(BuildConnectionString(dlg.FileName));
            if (null == conn)
            {
                MessageBox.Show("Không thể mở tệp dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                conn.Open();
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
                MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Settings.DBConnection != null)
            {
                Settings.DBConnection.Close();
            }
            Settings.DataFile = dlg.FileName;
            Settings.DBConnection = conn;
            closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;
            InitStockList();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {

        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {

        }

        private void addStockMenuItem_Click(object sender, EventArgs e)
        {
            StockManager stockManager = new StockManager(null);
            if(stockManager.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                InitStockList();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(Settings.DataFile != null && Settings.DataFile != string.Empty)
            {
                if (File.Exists(Settings.DataFile))
                {
                    SQLiteConnection conn = new SQLiteConnection(BuildConnectionString(Settings.DataFile));
                    if (null == conn)
                    {
                        MessageBox.Show("Không thể mở tệp dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.Message);
                        MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Settings.DBConnection != null)
                    {
                        Settings.DBConnection.Close();
                    }
                    Settings.DBConnection = conn;
                    closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;
                    InitExchangesList();
                    InitStockList();
                }
                else
                {
                    Settings.DataFile = string.Empty;
                }
            }
        }

        private void stockCodeListView_MouseDown(object sender, MouseEventArgs e)
        {
            stockCodeListView.ContextMenuStrip = null;
            if (e.Button == MouseButtons.Right)
            {
                StockListHitTestInfo = stockCodeListView.HitTest(e.X, e.Y);
            }
        }

        private void stockCodeListView_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(null!= StockListHitTestInfo)
                {
                    Debug.Print(StockListHitTestInfo.Location.ToString());
                    if(StockListHitTestInfo.Location ==  ListViewHitTestLocations.None)
                    {
                        stockCodeListView.ContextMenuStrip = contextMenuStrip1;
                    }
                    else if(StockListHitTestInfo.Location == ListViewHitTestLocations.Label)
                    {
                        stockCodeListView.ContextMenuStrip = contextMenuStrip2;
                    }
                }
            }
            StockListHitTestInfo = null;
        }

        private void stockCodeListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && stockCodeListView.SelectedItems.Count == 1)
            {
                ListViewItem selItem = stockCodeListView.SelectedItems[0];
                if (null != selItem.Tag)
                {
                    ChungKhoan stock = selItem.Tag as ChungKhoan;
                    if (null != stock)
                    {
                        StockManager form = new StockManager(stock);
                        if(form.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            InitStockList();
                        }
                    }
                }
            }
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buyStockMenuItem_Click(object sender, EventArgs e)
        {
            if (stockCodeListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Chọn một chứng khoán đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selItem = stockCodeListView.SelectedItems[0];
            if (null == selItem || null == selItem.Tag)
            {
                MessageBox.Show("Chọn một chứng khoán đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChungKhoan selStock = selItem.Tag as ChungKhoan;
            if (null == selStock)
            {
                MessageBox.Show("Chọn một chứng khoán đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BuyForm form = new BuyForm(selStock);
            if(form.ShowDialog()!= DialogResult.OK)
            {
                return;
            }
            else
            {
                //Refresh Invest catagory
            }
        }

        private void editStockMenuItem_Click(object sender, EventArgs e)
        {
            stockCodeListView_MouseDoubleClick(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void deleteStockMenuItem_Click(object sender, EventArgs e)
        {
            if (stockCodeListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Chọn một chứng khoán đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selItem = stockCodeListView.SelectedItems[0];
            if(null==selItem || null == selItem.Tag)
            {
                MessageBox.Show("Chọn một chứng khoán đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChungKhoan selStock = selItem.Tag as ChungKhoan;
            if (null == selStock)
            {
                MessageBox.Show("Chọn một chứng khoán đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(MessageBox.Show(string.Format("Bạn chắc chắn muốn xóa chứng khoán có mã \"{0}\"?", selStock.MaChungkhoan), "Xóa chứng khoán", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!= DialogResult.Yes)
            {
                return;
            }

            var cmd = new SQLiteCommand(string.Format("DELETE FROM ChungKhoan WHERE ID={0}", selStock.ID), Settings.DBConnection);
            if (cmd.ExecuteNonQuery() <= 0)
            {
                MessageBox.Show(string.Format("Xóa chứng khoán có mã \"{0}\" thất bại!", selStock.MaChungkhoan), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                InitStockList();
            }
        }

        private void closeDataFileMenuItem_Click(object sender, EventArgs e)
        {
            if(null != Settings.DBConnection)
            {
                if(Settings.DBConnection.State!= ConnectionState.Closed)
                {
                    Settings.DBConnection.Close();
                }
                Settings.DataFile = string.Empty;
                closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;
                Settings.DBConnection = null;
            }
            InitStockList();
        }
    }
}
