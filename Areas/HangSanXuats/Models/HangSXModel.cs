using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.HangSanXuats.Models
{
    public class HangSXModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tên hãng sản xuất")]
        public string TenHang { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Trụ sở chính")]
        public string TruSoChinh { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Quốc Gia")]
        public string QuocGia { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        public bool HienThi { get; set; }
    }
}