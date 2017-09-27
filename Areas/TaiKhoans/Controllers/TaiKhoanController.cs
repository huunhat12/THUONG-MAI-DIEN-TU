using QLBANDTDD.Areas.TaiKhoans.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using System.Data.Entity;

namespace QLBANDTDD.Areas.TaiKhoans.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoans/TaiKhoan
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult Index(int? page)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var list = (from tk in db.TaiKhoan
                        select new TaiKhoanModel
                        {
                            MaTK=tk.MaTK,
                            TenTK=tk.TenTK,
                            Quyen=(bool)tk.Quyen
                        }).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult ThemTaiKhoan()
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemTaiKhoan(TaiKhoanModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.TenTK = model.TenTK;
                    tk.MatKhau = model.MatKhau;
                    tk.Quyen = model.Quyen;
                    db.TaiKhoan.Add(tk);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult XoaTaiKhoan(int id)
        {
            try
            {
                int matk = Int32.Parse( Session["MaTK"].ToString());
                if (ModelState.IsValid)
                {
                    if (id==matk)
                    {
                        ModelState.AddModelError("", "Tài khoản đang đăng nhập không thể xóa");
                    }
                    TaiKhoan tk = db.TaiKhoan.Find(id);
                    db.TaiKhoan.Remove(tk);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

        }
        public ActionResult ThayDoiMK()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThayDoiMK(TaikhoanChanged model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var tk = (from t in db.TaiKhoan
                      where t.MaTK == model.MaTK
                      select new TaikhoanChanged
                      {
                          MaTK = t.MaTK,
                          MatKhau = t.MatKhau
                      }).SingleOrDefault();
            if(tk.MatKhau!=model.MatKhau)
            {
                //("Mật khẩu cũ không đúng");
                return HttpNotFound();
            }
            else
            {
                var taikhoan = db.TaiKhoan.Find(model.MaTK);
                if (ModelState.IsValid)
                {
                    taikhoan.MatKhau = model.MatKhauMoi;
                    db.Entry<TaiKhoan>(taikhoan).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
            }
            return RedirectToAction("Index");
        }

    }
}