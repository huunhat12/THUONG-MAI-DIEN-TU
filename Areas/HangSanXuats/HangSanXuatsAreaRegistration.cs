using System.Web.Mvc;

namespace QLBANDTDD.Areas.HangSanXuats
{
    public class HangSanXuatsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HangSanXuats";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Hãng Sản Xuất", "hang-san-xuat", new { controller = "HangSanXuat", action = "Index" });
            context.MapRoute("Sửa Hãng Sản Xuất", "sua-hang-san-xuat", new { controller = "HangSanXuat", action = "SuaHangSX" });
            context.MapRoute("Xóa Hãng Sản Xuất", "xoa-hang-san-xuat", new { controller = "HangSanXuat", action = "XoaHangSX" });
            context.MapRoute("Thêm Hãng Sản Xuất ", "them-hang-san-xuat", new { controller = "HangSanXuat", action = "ThemHangSX" });
        }
    }
}