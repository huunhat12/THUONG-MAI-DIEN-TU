using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class SanPhamCT
    {
        public List<ImageLinkViewModel> sanphamcungloai { get; set; }
        public SanPham sanpham { get; set; }
        public IQueryable<SanPham> spdx { get; set; }
        public List<ImageLinkViewModel> sanphamnoibat { get; set; }
    }
}