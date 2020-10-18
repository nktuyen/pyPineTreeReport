using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    public class SanGiaoDich
    {
        public int ID { get; set; } = 0;
        public string MaSan { get; set; } = string.Empty;
        public string TenSan { get; set; } = string.Empty;
        public string TrangWeb { get; set; } = string.Empty;

        public SanGiaoDich(int _id = 0, string _code = "", string _name = "", string _web = "")
        {
            ID = _id;
            MaSan = _code;
            TenSan = _name;
            TrangWeb = _web;
        }
    }
}
