using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.Areas.SanPhams.Models
{
    public class SanPhamModel
    {
        public int MaSP { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Loại sản phẩm")]
        public int LoaiSP { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Hãng sản xuất")]
        public int HangSX { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Xuất xứ")]
        public string XuatXu { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Giá gốc")]
        public int GiaGoc { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Giá tiền")]
        public int GiaTien { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        public bool HienThi { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
        public string AnhKhac { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        public bool IsNew { get; set; }
        public bool IsHot { get; set; }
        public string LoaiSPham { get; set; }
        public string HangSXuat { get; set;}
    }
}