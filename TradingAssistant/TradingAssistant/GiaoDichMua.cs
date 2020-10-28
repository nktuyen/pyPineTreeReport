using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    class GiaoDichMua
    {
        public int ID { get; set; } = 0;
        public int CoPhieu { get; set; } = 0;
        public int KhoiLuongMua { get; set; } = 0;
        public int GiaMua { get; set; } = 0;
        public string LoaiLenh { get; set; } = string.Empty;
        public DateTime ThoiGianMua { get; set; } = DateTime.Today;
        public int PhiGiaoDichMua { get; set; } = 0;
    }
}
