using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.Banners.Models
{
    public class BannerModel
    {
        public int BannerId { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Mã nhóm Banner ")]
        public int BannerGroupId { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Hình Anh")]
        public string HinhAnh { get; set; }
        public string Link { get; set; }
        public int ThuTu { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tiêu Đề")]
        public string TieuDe { get; set; }
        public bool HienThi { get; set; }
        public string NhomBanner { get; set; }
    }
}