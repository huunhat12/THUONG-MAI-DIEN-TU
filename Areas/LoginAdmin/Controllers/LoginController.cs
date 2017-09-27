using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANDTDD.Areas.LoginAdmin.Controllers
{
    public class LoginController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        // GET: LoginAdmin/Login
        public ActionResult Login()
        {
            if (ModelState.IsValid)
            {
                if (Session["UserName"] != null)
                {
                    Session.Clear();
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.UserItem user)
        {

            if (ModelState.IsValid)
            {
                var v = db.TaiKhoan.Where(a => a.TenTK.Equals(user.UserName) && a.MatKhau.Equals(user.Password)).FirstOrDefault();
                if (v != null)
                {
                    Session["UserName"] = v.TenTK.ToString();
                    Session["Quyen"] = v.Quyen.ToString();
                    Session["MaTK"] = v.MaTK.ToString();
                    return base.RedirectToAction("Index", "Home",new { area = "HomeAdmin" });
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hay mật khẩu không đúng");
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return base.RedirectToAction("Login", "Login");
        }
    }
}