using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBANDTDD.ViewModel
{
    public class DonHangModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Tên Khách Hàng")]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "{0} không để trống!")]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
    }
}