using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class MenuItem
    {
        public LoaiSanPham loaiSp { get; set; }
        public List<HangSanXuat> hangSX { get; set; }
    }
}