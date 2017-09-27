using System.Web.Mvc;

namespace QLBANDTDD.Areas.LoginAdmin
{
    public class LoginAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LoginAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("Admin_admin", "admin", new { controller = "Login", action = "Login"});
        }
    }
}