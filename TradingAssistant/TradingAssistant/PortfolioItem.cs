using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    class PortfolioItem
    {
        public string MaCoPhieu { get; set; } = string.Empty;
        public int KhoiLuongMua { get; set; } = 0;
        public int GiaMuaTriMua { get; set; } = 0;
        public int KhoiLuongMuaBan { get; set; } = 0;
        public int GiaMuaTriBan { get; set; } = 0;
        public int LaiLo { get; set; } = 0;
        public List<GiaoDichMua> DanhSachGiaoDichMua { get; set; } = new List<GiaoDichMua>();
        public void Calc()
        {
            if (MaCoPhieu == null || MaCoPhieu == string.Empty)
            {
                KhoiLuongMua = 0;
                GiaMuaTriMua = 0;
                KhoiLuongMuaBan = 0;
                GiaMuaTriBan = 0;
                LaiLo = 0;
                DanhSachGiaoDichMua.Clear();
                return;
            }
        }
    }
}
