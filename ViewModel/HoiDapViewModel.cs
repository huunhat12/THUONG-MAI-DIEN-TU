using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class HoiDapViewModel
    {
        public int Id { get; set; }
        public string TenNguoiHoi { get; set; }
        public DateTime NgayHoi { get; set; }
        public string NoiDungCH { get; set; }
        public string TieuDeCH { get; set; }
        public string NoiDungTL { get; set; }
    }
}