using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.TinTucs.Models
{
    public class TinTucModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Ngày đăng")]
        public DateTime NgayDang { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Nội dung tóm tắt")]
        public string NoiDungTT { get; set; }
        public string TenNhom { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Hiển thị")]
        public bool HienThi { get; set; }
        public int MaNhom { get; set; }
    }
}