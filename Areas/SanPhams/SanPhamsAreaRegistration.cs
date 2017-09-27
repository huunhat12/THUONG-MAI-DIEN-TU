using System.Web.Mvc;

namespace QLBANDTDD.Areas.SanPhams
{
    public class SanPhamsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SanPhams";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Sản Phẩm", "san-pham", new { controller = "SanPham", action = "Index" });
            context.MapRoute("Sửa Sản Phẩm", "sua-san-pham", new { controller = "SanPham", action = "SuaSanPham" });
            context.MapRoute("Xóa Sản Phẩm", "xoa-san-pham", new { controller = "SanPham", action = "XoaSanPham" });
            context.MapRoute("Thêm Sản Phẩm ", "them-san-pham", new { controller = "SanPham", action = "ThemSanPham" });
            context.MapRoute("Sửa Thông Số KT", "sua-thong-so-ky-thuat", new { controller = "SanPham", action = "SuaThongSoKT" });
            context.MapRoute("Xóa Thông Số KT", "xoa-thong-so-ky-thuat", new { controller = "SanPham", action = "XoaThongSoKT" });
            context.MapRoute("Thêm Thông Số KT ", "them-thong-so-ky-thuat", new { controller = "SanPham", action = "ThemThongSoKT" });
        }
    }
}