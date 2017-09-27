using QLBANDTDD.Models;
using QLBANDTDD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANDTDD.Controllers
{
    public class HoiDapController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult CauHoi()
        {
            List<HoiDapViewModel> model = new List<HoiDapViewModel>();
            var sql = db.CauHoi.Where(d => d.HienThi == true).ToList().Select(d => new HoiDapViewModel() { NgayHoi = (DateTime)d.NgayHoi, TenNguoiHoi = d.TenNguoiHoi, TieuDeCH = d.TieuDe, NoiDungCH = d.NoiDung, NoiDungTL = d.TraLoi.Count() > 0 ? d.TraLoi.SingleOrDefault().NoiDungTL : "" });
            model = sql.ToList();
            ViewBag.TieuDe = "Hỏi Đáp";
            return View(model);
        }
        public ActionResult Soancauhoi()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Soancauhoi(CauHoi model)
        {
            if(ModelState.IsValid)
            {
                model.HienThi = true;
                model.NgayHoi = DateTime.Now;
                db.CauHoi.Add(model);
                db.SaveChanges();
                return View("Success");
            }
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}