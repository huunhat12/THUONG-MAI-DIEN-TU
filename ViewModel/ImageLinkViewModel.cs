using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class ImageLinkViewModel
    {
        public int MaSP { get; set; }
        public string Hinh { get; set; }
        public string Link { get; set; }
        public string TenSP { get; set; }
        public string HangSX { get; set; }
        public int? GiaTien { get; set; }
        public int? GiaGoc { get; set; }
        public Double? Phantram { get; set; }
        public Boolean IsNew { get; set; }
        public Boolean IsHot { get; set; }
        public int? SoLuong { get; set; }
    }
}