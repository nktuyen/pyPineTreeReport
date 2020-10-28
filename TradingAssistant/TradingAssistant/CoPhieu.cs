using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    public class CoPhieu
    {
        public int ID { get; set; } = 0;
        public string MaCoPhieu { get; set; } = string.Empty;
        public string TenDoanhNghiep { get; set; } = string.Empty;
        public int SanNiemYet { get; set; } = 0;
        public long KhoiLuongNiemYet { get; set; } = 0;
        public long KhoiLuongLuuHanh { get; set; } = 0;
        public DateTime NgayNiemYet { get; set; } = new DateTime();
    }
}
