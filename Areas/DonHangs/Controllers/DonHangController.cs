using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;

namespace QLBANDTDD.Areas.DonHangs.Controllers
{
    public class DonHangController : Controller
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
            var list = (from d in db.DonHang
                        orderby d.NgayDat descending
                        select d).ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Details(int id)
        {
            return View(db.DonHang.Find(id));
        }
        public ActionResult Delivery(int id)
        {
            var dh = db.DonHang.Find(id);
            if (ModelState.IsValid)
            {
                //DonHang dh = new DonHang();
                dh.TinhTrang = true;
               //UpdateSL(masp,soluong,tinhtrang);
                db.Entry<DonHang>(dh).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Details", dh);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                List<ChiTietDonHang> list = new List<ChiTietDonHang>();
                list = db.ChiTietDonHang.Where(m => m.MaDonHang == id).ToList();
                ChiTietDonHang ct = new ChiTietDonHang();
                ct.MaDonHang = id;
                list.Remove(ct);
                DonHang dh = db.DonHang.Find(id);
                db.DonHang.Remove(dh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }

        }
        private void UpdateSL(int? masp, int? sl, bool? tinhtrang)
        {
            if (sl != null)
            {
                var s = db.SanPham.Find(masp);
                if (tinhtrang == true)
                    s.SoLuong-=sl;
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}