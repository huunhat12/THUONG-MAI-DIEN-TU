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
    public class TinTucController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult chitiettintuc(string tin,string loaitin,int id,string tieude)
        {
            TinTucCT model = new TinTucCT();
            TinTuc sql = db.TinTuc.Find(new object[] { id });
            model.tintuc = sql;
            IEnumerable<TinTucModel> list= from m in
                                                 (from d in db.TinTuc
                                                  where d.MaNhom == sql.MaNhom && d.Id != sql.Id
                                                  orderby d.NgayDang descending
                                                  select d).ToList<TinTuc>()
                                           from lt in db.NhomTinTuc
                                           where m.MaNhom==lt.Id
                                           select new TinTucModel
                                           {
                                               TieuDe = m.TieuDe,
                                               NgayDang = (DateTime)m.NgayDang,
                                               Link = m.GetUrl(),
                                               NoiDungTT = m.NoiDungTT,
                                               Hinh = m.HinhAnh,
                                               TenNhomTT=lt.TieuDe
                                           };
            model.tinCungLoai = list.Take(5).ToList<TinTucModel>();
            List<LinkModel> list1 = new List<LinkModel>();
            LinkModel item = new LinkModel
            {
                Title = sql.NhomTinTuc.TieuDe,
                Link = sql.NhomTinTuc.GetUrl(),
                TuKhoa = sql.NhomTinTuc.TuKhoa
            };
            list1.Add(item);
            LinkModel item1 = new LinkModel
            {
                Title = sql.TieuDe,
                Link = sql.GetUrl(),
                TuKhoa = sql.TuKhoa
            };
            list1.Add(item1);
            ViewBag.path = list1;
            ViewBag.TieuDe = sql.NhomTinTuc.TuKhoa;
            ViewBag.TinTuc = sql.TuKhoa;
            return View(model);

        }
        public ActionResult nhomtintuc(string loaitin,int? page)
        {
            List<TinTuc> list = new List<TinTuc>();
            NhomTinTuc sql = this.db.NhomTinTuc.SingleOrDefault<NhomTinTuc>(d => d.TuKhoa == loaitin);
            ViewBag.menuId = loaitin;
            list = (from d in this.db.TinTuc
                    where d.MaNhom == sql.Id
                    orderby d.NgayDang descending
                    select d).ToList<TinTuc>();
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            List<LinkModel> list2 = new List<LinkModel>();
            LinkModel item = new LinkModel
            {
                Title = sql.TieuDe,
                Link = sql.GetUrl(),
                TuKhoa = sql.TuKhoa
            };
            list2.Add(item);
            ViewBag.path = list2;
            ViewBag.TieuDe = sql.TuKhoa;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}