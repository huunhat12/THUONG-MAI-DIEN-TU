using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class CartItem
    {
        public SanPham sanpham { get; set; }
        public int Quantity { get; set; }
        public double Totalprice { get; set; }
    }
}