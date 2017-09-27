using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.TaiKhoans.Models
{
    public class TaikhoanChanged
    {
        public int MaTK { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Mật Khẩu Mới")]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nhập Lại Mật Khẩu")]
        [Compare("MatKhauMoi", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không khớp!")]
        public string NhapLaiMatKhau { get; set; }
    }
}