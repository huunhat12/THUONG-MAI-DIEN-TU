using System.Web.Mvc;

namespace QLBANDTDD.Areas.TaiKhoans
{
    public class TaiKhoansAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TaiKhoans";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Tài Khoản", "tai-khoan", new { controller = "TaiKhoan", action = "Index" });
            context.MapRoute("Thêm Tài Khoản", "them-tai-khoan", new { controller = "TaiKhoan", action = "ThemTaikhoan" });
            context.MapRoute("Thay Đổi Mật Khẩu", "thay-doi-mat-khau", new { controller = "TaiKhoan", action = "ThayDoiMK" });
            context.MapRoute("Xóa Tài Khoản", "xoa-tai-khoan", new { controller = "TaiKhoan", action = "XoaTaiKhoan" });
        }
    }
}