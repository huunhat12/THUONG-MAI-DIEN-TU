using PagedList;
using QLBANDTDD.Areas.SanPhams.Models;
using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QLBANDTDD.Areas.SanPhams.Controllers
{
    public class SanPhamController : Controller
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
            var list = (from s in db.SanPham
                        from h in db.HangSanXuat
                        from l in db.LoaiSanPham
                        where h.Id==s.HangSX && l.MaLoai==s.LoaiSP
                        orderby s.MaSP descending
                        select new SanPhamModel
                        {
                            MaSP=s.MaSP,
                            TenSP=s.TenSP,
                            HangSXuat=h.TenHang,
                            LoaiSPham=l.TenLoai,
                            HinhAnh=s.HinhAnh,
                            HienThi=(bool)s.HienThi,
                            GiaTien=(int)s.GiaTien,
                            IsNew=(bool)s.IsNew,
                            IsHot=(bool)s.IsHot
                        }).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemSanPham()
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login");
            }
            ViewBag.HangSX = new SelectList(db.HangSanXuat, "Id", "TenHang");
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPham, "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSanPham(SanPhamModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SanPham sp = new SanPham();
                    sp.TenSP = model.TenSP;
                    sp.LoaiSP = model.LoaiSP;
                    sp.HangSX = model.HangSX;
                    sp.XuatXu = model.XuatXu;
                    sp.GiaGoc = model.GiaGoc;
                    sp.GiaTien = model.GiaTien;
                    sp.MoTa = model.MoTa;
                    sp.HinhAnh = model.HinhAnh;
                    sp.AnhKhac = model.AnhKhac;
                    sp.SoLuong = model.SoLuong;
                    sp.HienThi = model.HienThi;
                    sp.IsHot = model.IsHot;
                    sp.IsNew = model.IsNew;
                    sp.TuKhoa = model.TuKhoa;
                    db.SanPham.Add(sp);
                    db.SaveChanges();
                    ThongSoKiThuat ts = new ThongSoKiThuat();
                    ts.MaSP = sp.MaSP;
                    List<ThongSoKiThuat> lst = new List<ThongSoKiThuat>();
                    lst.Add(ts);
                    return View("ThemThongSoKT", lst);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            ViewBag.HangSX = new SelectList(db.HangSanXuat, "Id", "TenHang", model.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPham, "MaLoai", "TenLoai", model.LoaiSP);
            return View(model);
        }
        public ActionResult ThemThongSoKT()
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
        public ActionResult ThemThongSoKT(List<ThongSoKiThuat> lstkt)
        {
            try
            {
                if (lstkt.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in lstkt)
                {
                    if (!string.IsNullOrEmpty(item.TenTSKT))
                    {
                        ThongSoKiThuat ts = new ThongSoKiThuat();
                        db.ThongSoKiThuat.Add(item);
                        db.SaveChanges();
                    }
                }
                
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult SuaSanPham(int id)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                return base.RedirectToAction("Login", "Login");
            }
            var list = (from s in db.SanPham
                        where s.MaSP == id
                        select new SanPhamModel
                        {
                            MaSP = s.MaSP,
                            TenSP = s.TenSP,
                            HangSX = (int)s.HangSX,
                            LoaiSP=(int)s.LoaiSP,
                            HinhAnh = s.HinhAnh,
                            GiaGoc=(int)s.GiaGoc,
                            MoTa=s.MoTa,
                            XuatXu=s.XuatXu,
                            AnhKhac=s.AnhKhac,
                            SoLuong=(int)s.SoLuong,
                            HienThi = (bool)s.HienThi,
                            GiaTien = (int)s.GiaTien,
                            IsNew = (bool)s.IsNew,
                            IsHot = (bool)s.IsHot,
                            TuKhoa=s.TuKhoa
                        }).SingleOrDefault();
            if (list==null)
            {
                return HttpNotFound();
            }
            ViewBag.HangSX = new SelectList(db.HangSanXuat, "Id", "TenHang", list.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPham, "MaLoai", "TenLoai", list.LoaiSP);
            return View(list);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSanPham(SanPhamModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    SanPham sp = new SanPham();
                    sp.MaSP = model.MaSP;
                    sp.TenSP = model.TenSP;
                    sp.LoaiSP = model.LoaiSP;
                    sp.HangSX = model.HangSX;
                    sp.GiaGoc = model.GiaGoc;
                    sp.GiaTien = model.GiaTien;
                    sp.HinhAnh = model.HinhAnh;
                    sp.HienThi = model.HienThi;
                    sp.IsHot = model.IsHot;
                    sp.IsNew = model.IsNew;
                    sp.MoTa = model.MoTa;
                    sp.SoLuong = model.SoLuong;
                    sp.XuatXu = model.XuatXu;
                    sp.TuKhoa = model.TuKhoa;
                    sp.AnhKhac = model.AnhKhac;
                    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return HttpNotFound();
            }
            ViewBag.HangSX = new SelectList(db.HangSanXuat, "Id", "TenHang", model.HangSX);
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPham, "MaLoai", "TenLoai", model.LoaiSP);
            return View(model);
        }
        public ActionResult SuaThongSoKT(int id)
        {
            List<ThongSoKiThuat> lstkt = new List<ThongSoKiThuat>();
            lstkt = db.ThongSoKiThuat.Where(m => m.MaSP == id).ToList();
            if(lstkt.Count==0)
            {
                ThongSoKiThuat ts = new ThongSoKiThuat();
                ts.MaSP = id;
                List<ThongSoKiThuat> lst = new List<ThongSoKiThuat>();
                lst.Add(ts);
                return View("ThemThongSoKT", lst);
            }
            return View("SuaThongSoKT",lstkt);
        }
        [HttpPost]
        public ActionResult SuaThongSoKT(List<ThongSoKiThuat> lstkt)
        {
            if(lstkt.Count==0)
            {
                return RedirectToAction("Index");
            }
            var masp = lstkt[0].MaSP;
            db.ThongSoKiThuat.RemoveRange(db.ThongSoKiThuat.Where(m => m.MaSP == masp));
            db.SaveChanges();
            foreach( var item in lstkt)
            {
                if(!string.IsNullOrEmpty(item.TenTSKT))
                {
                    db.ThongSoKiThuat.Add(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        //public ActionResult XoaSanPham(int id)
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
        //    SanPham sp = db.SanPham.Find(id);
        //    if (sp == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sp);
        //}

        [HttpPost]/*, ActionName("XoaSanPham")]*/
        public ActionResult XoaSanPham(int id)
        {
            try
            {
                List<ThongSoKiThuat> lstkt = new List<ThongSoKiThuat>();
                lstkt = db.ThongSoKiThuat.Where(m => m.MaSP == id).ToList();
                ThongSoKiThuat ts = new ThongSoKiThuat();
                ts.MaSP = id;
                lstkt.Remove(ts);
                SanPham sp = db.SanPham.Find(id);
                db.SanPham.Remove(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch 
            {
                return HttpNotFound();
            }
        }
    }
}