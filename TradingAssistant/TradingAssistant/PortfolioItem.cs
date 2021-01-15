using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant
{
    public class PortfolioItem
    {
        private int _lailo = 0;
        private readonly List<GiaoDichMua> _danhsachGiaoDichMua = null;
        public string MaCoPhieu { get; set; } = string.Empty;
        public int KhoiLuongMua { get; set; } = 0;
        public int GiaTriMua { get; set; } = 0;
        public int KhoiLuongBan { get; set; } = 0;
        public int GiaTriBan { get; set; } = 0;
        public bool Updated { get; set; } = false;
        public bool DaBanHet { get; set; } = false;
        public int LaiLo
        {
            get
            {
                if (Updated) // Can phai tinh lai lo
                {
                    DaBanHet = false;
                    if (KhoiLuongBan > 0)
                    {
                        _lailo = GiaTriBan - GiaTriMua;
                        if (KhoiLuongBan == KhoiLuongMua)
                        {
                            DaBanHet = true;
                        }
                    }
                    else
                        _lailo = 0;
                }
                return _lailo;
            }
        }

        public List<GiaoDichMua> DanhsachGiaoDichMua { get { return _danhsachGiaoDichMua; } }

        public PortfolioItem()
        {
            _danhsachGiaoDichMua = new List<GiaoDichMua>();
        }
    }
}
