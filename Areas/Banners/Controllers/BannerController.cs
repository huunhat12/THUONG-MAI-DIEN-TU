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
    public class BannerController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult Index(int? page)
        {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
                }
            var list = (from b in db.Banner
                        from bg in db.BannerGroup
                        where b.BannerGroupId==bg.BannerGroupId
                        orderby b.ThuTu descending
                        select new BannerModel
                        {
                            BannerId=b.BannerId,
                            HinhAnh=b.HinhAnh,
                            ThuTu=(int)b.ThuTu,
                            TieuDe=b.TieuDe,
                            Link=b.Link,
                            HienThi=(bool)b.HienThi,
                            NhomBanner=bg.TieuDe
                        }).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult ThemBanner()
        {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
                }
            ViewBag.BannerGroupId = new SelectList(db.BannerGroup, "BannerGroupId", "TieuDe");
            return View();
        }
        [HttpPost]
        public ActionResult ThemBanner(BannerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Banner bn = new Banner();
                    bn.TieuDe = model.TieuDe;
                    bn.HinhAnh = model.HinhAnh;
                    bn.Link = model.Link;
                    bn.ThuTu = model.ThuTu;
                    bn.HienThi = model.HienThi;
                    bn.BannerGroupId = model.BannerGroupId;
                    db.Banner.Add(bn);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return HttpNotFound();
            }
            ViewBag.BannerGroupId = new SelectList(db.BannerGroup, "BannerGroupId", "TieuDe", model.BannerGroupId);
            return View(model);
        }
        public ActionResult SuaBanner(int id)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login");
            }
            var list = (from bn in db.Banner
                        where bn.BannerId==id
                        select new BannerModel
                        {
                            BannerId = bn.BannerId,
                            HinhAnh = bn.HinhAnh,
                            ThuTu = (int)bn.ThuTu,
                            TieuDe = bn.TieuDe,
                            Link = bn.Link,
                            HienThi = (bool)bn.HienThi,
                            BannerGroupId = (int)bn.BannerGroupId
                        }).SingleOrDefault();
            if(list==null)
            {
                return HttpNotFound();
            }
            ViewBag.BannerGroupId = new SelectList(db.BannerGroup, "BannerGroupId", "TieuDe", list.BannerGroupId);
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaBanner(BannerModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Banner bn = new Banner();
                    bn.BannerId = model.BannerId;
                    bn.TieuDe = model.TieuDe;
                    bn.HinhAnh = model.HinhAnh;
                    bn.Link = model.Link;
                    bn.ThuTu = model.ThuTu;
                    bn.HienThi = model.HienThi;
                    bn.BannerGroupId = model.BannerGroupId;
                    db.Entry(bn).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return HttpNotFound();
            }
            ViewBag.BannerGroupId = new SelectList(db.BannerGroup, "BannerGroupId", "TieuDe", model.BannerGroupId);
            return View(model);
        }
        //public ActionResult XoaBanner(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Session["UserName"] == null)
        //        {
        //            FormsAuthentication.SignOut();
        //            Session.Clear();
        //            return base.RedirectToAction("Login", "Login");
        //        }
        //    }
        //    Banner bn = db.Banner.Find(id);
        //    if (bn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bn);
        //}

        [HttpPost]
        public ActionResult XoaBanner(int id)
        {
            try
            {
                Banner bn = db.Banner.Find(id);
                db.Banner.Remove(bn);
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