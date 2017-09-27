using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.LoaiSanPhams.Models
{
    public class LoaiSPModel
    {
        public int MaLoai { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tên loại sản phẩm")]
        public string TenLoai { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        public bool HienThi { get; set; }
    }
}