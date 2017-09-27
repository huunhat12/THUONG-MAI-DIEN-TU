using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLBANDTDD.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeAdmin/Home
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            return View();
        }
    }
}