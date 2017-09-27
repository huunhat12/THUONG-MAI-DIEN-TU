using System.Web.Mvc;

namespace QLBANDTDD.Areas.LoaiSanPhams
{
    public class LoaiSanPhamsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LoaiSanPhams";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Loại Sản Phẩm", "loai-san-pham", new { controller = "LoaiSanPham", action = "Index" });
            context.MapRoute("Sửa Loại Sản Phẩm", "sua-loai-san-pham", new { controller = "LoaiSanPham", action = "SuaLoaiSP" });
            context.MapRoute("Xóa Loại Sản Phẩm", "xoa-loai-san-pham", new { controller = "LoaiSanPham", action = "XoaLoaiSP" });
            context.MapRoute("Thêm Loại Sản Phẩm ", "them-loai-san-pham", new { controller = "LoaiSanPham", action = "ThemLoaiSP" });
        }
    }
}