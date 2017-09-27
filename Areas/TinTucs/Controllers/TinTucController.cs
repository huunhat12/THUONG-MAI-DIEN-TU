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
    public class TinTucController : Controller
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
            var list = (from t in db.TinTuc
                        from n in db.NhomTinTuc
                        where t.MaNhom == n.Id
                        orderby t.NgayDang descending
                        select new TinTucModel
                        {
                            Id = t.Id,
                            TieuDe = t.TieuDe,
                            HinhAnh = t.HinhAnh,
                            NoiDungTT = t.NoiDungTT,
                            NgayDang = (DateTime)t.NgayDang,
                            TenNhom = n.TieuDe,
                            HienThi = (bool)t.HienThi,
                        }).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ThemTinTuc()
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            ViewBag.MaNhom = new SelectList(db.NhomTinTuc, "Id", "TieuDe");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(TinTucModel model)
        {
            if (ModelState.IsValid)
            {
                TinTuc t = new TinTuc();
                t.TieuDe = model.TieuDe;
                t.NoiDungTT = model.NoiDungTT;
                t.NoiDung = model.NoiDung;
                t.HinhAnh = model.HinhAnh;
                t.NgayDang = model.NgayDang;
                t.HienThi = model.HienThi;
                t.MaNhom = model.MaNhom;
                t.TuKhoa = model.TuKhoa;
                db.TinTuc.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.NhomTinTuc, "Id", "TieuDe", model.MaNhom);
            return View(model);
        }

        public ActionResult SuaTinTuc(int id=0)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login", new { area = "LoginAdmin" });
            }
            var list = (from t in db.TinTuc
                          where t.Id==id
                          orderby t.NgayDang descending
                          select new TinTucModel
                          {
                              Id = t.Id,
                              TieuDe = t.TieuDe,
                              HinhAnh = t.HinhAnh,
                              NoiDungTT = t.NoiDungTT,
                              NoiDung=t.NoiDung,
                              NgayDang = (DateTime)t.NgayDang,
                              MaNhom=(int)t.MaNhom,
                              HienThi = (bool)t.HienThi,
                              TuKhoa=t.TuKhoa
                          }).SingleOrDefault();
            if(list==null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.NhomTinTuc, "Id", "TieuDe", list.MaNhom);
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTinTuc(TinTucModel model )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TinTuc t = new TinTuc();
                    t.Id = model.Id;
                    t.TieuDe = model.TieuDe;
                    t.NoiDungTT = model.NoiDungTT;
                    t.NoiDung = model.NoiDung;
                    t.HinhAnh = model.HinhAnh;
                    t.NgayDang = model.NgayDang;
                    t.HienThi = model.HienThi;
                    t.MaNhom = model.MaNhom;
                    t.TuKhoa = model.TuKhoa;
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.NhomTinTuc, "Id", "TieuDe", model.MaNhom);
            return View(model);
        }

        //public ActionResult XoaTinTuc(int id)
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
        //    TinTuc tt = db.TinTuc.Find(id);
        //    if (tt == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tt);
        //}

        [HttpPost]
        public ActionResult XoaTinTuc(int id)
        {
            try
            {
                TinTuc tt = db.TinTuc.Find(id);
                db.TinTuc.Remove(tt);
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
