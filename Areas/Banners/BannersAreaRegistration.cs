using System.Web.Mvc;

namespace QLBANDTDD.Areas.Banners
{
    public class BannersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Banners";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Banner", "banner", new { controller = "Banner", action = "Index" });
            context.MapRoute("Sửa Banner", "sua-banner", new { controller = "Banner", action = "SuaBanner" });
            context.MapRoute("Xóa Banner", "xoa-banner", new { controller = "Banner", action = "XoaBanner" });
            context.MapRoute("Thêm Banner ", "them-banner", new { controller = "Banner", action = "ThemBanner" });
            context.MapRoute("Nhóm Banner", "nhom-banner", new { controller = "BannerGroup", action = "Index" });
            context.MapRoute("Sửa Nhóm Banner", "sua-nhom-banner", new { controller = "BannerGroup", action = "SuaNhomBanner" });
            context.MapRoute("Xóa Nhóm Banner", "xoa-nhom-banner", new { controller = "BannerGroup", action = "XoaNhomBanner" });
            context.MapRoute("Thêm Nhóm Banner ", "them-nhom-banner", new { controller = "BannerGroup", action = "ThemNhomBanner" });
        }
    }
}