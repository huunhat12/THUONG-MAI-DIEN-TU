using QLBANDTDD.Areas.TinTucs.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace QLBANDTDD.Areas.TinTucs.Controllers
{
    public class NhomTinTucController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult Index(int? page)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var list = (from n in db.NhomTinTuc
                        orderby n.Id descending
                        select new NhomTinModel
                        {
                            Id = n.Id,
                            TieuDe=n.TieuDe,
                            TuKhoa=n.TuKhoa,
                            HienThi=(bool)n.HienThi
                        }).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemNhomTinTuc()
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
        [ValidateInput(false)]
        public ActionResult ThemNhomTinTuc(NhomTinModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    NhomTinTuc nt = new NhomTinTuc();
                    nt.TieuDe = model.TieuDe;
                    nt.TuKhoa = model.TuKhoa;
                    nt.HienThi = model.HienThi;
                    db.NhomTinTuc.Add(nt);
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
        public ActionResult SuaNhomTinTuc(int id=0)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserName"] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    return base.RedirectToAction("Login", "Login");
                }
            }
            var list = (from n in db.NhomTinTuc
                        where n.Id == id
                        orderby n.TieuDe descending
                        select new NhomTinModel
                        {
                            Id = n.Id,
                            TieuDe = n.TieuDe,
                            TuKhoa = n.TuKhoa,
                            HienThi = (bool)n.HienThi
                        }).SingleOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaNhomTinTuc(NhomTinModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    NhomTinTuc nt = new NhomTinTuc();
                    nt.Id = model.Id;
                    nt.TieuDe = model.TieuDe;
                    nt.TuKhoa = model.TuKhoa;
                    nt.HienThi = model.HienThi;
                    db.Entry(nt).State = EntityState.Modified;
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
        //public ActionResult XoaNhomTinTuc(int id=0)
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
        //    NhomTinTuc nt = db.NhomTinTuc.Find(id);
        //    if (nt == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(nt);
        //}
        [HttpPost]
        public ActionResult XoaNhomTinTuc(int id)
        {
            try
            {
                List<TinTuc> list = new List<TinTuc>();
                list = db.TinTuc.Where(m => m.MaNhom == id).ToList();
                TinTuc bn = new TinTuc();
                bn.MaNhom = id;
                list.Remove(bn);
                NhomTinTuc nt = db.NhomTinTuc.Find(id);
                db.NhomTinTuc.Remove(nt);
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