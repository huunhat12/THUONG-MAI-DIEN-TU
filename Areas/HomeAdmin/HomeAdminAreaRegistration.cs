using System.Web.Mvc;

namespace QLBANDTDD.Areas.Home
{
    public class HomeAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HomeAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Trang Chu", "trang-chu", new { controller = "Home", action = "Index" });
        }
    }
}