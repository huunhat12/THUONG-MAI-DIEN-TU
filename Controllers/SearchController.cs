using QLBANDTDD.Models;
using QLBANDTDD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace QLBANDTDD.Controllers
{
    public class SearchController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        // GET: Search
        public ActionResult SearchForm()
        {
            return PartialView("_SearchFormPartial");
        }
        [HttpPost]
        public ActionResult SearchByName(string term)
        {
            List<ImageLinkViewModel> list = new List<ImageLinkViewModel>();
            list = (from s in (from d in this.db.SanPham
                               select d).ToList<SanPham>()
                    from hsx in db.HangSanXuat
                    from l in db.LoaiSanPham
                    where s.LoaiSP == l.MaLoai && s.HangSX == hsx.Id && s.TenSP.Contains(term)
                    select new ImageLinkViewModel
                    {
                        TenSP = s.TenSP,
                        Hinh = s.HinhAnh,
                        HangSX = hsx.TenHang,
                        GiaGoc = s.GiaGoc,
                        GiaTien = s.GiaTien,
                        Link = s.GetUrl(),
                        IsNew = (Boolean)s.IsNew,
                        IsHot = (Boolean)s.IsHot,
                        SoLuong = s.SoLuong
                    }).ToList<ImageLinkViewModel>().Take(5).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchView(string name,int? loai,int? hangsx,int? page,int? minprice,int? maxprice)
        {
            var list = (from s in db.SanPham
                        orderby s.TenSP descending
                        select s);
            if(!string.IsNullOrEmpty(name))
            {
                list = list.Where(d => d.TenSP.Contains(name)).OrderBy(d=>d.TenSP);
            }
            if(loai!=null)
            {
                list = list.Where(d => d.LoaiSP==loai).OrderBy(d => d.TenSP);
            }
            if(hangsx!=null)
            {
                list = list.Where(d => d.HangSX==hangsx).OrderBy(d => d.TenSP);
            }
            if(minprice!=null)
            {
                list = list.Where(d => d.GiaTien >= minprice).OrderBy(d => d.TenSP);
            }
            if(maxprice!=null)
            {
                list = list.Where(d => d.GiaTien <= maxprice).OrderBy(d => d.TenSP);
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            ViewBag.Ten = name;
            ViewBag.Loai = loai;
            ViewBag.Hang = hangsx;
            ViewBag.Min = minprice;
            ViewBag.Max = maxprice;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}