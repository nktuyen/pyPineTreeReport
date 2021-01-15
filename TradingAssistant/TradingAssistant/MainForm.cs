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
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Collections.Specialized;

namespace TradingAssistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Settings = Settings.Instance;
            Settings.DBFileChanged += new Settings.DBFileChangedDel(OnDBFileChanged);
        }

        private int TongTienMua { get; set; } = 0;
        public int TongTienBan { get; set; } = 0;
        public int TongTienPhiGiaoDich { get; set; } = 0;
        public int TongTienThue { get; set; } = 0;

        private Microsoft.Office.Interop.Excel.Application Excel
        {
            get
            {
                Type officeType = Type.GetTypeFromProgID("Excel.Application");
                if (officeType == null)
                {
                    return null;
                }
                else
                {
                    return (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(officeType);
                }
            }
        }

        private Settings Settings { get; } = Settings.Instance;
        private ListViewHitTestInfo StockListHitTestInfo { get; set; } = null;
        private ListViewHitTestInfo PortfolioListHitTestInfo { get; set; } = null;
        private bool ReloadStockListAfterImport { get; set; } = false;
        private bool ReloadPortfolioAfterImport { get; set; } = false;
        private SortedDictionary<string, PortfolioItem> PortfolioMap { get; set; } = new SortedDictionary<string, PortfolioItem>();

        private string BuildConnectionString(string dbFile)
        {
            return string.Format("Data Source={0}; Mode=ReadWrite", dbFile);
        }

        private void OnDBFileChanged(string file)
        {
            if(file==string.Empty)
            {
                Text = "TradingAssistant";
            }
            else
            {
                Text = string.Format("TradingAssistant - {0}", file);
            }
        }

        public bool AddSellTransaction(GiaoDichBan transac)
        {
            if (Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return false;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichBan(GiaoDichMua,KhoiLuongBan,GiaBan,ThoiGianBan,PhiGiaoDichBan,LenhBan,ThueGiaoDichBan) VALUES({0},{1},{2},'{3:D2}/{4:D2}/{5:D4}-{6:D2}:{7:D2}:{8:D2}',{9},'{10}',{11})", transac.GiaoDichMua, transac.KhoiLuongBan, transac.GiaBan, transac.ThoiGianBan.Day, transac.ThoiGianBan.Month, transac.ThoiGianBan.Year, transac.ThoiGianBan.Hour, transac.ThoiGianBan.Minute, transac.ThoiGianBan.Second, transac.PhiGiaoDichBan, transac.LoaiLenhBan, transac.ThueGiaoDichBan), Settings.DBConnection);
            int rows = cmd.ExecuteNonQuery();

            return rows > 0;
        }
        
        private bool AddBuyTransaction(GiaoDichMua transac)
        {
            if(Settings.DBConnection==null||Settings.DBConnection.State!= ConnectionState.Open)
            {
                return false;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichMua(CoPhieu,KhoiLuongMua,GiaMua,ThoiGianMua,PhiGiaoDichMua,LenhMua) VALUES({0},{1},{2},'{3:D2}/{4:D2}/{5:D4}-{6:D2}:{7:D2}:{8:D2}',{9},'{10}')", transac.CoPhieu, transac.KhoiLuongMua, transac.GiaMua, transac.ThoiGianMua.Day, transac.ThoiGianMua.Month, transac.ThoiGianMua.Year, transac.ThoiGianMua.Hour, transac.ThoiGianMua.Minute, transac.ThoiGianMua.Second, transac.PhiGiaoDichMua,transac.LoaiLenh), Settings.DBConnection);
            int rows = cmd.ExecuteNonQuery();

            return rows > 0;
        }

        private void LoadExchangesList()
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
                        Utils.DebugPrint(ex.Message);
                        name = string.Empty;
                    }

                    try
                    {
                        website = reader.GetString(3);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                        website = string.Empty;
                    }

                    SanGiaMuaoDich exch = new SanGiaMuaoDich();
                    exch.ID = ID;
                    exch.MaSan = code;
                    exch.TenSan = name;
                    exch.TrangWeb = website;

                    Settings.DanhSachSanGiaoDich.Add(exch);
                }
            }

            Settings.DanhSachSanGiaoDich.Insert(0, new SanGiaMuaoDich(0, "---"));
        }

        private void LoadPortfolio()
        {
            portfolioListView.Items.Clear();
            PortfolioMap.Clear();
            TongTienMua = 0;
            TongTienBan = 0;
            TongTienThue = 0;
            TongTienPhiGiaoDich = 0;
            if(Settings.DBConnection==null||Settings.DBConnection.State!= ConnectionState.Open)
            {
                return;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT ID,CoPhieu,KhoiLuongMua,GiaMua,LenhMua,ThoiGianMua,PhiGiaoDichMua FROM GiaoDichMua"), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            int iColIndex = 0;
            int ID = 0;
            int CoPhieu = 0;
            string MaCoPhieu = string.Empty;
            int KhoiLuongGiaoDich = 0;
            int GiaCoPhieu = 0;
            string LenhGiaoDich = string.Empty;
            string ThoiGianGiaoDich = string.Empty;
            int PhiGiaoDich = 0;
            int GiaTriGiaoDich = 0;
            int ThueGiaoDich = 0;
            int MaGiaoDichMua = 0;
            PortfolioItem portfolioItem = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        iColIndex = 0;
                        ID = reader.GetInt32(iColIndex++);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                        ID = 0;
                    }

                    if(ID != 0)
                    {
                        try
                        {
                            CoPhieu = reader.GetInt32(iColIndex++);
                            KhoiLuongGiaoDich = reader.GetInt32(iColIndex++);
                            GiaCoPhieu = reader.GetInt32(iColIndex++);
                            LenhGiaoDich = reader.GetString(iColIndex++);
                            ThoiGianGiaoDich = reader.GetString(iColIndex++);
                            PhiGiaoDich = reader.GetInt32(iColIndex++);
                            GiaTriGiaoDich = KhoiLuongGiaoDich * GiaCoPhieu;
                            DateTime dt = Utils.DateTimeFromString(ThoiGianGiaoDich);
                            MaCoPhieu = GetStockCode(CoPhieu);
                            if (PortfolioMap.ContainsKey(MaCoPhieu))
                            {
                                portfolioItem = PortfolioMap[MaCoPhieu] as PortfolioItem;
                            }
                            else
                            {
                                portfolioItem = new PortfolioItem();
                                portfolioItem.MaCoPhieu = MaCoPhieu;
                                PortfolioMap.Add(MaCoPhieu, portfolioItem);
                            }
                            portfolioItem.KhoiLuongMua += KhoiLuongGiaoDich;
                            portfolioItem.GiaTriMua += GiaTriGiaoDich;
                            TongTienMua += GiaTriGiaoDich;
                            TongTienPhiGiaoDich += PhiGiaoDich;
                            TongTienThue += ThueGiaoDich;
                            portfolioItem.DanhsachGiaoDichMua.Add(new GiaoDichMua(ID, CoPhieu, KhoiLuongGiaoDich, GiaCoPhieu, LenhGiaoDich, dt, PhiGiaoDich));
                            portfolioItem.Updated = true;
                        }
                        catch(Exception ex)
                        {
                            Utils.DebugPrint(ex.Message);
                            continue;
                        }
                    }
                }
            }

            if (PortfolioMap.Count > 0)
            {
                cmd = new SQLiteCommand(string.Format("SELECT ID,GiaoDichMua,KhoiLuongBan,GiaBan,LenhBan,ThoiGianBan,PhiGiaoDichBan,ThueGiaoDichBan FROM GiaoDichBan"), Settings.DBConnection);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        iColIndex = 0;
                        ID = reader.GetInt32(iColIndex++);
                        MaGiaoDichMua = reader.GetInt32(iColIndex++);
                        KhoiLuongGiaoDich = reader.GetInt32(iColIndex++);
                        GiaCoPhieu = reader.GetInt32(iColIndex++);
                        LenhGiaoDich = reader.GetString(iColIndex++);
                        ThoiGianGiaoDich = reader.GetString(iColIndex++);
                        PhiGiaoDich = reader.GetInt32(iColIndex++);
                        ThueGiaoDich = reader.GetInt32(iColIndex++);
                        GiaTriGiaoDich = KhoiLuongGiaoDich * GiaCoPhieu;

                        CoPhieu = CoPhieuCuaGiaoDichMua(MaGiaoDichMua);
                        if (CoPhieu == 0)
                        {
                            continue;
                        }
                        MaCoPhieu = GetStockCode(CoPhieu);
                        if (PortfolioMap.ContainsKey(MaCoPhieu))
                        {
                            portfolioItem = PortfolioMap[MaCoPhieu];
                        }
                        else
                        {
                            portfolioItem = new PortfolioItem();
                            portfolioItem.MaCoPhieu = MaCoPhieu;
                            PortfolioMap.Add(MaCoPhieu, portfolioItem);
                        }

                        portfolioItem.KhoiLuongBan += KhoiLuongGiaoDich;
                        portfolioItem.GiaTriBan += GiaTriGiaoDich;
                        TongTienBan += GiaTriGiaoDich;
                        TongTienPhiGiaoDich += PhiGiaoDich;
                        TongTienThue += ThueGiaoDich;
                    }
                }
                foreach (string cophieu in PortfolioMap.Keys)
                {
                    PortfolioItem pi = PortfolioMap[cophieu] as PortfolioItem;
                    ListViewItem li = portfolioListView.Items.Add(pi.MaCoPhieu);
                    li.Tag = pi;
                    li.SubItems.Add(pi.KhoiLuongMua.ToString());
                    li.SubItems.Add(pi.GiaTriMua.ToString("n0"));
                    li.SubItems.Add(pi.KhoiLuongBan.ToString());
                    li.SubItems.Add(pi.GiaTriBan.ToString("n0"));
                    li.SubItems.Add(pi.LaiLo.ToString("n0"));
                }
            }
        }

        private void LoadSystemRecords()
        {
            if (Settings.System == null)
            {
                Settings.System = new HeThong();
            }
            if (Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return;
            }

            var cmd = new SQLiteCommand("SELECT PhiGiaoDichMua,PhiGiaoDichBan,ThueThuNhap FROM HeThong", Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
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
                    Settings.System.ThueTrenMoiGiaoDich = fTemp;

                    break;
                }
            }
        }

        private int AddStock(string stockCode, string companyName = "")
        {
            int res = 0;
            if (Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return res;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO CoPhieu(MaCoPhieu,TenDoanhNghiep)VALUES(\'{0}\',\'{1}\')", stockCode, companyName), Settings.DBConnection);
            if (cmd.ExecuteNonQuery() > 0)
            {
                res = GetStockID(stockCode);
            }

            return res;
        }

        private int GetStockID(string stockCode)
        {
            int res = 0;
            if (Settings.DBConnection == null||Settings.DBConnection.State!= ConnectionState.Open)
            {
                return res;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT ID FROM CoPhieu WHERE MaCoPhieu='{0}'", stockCode), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        res = reader.GetInt32(0);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                        res = 0;
                    }
                    break;
                }
            }

            return res;
        }

        private int CoPhieuCuaGiaoDichMua(int ID)
        {
            int res = 0;
            if (ID == 0 || Settings.DBConnection==null||Settings.DBConnection.State != ConnectionState.Open)
            {
                return res;
            }

            var cmd = new SQLiteCommand(string.Format("SELECT CoPhieu FROM GiaoDichMua WHERE ID={0}", ID), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    res = reader.GetInt32(0);
                    break;
                }
            }
            return res;
        }

        private string GetStockCode(int stockID)
        {
            string res = string.Empty;
            if (Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return res;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT MaCoPhieu FROM CoPhieu WHERE ID='{0}'", stockID), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        res = reader.GetString(0);
                    }
                    catch (Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                        res = string.Empty;
                    }
                    break;
                }
            }

            return res;
        }

        private void LoadStockList()
        {
            if (Settings.System == null)
            {
                LoadSystemRecords();
            }

            if (Settings.DanhSachSanGiaoDich.Count <= 0)
            {
                LoadExchangesList();
            }
            
            stockCodeListView.Items.Clear();
            if(Settings.DBConnection == null || Settings.DBConnection.State != ConnectionState.Open)
            {
                return;
            }
            var cmd = new SQLiteCommand("SELECT ID,MaCophieu,TenDoanhNghiep,SanNiemYet,KhoiLuongNiemYet,KhoiLuongLuuHanh,NgayNiemYet FROM CoPhieu ORDER BY MaCoPhieu", Settings.DBConnection);
            //try
            {
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
                    try
                    {
                        temp = reader.GetString(6);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                        temp = "1/1/1970";
                    }

                    if (temp != string.Empty)
                    {
                        string[] numbers = temp.Split('/');
                        try
                        {
                            if (numbers.Length > 0)
                                day = int.Parse(numbers[0].Trim());
                            else
                                day = 1;
                            if (numbers.Length > 1)
                                month = int.Parse(numbers[1].Trim());
                            else
                                month = 1;
                            if (numbers.Length > 2)
                                year = int.Parse(numbers[2].Trim());
                            else
                                year = 1970;
                        }
                        catch(Exception ex)
                        {
                            Utils.DebugPrint(ex.Message);
                        }
                    }
                    else
                    {
                        day = 1;
                        month = 1;
                        year = 1970;
                    }

                    CoPhieu stock = new CoPhieu();
                    stock.ID = ID;
                    stock.MaCoPhieu = code;
                    stock.TenDoanhNghiep = name;
                    stock.SanNiemYet = exchange;
                    stock.KhoiLuongNiemYet = listed;
                    stock.KhoiLuongLuuHanh = shared;
                    try
                    {
                        stock.NgayNiemYet = new DateTime(year, month, day, 0, 0, 0);
                    }
                    catch(Exception ex)
                    {
                        Utils.DebugPrint(ex.Message);
                    }

                    ID = stockCodeListView.Items.Count + 1;
                    ListViewItem item = stockCodeListView.Items.Add(ID.ToString());
                    item.Tag = stock;
                    item.SubItems.Add(code);
                    item.SubItems.Add(name);
                    item.SubItems.Add(Settings.DanhSachSanGiaoDich[stock.SanNiemYet].MaSan);
                    item.SubItems.Add(string.Format("{0:D2}/{1:D2}/{2:D4}", stock.NgayNiemYet.Day, stock.NgayNiemYet.Month, stock.NgayNiemYet.Year));
                }

                tabControl1_SelectedIndexChanged(this, new EventArgs());
            }
            /*catch(Exception ex)
            {
                DebugPrint(ex.Message);
            }*/
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
                Utils.DebugPrint(ex.Message);
                MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(writer != null)
            {
                try
                {
                    writer.Write(TradingAssistant.Properties.Resources.Stock);
                    writer.Flush();
                }
                catch(Exception ex)
                {
                    Utils.DebugPrint(ex.Message);
                    MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    writer.Close();
                }
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
            catch (Exception ex)
            {
                Utils.DebugPrint(ex.Message);
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

            LoadExchangesList();
            LoadStockList();
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
                Utils.DebugPrint(ex.Message);
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
            LoadExchangesList();
            LoadStockList();
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
                LoadStockList();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            includedFee.Checked = Settings.BaoGomCaPhiGiaoDich;
            includedTax.Checked = Settings.BaoGomCaThue;

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
                        Utils.DebugPrint(ex.Message);
                        MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Settings.DBConnection != null)
                    {
                        Settings.DBConnection.Close();
                    }
                    OnDBFileChanged(Settings.DataFile);
                    Settings.DBConnection = conn;
                    closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;
                    LoadExchangesList();
                    LoadStockList();
                    LoadPortfolio();
                }
                else
                {
                    Settings.DataFile = string.Empty;
                }
            }

            importFromExcelMenuItem.Enabled = (Excel != null);
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
                    Utils.DebugPrint(StockListHitTestInfo.Location.ToString());
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
                    CoPhieu stock = selItem.Tag as CoPhieu;
                    if (null != stock)
                    {
                        StockManager form = new StockManager(stock);
                        if(form.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        else
                        {
                            LoadStockList();
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
                MessageBox.Show("Chọn một cổ phiếu đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selItem = stockCodeListView.SelectedItems[0];
            if (null == selItem || null == selItem.Tag)
            {
                MessageBox.Show("Chọn một cổ phiếu đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CoPhieu selStock = selItem.Tag as CoPhieu;
            if (null == selStock)
            {
                MessageBox.Show("Chọn một cổ phiếu đề mua!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Chọn một cổ phiếu đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selItem = stockCodeListView.SelectedItems[0];
            if(null==selItem || null == selItem.Tag)
            {
                MessageBox.Show("Chọn một cổ phiếu đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CoPhieu selStock = selItem.Tag as CoPhieu;
            if (null == selStock)
            {
                MessageBox.Show("Chọn một cổ phiếu đề xóa!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(MessageBox.Show(string.Format("Bạn chắc chắn muốn xóa cổ phiếu có mã \"{0}\"?", selStock.MaCoPhieu), "Xóa cổ phiếu", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!= DialogResult.Yes)
            {
                return;
            }

            var cmd = new SQLiteCommand(string.Format("DELETE FROM CoPhieu WHERE ID={0}", selStock.ID), Settings.DBConnection);
            if (cmd.ExecuteNonQuery() <= 0)
            {
                MessageBox.Show(string.Format("Xóa cổ phiếu có mã \"{0}\" thất bại!", selStock.MaCoPhieu), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                LoadStockList();
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
            LoadStockList();
            LoadExchangesList();
            LoadPortfolio();
        }

        private void stockCodeListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void stockCodeListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Settings.DataFile = files[0];
            if (Settings.DataFile != null && Settings.DataFile != string.Empty)
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
                        Utils.DebugPrint(ex.Message);
                        MessageBox.Show(ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (Settings.DBConnection != null)
                    {
                        Settings.DBConnection.Close();
                    }
                    Settings.DBConnection = conn;
                    closeDataFileMenuItem.Enabled = Settings.DBConnection != null && Settings.DBConnection.State == ConnectionState.Open;
                    LoadExchangesList();
                    LoadStockList();
                }
                else
                {
                    Settings.DataFile = string.Empty;
                }
            }
        }

        private void portfolioListView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Right)
            {
                PortfolioListHitTestInfo = portfolioListView.HitTest(e.X, e.Y);
                if(PortfolioListHitTestInfo.Location == ListViewHitTestLocations.Label)
                {
                    portfolioListView.ContextMenuStrip = contextMenuStrip4;
                }
                else if(PortfolioListHitTestInfo.Location == ListViewHitTestLocations.None)
                {
                    portfolioListView.ContextMenuStrip = contextMenuStrip3;
                }
            }
        }

        private void importFromExcelMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Chọn tệp tin";
            dlg.Filter = "Tất cả các tệp tin(*.*)|*.*|Tệp tin Excel(*.xlsx)|*.xlsx";
            dlg.FilterIndex = 1;
            dlg.CheckPathExists = true;
            dlg.CheckFileExists = true;
            dlg.ShowReadOnly = true;

            if(dlg.ShowDialog()!= DialogResult.OK)
            {
                return;
            }

            string excelFiles = dlg.FileName;
            Workbook wb = null;
            try
            {
                wb = Excel.Workbooks.Open(excelFiles);
            }
            catch(Exception ex)
            {
                Utils.DebugPrint(ex.Message);
                MessageBox.Show(ex.Message, string.Format("Lỗi 0x{0:X8}", ex.HResult), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            backgroundWorker1.RunWorkerAsync(wb);

        }

        private void detailMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selItem = portfolioListView.SelectedItems[0];
            if (null != selItem.Tag)
            {
                PortfolioItem pi = selItem.Tag as PortfolioItem;
                if (null != pi)
                {
                    PortfolioDetailForm frm = new PortfolioDetailForm();
                    frm.PortfolioItem = pi;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //
                    }
                }
            }
        }

        private void buyMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem selItem = null;
            PortfolioItem pi = null;
            if (portfolioListView.SelectedItems.Count > 0)
                selItem = portfolioListView.SelectedItems[0];
            if (selItem != null)
                pi = selItem.Tag as PortfolioItem;
            if (pi == null)
            {
                MessageBox.Show("Vui lòng chọn 1 cổ phiếu để mua thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BuyForm frm = new BuyForm(new CoPhieu(GetStockID(pi.MaCoPhieu), pi.MaCoPhieu));
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            LoadPortfolio();
        }

        private void sellMenuItem_Click(object sender, EventArgs e)
        {

        }

        private string DynamicToString(dynamic val)
        {
            object obj = (object)val;
            if (obj.GetType().Equals(typeof(System.DateTime)))
            {
                DateTime dt = (DateTime)val;
                return dt.ToString();
            }
            else if (obj.GetType().Equals(typeof(System.Double)))
            {
                System.Double dt = (System.Double)val;
                return dt.ToString();
            }
            else if (obj.GetType().Equals(typeof(System.String)))
            {
                System.String dt = (System.String)val;
                return dt.ToString();
            }
            else if (obj.GetType().Equals(typeof(int)))
            {
                int n = (int)val;
                return n.ToString();
            }


            return string.Empty;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);

            Workbook wb = e.Argument as Workbook;
            int row = 11;
            int temp = 0;
            Range cellDate = null;
            Range cellType = null;
            Range cellCode = null;
            Range cellCount = null;
            Range cellPrice = null;
            Range cellCommand = null;
            Range cellFee = null;
            Range cellTax = null;

            string sDate = string.Empty;
            string sType = string.Empty;
            string sCode = string.Empty;
            string sCount = string.Empty;
            string sPrice = string.Empty;
            string sCommand = string.Empty;
            string sFee = string.Empty;
            string sTax = string.Empty;

            foreach (Worksheet ws in wb.Sheets)
            {
                while (true)
                {
                    cellDate = ws.Cells[row, 3];
                    cellType = ws.Cells[row, 4];
                    cellCode = ws.Cells[row, 5];
                    cellCount = ws.Cells[row, 8];
                    cellPrice = ws.Cells[row, 9];
                    cellFee = ws.Cells[row, 12];
                    cellTax = ws.Cells[row, 13];
                    cellCommand = ws.Cells[row, 15];

                    sDate = string.Empty;
                    sType = string.Empty;
                    sCode = string.Empty;
                    sCount = string.Empty;
                    sPrice = string.Empty;
                    sCommand = string.Empty;
                    sFee = string.Empty;
                    sTax = string.Empty;

                    if (cellCode.Value == null)
                    {
                        break;
                    }

                    if (cellCode.Value != null)
                        sCode = DynamicToString(cellCode.Value);
                    
                    if (sCode == string.Empty)
                        break;

                    if (cellDate.Value != null)
                        sDate = DynamicToString(cellDate.Value);
                    if (cellType.Value != null)
                        sType = DynamicToString(cellType.Value);
                    if (cellCount.Value != null)
                        sCount = DynamicToString(cellCount.Value);
                    if (cellPrice.Value != null)
                        sPrice = DynamicToString(cellPrice.Value);
                    if (cellCommand.Value != null)
                        sCommand = DynamicToString(cellCommand.Value);
                    if (cellFee.Value != null)
                        sFee = DynamicToString(cellFee.Value);
                    if (cellTax.Value != null)
                        sTax = DynamicToString(cellTax.Value);

                    Utils.DebugPrint(string.Format("Ngày GD:{0}\tLoại GD:{1}\tMã CK:{2}\tKL:{3}\tGiá:{4}\tPhí:{5}\tThuế:{6}\tLệnh:{7}", sDate, sType, sCode, sCount, sPrice, sFee, sTax, sCommand));


                    if (sType.ToLower() == "Mua".ToLower())
                    {
                        GiaoDichMua  giaoDichMua = new GiaoDichMua();
                        giaoDichMua.CoPhieu = GetStockID(sCode.Trim());
                        if (giaoDichMua.CoPhieu == 0)
                        {
                            giaoDichMua.CoPhieu = AddStock(sCode.Trim());
                            ReloadStockListAfterImport = true;
                        }

                        giaoDichMua.GiaMua = (int)float.Parse(sPrice.Replace(".","").Trim());
                        giaoDichMua.KhoiLuongMua = int.Parse(sCount.Replace(",", "").Trim());

                        if (giaoDichMua.KhoiLuongMua > 0 && giaoDichMua.GiaMua != 0)
                        {
                            giaoDichMua.ThoiGianMua = Utils.DateTimeFromString(sDate.Trim());
                            if(int.TryParse(sFee, out temp))
                            {
                                giaoDichMua.PhiGiaoDichMua = temp;
                            }
                            else
                            {
                                giaoDichMua.PhiGiaoDichMua = (int)((float)giaoDichMua.GiaMua * (float)giaoDichMua.KhoiLuongMua / (float)100 * (float)Settings.System.PhiGiaoDichMua);
                            }
                            giaoDichMua.LoaiLenh = sCommand;

                            if (!AddBuyTransaction(giaoDichMua))
                            {
                                Utils.DebugPrint(giaoDichMua.ToString());
                            }
                            ReloadPortfolioAfterImport = true;
                        }
                        else
                        {
                            row++;
                            continue;
                        }
                    }
                    else if(sType.ToLower() == "Bán".ToLower())
                    {
                        //TODO
                        int IDMua = 0;
                        if (PortfolioMap.ContainsKey(sCode))
                        {
                            PortfolioItem pi = PortfolioMap[sCode];
                            if (!pi.DaBanHet)
                            {
                                int count = 0;
                                foreach(GiaoDichMua mua in pi.DanhsachGiaoDichMua)
                                {
                                    count += mua.KhoiLuongMua;
                                    if(count>=pi.KhoiLuongBan)
                                    {
                                        IDMua = mua.ID;
                                        break;
                                    }
                                }
                            }
                        }

                        GiaoDichBan ban = new GiaoDichBan();
                        ban.GiaoDichMua = IDMua;
                        ban.KhoiLuongBan = int.Parse(sCount.Replace(",", "").Trim());
                        ban.GiaBan = (int)float.Parse(sPrice.Replace(".", "").Trim());
                        if (ban.KhoiLuongBan > 0 && ban.GiaBan != 0)
                        {
                            ban.ThoiGianBan = Utils.DateTimeFromString(sDate.Trim());
                            ban.PhiGiaoDichBan = int.Parse(sFee.Replace(",", "").Trim());
                            ban.ThueGiaoDichBan = int.Parse(sTax.Replace(",", "").Trim());

                            if (!AddSellTransaction(ban))
                            {
                                Utils.DebugPrint(ban.ToString());
                            }
                            ReloadPortfolioAfterImport = true;
                        }
                        else
                        {
                            row++;
                            continue;
                        }
                    }

                    row++;
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                progressBar1.Minimum = 0;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.Visible = true;
                this.UseWaitCursor = true;
                contextMenuStrip1.Enabled = contextMenuStrip2.Enabled = contextMenuStrip3.Enabled = contextMenuStrip4.Enabled = false;
                menuStrip1.Enabled = false;
                StatusLabel.Visible = false;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            contextMenuStrip1.Enabled = contextMenuStrip2.Enabled = contextMenuStrip3.Enabled = contextMenuStrip4.Enabled = true;
            menuStrip1.Enabled = true;
            progressBar1.Visible = false;
            StatusLabel.Visible = true;
            this.UseWaitCursor = false;
            Excel.Quit();
            if (ReloadStockListAfterImport)
            {
                LoadStockList();
                ReloadStockListAfterImport = false;
            }
            if (ReloadPortfolioAfterImport)
            {
                LoadPortfolio();
                ReloadPortfolioAfterImport = false;
            }
            tabControl1_SelectedIndexChanged(this, new EventArgs());
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null != Excel)
            {
                Excel.Application.Quit();
            }
        }

        private void portfolioListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (portfolioListView.SelectedItems.Count <= 0)
            {
                portfolioListView.ContextMenuStrip = null;
            }
            else
            {
                portfolioListView.ContextMenuStrip = contextMenuStrip4;
                ListViewItem selItem = portfolioListView.SelectedItems[0];
                if (selItem == null)
                {
                    detailMenuItem.Enabled = false;
                    buyMenuItem.Enabled = false;
                    sellMenuItem.Enabled = false;
                }
                else
                {
                    PortfolioItem pi = selItem.Tag as PortfolioItem;
                    if (pi == null)
                    {
                        detailMenuItem.Enabled = false;
                        buyMenuItem.Enabled = false;
                        sellMenuItem.Enabled = false;
                    }
                    else
                    {
                        detailMenuItem.Enabled = true;
                        buyMenuItem.Enabled = true;
                        if (pi.DaBanHet) {
                            sellMenuItem.Enabled = false;
                            sellMenuItem.Text = "Đã bán hết!";
                        }
                        else {
                            sellMenuItem.Enabled = true;
                            sellMenuItem.Text = "Bán";
                        }
                    }
                }
            }
        }

        private void StatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selPage = tabControl1.SelectedTab;
            if (selPage == stockTabPage)
            {
                StatusLabel.Text = string.Format("Tổng số: {0} mã cổ phiếu", stockCodeListView.Items.Count);
            }
            else if (selPage == portfolioTagPage)
            {
                int TongLaiLo = (TongTienBan - TongTienMua);
                if (Settings.BaoGomCaPhiGiaoDich)
                    TongLaiLo -= TongTienPhiGiaoDich;
                if (Settings.BaoGomCaThue)
                    TongLaiLo -= TongTienThue;

                StatusLabel.Text = string.Format("Tổng giá trị mua: {0} ₫  |  Tổng giá trị bán: {1} ₫  |  Tổng phí giao dịch: {2} ₫  |  Tổng thuế: {3} ₫  |  Tổng lãi/lỗ: {4} ₫", TongTienMua.ToString("n0"), TongTienBan.ToString("n0"), TongTienPhiGiaoDich.ToString("n0"), TongTienThue.ToString("n0"), TongLaiLo.ToString("n0"));
            }
            else
            {
                StatusLabel.Text = string.Empty;
            }
        }

        private void portfolioListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && portfolioListView.SelectedItems.Count == 1)
            {
                ListViewItem selItem = portfolioListView.SelectedItems[0];
                if (null != selItem.Tag)
                {
                    PortfolioItem pi = selItem.Tag as PortfolioItem;
                    if (null != pi)
                    {
                        PortfolioDetailForm frm = new PortfolioDetailForm();
                        frm.PortfolioItem = pi;
                        if(frm.ShowDialog()== DialogResult.OK)
                        {
                            //
                        }
                    }
                }
            }
        }

        private void includedFee_Click(object sender, EventArgs e)
        {
            includedFee.Checked = !includedFee.Checked;
            Settings.BaoGomCaPhiGiaoDich = includedFee.Checked;
            tabControl1_SelectedIndexChanged(sender, new EventArgs());
        }

        private void includedTax_Click(object sender, EventArgs e)
        {
            includedTax.Checked = !includedTax.Checked;
            Settings.BaoGomCaThue = includedTax.Checked;
            tabControl1_SelectedIndexChanged(sender, new EventArgs());
        }
    }
}
