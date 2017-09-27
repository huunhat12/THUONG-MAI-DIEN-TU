using System.Web.Mvc;

namespace QLBANDTDD.Areas.TinTucs
{
    public class TinTucsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TinTucs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Tin Tức", "tin-tuc", new { controller = "TinTuc", action = "Index" });
            context.MapRoute("Sửa Tin Tức", "sua-tin-tuc", new { controller = "TinTuc", action = "SuaTinTuc" });
            context.MapRoute("Xóa Tin Tức", "xoa-tin-tuc", new { controller = "TinTuc", action = "XoaTinTuc" });
            context.MapRoute("Thêm Tin Tức ", "them-tin-tuc", new { controller = "TinTuc", action = "ThemTinTuc" });
            context.MapRoute("Nhóm Tin Tức", "nhom-tin-tuc", new { controller = "NhomTinTuc", action = "Index" });
            context.MapRoute("Sửa Nhóm Tin Tức", "sua-nhom-tin-tuc", new { controller = "NhomTinTuc", action = "SuaNhomTinTuc" });
            context.MapRoute("Xóa Nhóm Tin Tức", "xoa-nhom-tin-tuc", new { controller = "NhomTinTuc", action = "XoaNhomTinTuc" });
            context.MapRoute("Thêm Nhóm Tin Tức ", "them-nhom-tin-tuc", new { controller = "NhomTinTuc", action = "ThemNhomTinTuc" });
        }
    }
}