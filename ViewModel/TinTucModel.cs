using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class TinTucModel
    {
        public int Id { get; set; }
        public string Hinh { get; set; }
        public string Link { get; set; }
        public string TieuDe { get; set; }
        public string NoiDungTT { get; set; }
        public DateTime NgayDang { get; set; }
        public string TenNhomTT { get; set; }
    }
}