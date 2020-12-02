using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    public class GiaoDichBan
    {
        public int ID { get; set; } = 0;
        public int GiaoDichMua { get; set; } = 0;
        public int KhoiLuongBan { get; set; } = 0;
        public int GiaBan { get; set; } = 0;
        public string LoaiLenhBan { get; set; } = string.Empty;
        public int PhiGiaoDichBan { get; set; } = 0;
        public int ThueGiaoDichBan { get; set; } = 0;
        public DateTime ThoiGianBan { get; set; } = DateTime.Today;

        public GiaoDichBan()
        {

        }
    }
}
