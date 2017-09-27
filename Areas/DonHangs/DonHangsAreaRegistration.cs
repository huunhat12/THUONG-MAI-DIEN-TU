using System.Web.Mvc;

namespace QLBANDTDD.Areas.DonHangs
{
    public class DonHangsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DonHangs";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Đơn Hàng", "don-hang", new { controller = "DonHang", action = "Index" });
            context.MapRoute("Chi Tiết Đơn Hàng", "chi-tiet-don-hang", new { controller = "DonHang", action = "Details" });
        }
    }
}