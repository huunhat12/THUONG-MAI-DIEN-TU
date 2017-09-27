using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.TinTucs.Models
{
    public class NhomTinModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }
        public bool HienThi { get; set; }
    }
}