using System.Web.Mvc;

namespace QLBANDTDD.Areas.Home
{
    public class HoiDapAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HoiDap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("HoiDap", "hoi-dap", new { controller = "HoiDap", action = "Index" });
            context.MapRoute("Sửa Hỏi Đáp", "sua-hoi-dap", new { controller = "HoiDap", action = "Edit" });
            context.MapRoute("Xóa Hỏi Đáp", "xoa-hoi-dap", new { controller = "HoiDap", action = "Delete" });
            context.MapRoute("Trả Lời Hỏi Đáp", "tra-loi-hoi-dap", new { controller = "HoiDap", action = "TraLoi" });
            context.MapRoute("Cập Nhật Trả Lời Hỏi Đáp", "cap-nhat-tra-loi-hoi-dap", new { controller = "HoiDap", action = "CapNhatTraLoi" });
        }
    }
}