using QLBANDTDD.Areas.HangSanXuats.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace QLBANDTDD.Areas.HangSanXuats.Controllers
{
    public class HangSanXuatController : Controller
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
            var list = (from h in db.HangSanXuat
                        orderby h.Id descending
                        select new HangSXModel
                        {
                            Id = h.Id,
                            TenHang = h.TenHang,
                            TruSoChinh = h.TruSoChinh,
                            QuocGia=h.QuocGia,
                            TuKhoa=h.TuKhoa,
                            HienThi = (bool)h.HienThi
                        }).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemHangSX()
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
        public ActionResult ThemHangSX(HangSXModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HangSanXuat hsx = new HangSanXuat();
                    hsx.TenHang = model.TenHang;
                    hsx.TruSoChinh = model.TruSoChinh;
                    hsx.QuocGia = model.QuocGia;
                    hsx.TuKhoa = model.TuKhoa;
                    hsx.HienThi = model.HienThi;
                    db.HangSanXuat.Add(hsx);
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
        public ActionResult SuaHangSX(int id = 0)
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
            var list = (from h in db.HangSanXuat
                        where h.Id == id
                        orderby h.Id descending
                        select new HangSXModel
                        {
                            Id = h.Id,
                            TenHang = h.TenHang,
                            TruSoChinh=h.TruSoChinh,
                            QuocGia=h.QuocGia,
                            TuKhoa = h.TuKhoa,
                            HienThi = (bool)h.HienThi
                        }).SingleOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult SuaHangSX(HangSXModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HangSanXuat hsx = new HangSanXuat();
                    hsx.Id = model.Id;
                    hsx.TenHang = model.TenHang;
                    hsx.TruSoChinh = model.TruSoChinh;
                    hsx.QuocGia = model.QuocGia;
                    hsx.TuKhoa = model.TuKhoa;
                    hsx.HienThi = model.HienThi;
                    db.Entry(hsx).State = EntityState.Modified;
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
        public ActionResult XoaHangSX(int id)
        {
            try
            {
                List<SanPham> list = new List<SanPham>();
                list = db.SanPham.Where(m => m.HangSX == id).ToList();
                SanPham sp = new SanPham();
                sp.HangSX = id;
                list.Remove(sp);
                HangSanXuat hsx = db.HangSanXuat.Find(id);
                db.HangSanXuat.Remove(hsx);
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