using QLBANDTDD.Areas.Banners.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace QLBANDTDD.Areas.Banners.Controllers
{
    public class BannerGroupController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        // GET: Banners/BannerGroup
        public ActionResult Index(int? page)
        {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
            }
            var list = (from bg in db.BannerGroup
                        orderby bg.BannerGroupId descending
                        select new BannerGroupModel
                        {
                            BannerGroupId=bg.BannerGroupId,
                            TieuDe=bg.TieuDe,
                            TuKhoa=bg.TuKhoa,
                            HienThi=(bool)bg.HienThi
                        }).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult ThemNhomBanner()
        {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
                }
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhomBanner(BannerGroupModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    BannerGroup bg = new BannerGroup();
                    bg.TieuDe = model.TieuDe;
                    bg.TuKhoa = model.TuKhoa;
                    bg.HienThi = model.HienThi;
                    db.BannerGroup.Add(bg);
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
        public ActionResult SuaNhomBanner(int id=0)
        {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
                }
            var list = (from bg in db.BannerGroup
                        where bg.BannerGroupId == id
                        select new BannerGroupModel
                        {
                            BannerGroupId = bg.BannerGroupId,
                            TieuDe = bg.TieuDe,
                            HienThi = (bool)bg.HienThi,
                            TuKhoa = bg.TuKhoa
                        }).SingleOrDefault();
            if(list==null)
            {
                return HttpNotFound();
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaNhomBanner(BannerGroupModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    BannerGroup bg = new BannerGroup();
                    bg.BannerGroupId = model.BannerGroupId;
                    bg.TieuDe = model.TieuDe;
                    bg.TuKhoa = model.TuKhoa;
                    bg.HienThi = model.HienThi;
                    db.Entry(bg).State = EntityState.Modified;
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
        //public ActionResult XoaNhomBanner(int id)
        //{
        //        if (Session["UserName"] == null)
        //        {
        //            FormsAuthentication.SignOut();
        //            Session.Clear();
        //            return base.RedirectToAction("Login", "Login");
        //        }
        //    BannerGroup bg = db.BannerGroup.Find(id);
        //    if(bg==null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bg);
        //}
        [HttpPost]
        public ActionResult XoaNhomBanner(int id)
        {
            try
            {
                List<Banner> list = new List<Banner>();
                list = db.Banner.Where(m => m.BannerGroupId == id).ToList();
                Banner bn = new Banner();
                bn.BannerGroupId = id;
                list.Remove(bn);
                BannerGroup bg = db.BannerGroup.Find(id);
                db.BannerGroup.Remove(bg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}