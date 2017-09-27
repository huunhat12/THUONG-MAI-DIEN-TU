using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.Banners.Models
{
    public class BannerGroupModel
    {
        public int BannerGroupId { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        public bool HienThi { get; set; }
    }
}