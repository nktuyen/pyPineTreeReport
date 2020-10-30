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

namespace TradingAssistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Settings = Settings.Instance;
        }

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

        private string BuildConnectionString(string dbFile)
        {
            return string.Format("Data Source={0}; Mode=ReadWrite", dbFile);
        }

        private DateTime DateTimeFromString(string sDate)
        {
            if (sDate==null || sDate==string.Empty)
            {
                return new DateTime();
            }

            int pos = -1;
            int year = 0;
            int month = 0;
            int day = 0;
            
            string[] arrTemp = sDate.Split('/');
            if (arrTemp.Length > 0)
            {
                day = int.Parse(arrTemp[0]);
            }
            if (arrTemp.Length > 1)
            {
                month = int.Parse(arrTemp[1]);
            }
            if (arrTemp.Length > 1)
            {
                pos = arrTemp[2].IndexOf(' ');
                if (pos == -1)
                    year = int.Parse(arrTemp[2]);
                else
                    year = int.Parse(arrTemp[2].Substring(0, pos));
            }

            return new DateTime(year, month, day);
        }

        private bool AddBuyTransaction(GiaoDichMua transac)
        {
            if(Settings.DBConnection==null||Settings.DBConnection.State!= ConnectionState.Open)
            {
                return false;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("INSERT INTO GiaoDichMua(CoPhieu,KhoiLuongMua,GiaMua,ThoiGianMua,PhiGiaoDichMua) VALUES({0},{1},{2},'{3:D2}/{4:D2}/{5:D4}-{6:D2}:{7:D2}:{8:D2}',{9})", transac.CoPhieu, transac.KhoiLuongMua, transac.GiaMua, transac.ThoiGianMua.Day, transac.ThoiGianMua.Month, transac.ThoiGianMua.Year, transac.ThoiGianMua.Hour, transac.ThoiGianMua.Minute, transac.ThoiGianMua.Second, transac.PhiGiaoDichMua), Settings.DBConnection);
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
            if(Settings.DBConnection==null||Settings.DBConnection.State!= ConnectionState.Open)
            {
                return;
            }

            SQLiteCommand cmd = new SQLiteCommand(string.Format("SELECT DISTINCT CoPhieu FROM GiaoDichMua"), Settings.DBConnection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int ID = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ID = reader.GetInt32(0);
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

            var cmd = new SQLiteCommand("SELECT PhiGiaoDichMua,PhiGiaoDichBan,PhiUngTruocTienBan,ThueTrenMoiGiaoDich FROM HeThong", Settings.DBConnection);
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
                    Settings.System.PhiUngTruocTienBan = fTemp;

                    fTemp = reader.GetFloat(3);
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
                        Debug.Print(ex.Message);
                        res = 0;
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
                        Debug.Print(ex.Message);
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
                            Debug.Print(ex.Message);
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
                        stock.NgayNiemYet = new DateTime(year, month, day);
                    }
                    catch(Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }

                    ID = stockCodeListView.Items.Count + 1;
                    ListViewItem item = stockCodeListView.Items.Add(ID.ToString());
                    item.Tag = stock;
                    item.SubItems.Add(code);
                    item.SubItems.Add(name);
                    item.SubItems.Add(Settings.DanhSachSanGiaoDich[stock.SanNiemYet].MaSan);
                    item.SubItems.Add(string.Format("{0:D2}/{1:D2}/{2:D4}", stock.NgayNiemYet.Day, stock.NgayNiemYet.Month, stock.NgayNiemYet.Year));
                }
            }
            /*catch(Exception ex)
            {
                Debug.Print(ex.Message);
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

                    LoadStockList();
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
                    LoadExchangesList();
                    LoadStockList();
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
                Debug.Print(ex.Message);
                MessageBox.Show(ex.Message, string.Format("Lỗi 0x{0:X8}", ex.HResult), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            backgroundWorker1.RunWorkerAsync(wb);

        }

        private void detailMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void buyMenuItem_Click(object sender, EventArgs e)
        {

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
            Range cellDate = null;
            Range cellType = null;
            Range cellCode = null;
            Range cellCount = null;
            Range cellPrice = null;
            Range cellCommand = null;
            string sDate = string.Empty;
            string sType = string.Empty;
            string sCode = string.Empty;
            string sCount = string.Empty;
            string sPrice = string.Empty;
            string sCommand = string.Empty;

            foreach (Worksheet ws in wb.Sheets)
            {
                while (true)
                {
                    cellDate = ws.Cells[row, 2];
                    cellType = ws.Cells[row, 3];
                    cellCode = ws.Cells[row, 4];
                    cellCount = ws.Cells[row, 7];
                    cellPrice = ws.Cells[row, 8];
                    cellCommand = ws.Cells[row, 13];

                    sDate = string.Empty;
                    sType = string.Empty;
                    sCode = string.Empty;
                    sCount = string.Empty;
                    sPrice = string.Empty;
                    sCommand = string.Empty;

                    if (cellDate.Value == null && cellType.Value == null && cellCode.Value == null && cellCount.Value == null && cellPrice.Value == null && cellCommand.Value == null)
                    {
                        break;
                    }


                    if (cellDate.Value != null)
                        sDate = DynamicToString(cellDate.Value);
                    if (cellType.Value != null)
                        sType = cellType.Value;
                    if (cellCode.Value != null)
                        sCode = cellCode.Value;
                    if (cellCount.Value != null)
                        sCount = cellCount.Value;
                    if (cellPrice.Value != null)
                        sPrice = cellPrice.Value;
                    if (cellCommand.Validation != null)
                        sCommand = cellCommand.Value;

                    Debug.Print(string.Format("Ngày GD:{0}\tLoại GD:{1}\tMã CK:{2}\tKL:{3}\tGiá:{4}\tLệnh:{5}", sDate, sType, sCode, sCount, sPrice, sCommand));

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
                            giaoDichMua.ThoiGianMua = DateTimeFromString(sDate.Trim());
                            giaoDichMua.PhiGiaoDichMua = (int)System.Math.Round((float)giaoDichMua.KhoiLuongMua * (float)giaoDichMua.GiaMua * Settings.System.PhiGiaoDichMua);

                            if (!AddBuyTransaction(giaoDichMua))
                            {
                                Debug.Print(giaoDichMua.ToString());
                            }
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
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            if (ReloadStockListAfterImport)
            {
                LoadStockList();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null != Excel)
            {
                Excel.Application.Quit();
            }
        }
    }
}
