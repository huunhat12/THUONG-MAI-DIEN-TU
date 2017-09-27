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
    public class SanPhamController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult chitietsanpham(string loaisp,int maloai,string tukhoa)
        {
            SanPhamCT model = new SanPhamCT();
            SanPham sql = db.SanPham.Find(new object[] { maloai });
            // Lấy cookie cũ tên views
            var views = Request.Cookies["views"];
            // Nếu chưa có cookie cũ -> tạo mới
            if (views == null)
            {
                views = new HttpCookie("views");
            }
            // bổ sung mặt hàng đã xem vào cookie
            views.Values[maloai.ToString()] = maloai.ToString();
            // Đặt thời hạn tồn tại của cookie
            views.Expires = DateTime.Now.AddMinutes(1);
            // Gửi cookie về client để lưu lại
            Response.Cookies.Add(views);
            //Lấy List<int> chứa mã hàng đã xem từ cookie
            var keys = views.Values.AllKeys.Select(k => int.Parse(k)).ToList();
            // Truy vấn hàng đã xem 
            model.spdx = db.SanPham.Where(p => keys.Contains(p.MaSP));
            model.sanpham = sql;
            IEnumerable<ImageLinkViewModel> sp = from m in
                                                     (from s in db.SanPham
                                                      where s.LoaiSP == sql.LoaiSP && s.MaSP != sql.MaSP
                                                      orderby s.TenSP descending
                                                      select s).ToList<SanPham>()
                                                 from hsx in db.HangSanXuat
                                                 from loai in db.LoaiSanPham
                                                 where m.HangSX == hsx.Id && m.LoaiSP == loai.MaLoai
                                                 select new ImageLinkViewModel
                                                 {
                                                     MaSP=m.MaSP,
                                                     TenSP = m.TenSP,
                                                     Hinh = m.HinhAnh,
                                                     HangSX = hsx.TenHang,
                                                     GiaGoc = m.GiaGoc,
                                                     GiaTien = m.GiaTien,
                                                     Link = m.GetUrl(),
                                                     IsNew=(Boolean)m.IsNew,
                                                     IsHot=(Boolean)m.IsHot,
                                                     SoLuong=m.SoLuong
                                                 };
            model.sanphamcungloai = sp.ToList<ImageLinkViewModel>();
            model.sanphamnoibat = (from s in (from d in this.db.SanPham
                                              select d).ToList<SanPham>()
                                   from hsx in db.HangSanXuat
                                   from l in db.LoaiSanPham
                                   where s.LoaiSP == l.MaLoai && s.HangSX == hsx.Id
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
            List<LinkModel> list = new List<LinkModel>();
            LinkModel item = new LinkModel
            {
                Title = sql.LoaiSanPham.TenLoai,
                Link = sql.LoaiSanPham.GetUrl(),
                TuKhoa = sql.LoaiSanPham.TuKhoa
            };
            list.Add(item);
            LinkModel item2 = new LinkModel
            {
                Title = sql.HangSanXuat.TenHang,
                Link = sql.HangSanXuat.GetUrl(),
                TuKhoa = sql.HangSanXuat.TuKhoa
            };
            list.Add(item2);
            LinkModel item1 = new LinkModel()
            {
                Title = sql.TenSP,
                Link = sql.GetUrl(),
                TuKhoa = sql.TuKhoa
            };
            list.Add(item1);
            ViewBag.path = list;
            ViewBag.LoaiSP = sql.LoaiSanPham.TuKhoa;
            ViewBag.Title = sql.TenSP;
            ViewBag.HangSX = sql.HangSanXuat.TuKhoa;
            ViewBag.TuKhoa = sql.TuKhoa;
            return View(model);
        }
        public ActionResult thongsokythuat(int masp)
        {
            List<ThongSoKiThuat> list = new List<ThongSoKiThuat>();
            list = db.ThongSoKiThuat.Where(a => a.MaSP == masp).ToList<ThongSoKiThuat>();
            return PartialView("ThongSoKyThuatPartial", list);
        }
        public ActionResult nhomsanpham(string hangsanxuat,int? page)
        {
            List<SanPham> listsp = new List<SanPham>();
            HangSanXuat sql = db.HangSanXuat.SingleOrDefault<HangSanXuat>(d => d.TuKhoa == hangsanxuat);
                listsp = (from s in db.SanPham
                          where s.HangSX == sql.Id
                          orderby s.GiaTien ascending
                          select s).ToList<SanPham>();
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            List<LinkModel> list = new List<LinkModel>();
            LinkModel item = new LinkModel
            {
                Title = sql.TenHang,
                Link = sql.GetUrl(),
                TuKhoa = sql.TuKhoa
            };
            list.Add(item);
            ViewBag.path = list;
            ViewBag.HangSX = sql.TenHang;
            return View(listsp.ToPagedList(pageNumber, pageSize));
        }
    }
}