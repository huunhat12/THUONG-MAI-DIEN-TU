using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace QLBANDTDD.Areas.Home.Controllers
{
    public class HoiDapController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        // GET: HoiDap/HoiDap
        public ActionResult Index(int? page)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var list = db.CauHoi.OrderByDescending(d => d.NgayHoi).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Edit(int id=0)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            CauHoi ch = db.CauHoi.Find(id);
            if(ch==null)
            {
                return HttpNotFound();
            }
            return View(ch);
        }
        [HttpPost]
        public ActionResult Edit(CauHoi cauhoi)
        {
            if(ModelState.IsValid)
            {
                db.Entry(cauhoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cauhoi);
        }
        //public ActionResult Delete(int id=0)
        //{
        //    if (Session["UserName"] == null)
        //    {
        //        FormsAuthentication.SignOut();
        //        Session.Clear();
        //        return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
        //    }
        //    CauHoi ch = db.CauHoi.Find(id);
        //    if(ch==null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ch);
        //}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CauHoi cauhoi = db.CauHoi.Find(id);
            var tl = cauhoi.TraLoi;
            if(tl.Count()>0)
            {
                db.TraLoi.Remove(tl.FirstOrDefault());
                db.SaveChanges();
            }
            db.CauHoi.Remove(cauhoi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult TraLoi(int id=0)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var cauhoi = db.CauHoi.Find(id);
            ViewBag.CauHoi = cauhoi;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TraLoi(int id,TraLoi traloi)
        {
            if(ModelState.IsValid)
            {
                traloi.MaCauHoi = id;
                db.TraLoi.Add(traloi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traloi);
        }
        public ActionResult CapNhatTraLoi(int id=0)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var cauhoi = db.CauHoi.Find(id);
            ViewBag.CauHoi = cauhoi;
            TraLoi tl = db.TraLoi.Where(d => d.MaCauHoi == id).SingleOrDefault();
            if(tl==null)
            {
                return HttpNotFound();
            }
            return View(tl);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CapNhatTraLoi(int id,TraLoi traloi)
        {
            if(ModelState.IsValid)
            {
                traloi.MaCauHoi = id;
                db.Entry(traloi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(traloi);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}