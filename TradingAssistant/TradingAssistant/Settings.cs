﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data.SQLite;

namespace TradingAssistant
{
    class HeThong
    {
        public float PhiGiaoDichMua { get; set; } = 0;
        public float PhiGiaoDichBan { get; set; } = 0;
        public float PhiUngTruocTienBan { get; set; } = 0;
    }

    class Settings
    {
        private static Settings _instance = null;
        private const string STR_KEY = "SOFTWARE\\NKTUYEN\\TradingAssistant\\Settings\\";
        private const string STR_KEY_RECENT_FILE = "RecentFile";
        private const string STR_VALUE_RECENT_FILE = "";
        private RegistryKey RegistryKey { get; set; } = null;
        public static Settings Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        public HeThong System { get; set; } = null;
        public string DataFile { get; set; } = string.Empty;
        public List<SanGiaoDich> DanhSachSanGiaoDich { get; } = new List<SanGiaoDich>();
        public SQLiteConnection DBConnection { get; set; } = null;
        private Settings()
        {
            try
            {
                RegistryKey = Registry.CurrentUser.OpenSubKey(STR_KEY, true);
                if(null == RegistryKey)
                {
                    RegistryKey = Registry.CurrentUser.CreateSubKey(STR_KEY, true);
                }
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
                RegistryKey = null;
            }

            if (null != RegistryKey)
            {
                DataFile = (string)RegistryKey.GetValue(STR_KEY_RECENT_FILE, STR_VALUE_RECENT_FILE);
            }
        }

        ~Settings()
        {
            if(null != RegistryKey)
            {
                RegistryKey.SetValue(STR_KEY_RECENT_FILE, DataFile);
            }
        }
    }
}
