using QLBANDTDD.Areas.LoaiSanPhams.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace QLBANDTDD.Areas.LoaiSanPhams.Controllers
{
    public class LoaiSanPhamController : Controller
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
            var list = (from l in db.LoaiSanPham
                        orderby l.MaLoai descending
                        select new LoaiSPModel
                        {
                            MaLoai = l.MaLoai,
                            TenLoai = l.TenLoai,
                            TuKhoa = l.TuKhoa,
                            HienThi = (bool)l.HienThi
                        }).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemLoaiSP()
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
        public ActionResult ThemLoaiSP(LoaiSPModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoaiSanPham lsp = new LoaiSanPham();
                    lsp.TenLoai = model.TenLoai;
                    lsp.TuKhoa = model.TuKhoa;
                    lsp.HienThi = model.HienThi;
                    db.LoaiSanPham.Add(lsp);
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
        public ActionResult SuaLoaiSP(int id = 0)
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
            var list = (from l in db.LoaiSanPham
                        where l.MaLoai == id
                        orderby l.MaLoai descending
                        select new LoaiSPModel
                        {
                            MaLoai = l.MaLoai,
                            TenLoai = l.TenLoai,
                            TuKhoa = l.TuKhoa,
                            HienThi = (bool)l.HienThi
                        }).SingleOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaLoaiSP(LoaiSPModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoaiSanPham lsp = new LoaiSanPham();
                    lsp.MaLoai = model.MaLoai;
                    lsp.TenLoai = model.TenLoai;
                    lsp.TuKhoa = model.TuKhoa;
                    lsp.HienThi = model.HienThi;
                    db.Entry(lsp).State = EntityState.Modified;
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
        public ActionResult XoaLoaiSP(int id)
        {
            try
            {
                List<SanPham> list = new List<SanPham>();
                list = db.SanPham.Where(m => m.LoaiSP == id).ToList();
                SanPham sp = new SanPham();
                sp.LoaiSP = id;
                list.Remove(sp);
                LoaiSanPham lsp = db.LoaiSanPham.Find(id);
                db.LoaiSanPham.Remove(lsp);
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