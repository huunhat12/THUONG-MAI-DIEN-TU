using QLBANDTDD.Models;
using QLBANDTDD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANDTDD.Controllers
{
    public class HomeController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        
        public ActionResult Index()
        {
            //int skipnum = (skip ?? 0);
            ViewModel.IndexViewModel model = new ViewModel.IndexViewModel();
            model.sanPhamMoiNhats= (from m in
                                         (from p in this.db.SanPham
                                          select p).ToList<SanPham>()
                                    from s in db.HangSanXuat
                                    where m.HangSX == s.Id && m.IsNew == true
                                    orderby m.MaSP descending
                                    select new ImageLinkViewModel
                                    {
                                        MaSP=m.MaSP,
                                        Hinh=m.HinhAnh,
                                        TenSP=m.TenSP,
                                        HangSX=s.TenHang,
                                        GiaGoc=m.GiaGoc,
                                        GiaTien=m.GiaTien,
                                        Link = m.GetUrl(),
                                        IsNew=(Boolean)m.IsNew,
                                        IsHot=(Boolean)m.IsHot,
                                        Phantram =((double)m.GiaTien/m.GiaGoc)*100
                                    }).ToList<ImageLinkViewModel>().Skip(0).Take(8).ToList();
            model.sanPhamHots= (from m in
                                          (from p in this.db.SanPham
                                           select p).ToList<SanPham>()
                                from s in db.HangSanXuat
                                where m.HangSX == s.Id && m.IsHot == true
                                orderby m.MaSP descending
                                select new ImageLinkViewModel
                                {
                                    MaSP = m.MaSP,
                                    Hinh = m.HinhAnh,
                                    TenSP = m.TenSP,
                                    HangSX = s.TenHang,
                                    GiaGoc = m.GiaGoc,
                                    GiaTien = m.GiaTien,
                                    Link=m.GetUrl(),
                                    IsNew = (Boolean)m.IsNew,
                                    IsHot = (Boolean)m.IsHot,
                                    Phantram = ((double)m.GiaTien / m.GiaGoc) * 100
                                }).ToList<ImageLinkViewModel>().Skip(0).Take(8).ToList();
            model.sanPhamGiamGias = (from m in
                                          (from p in this.db.SanPham
                                           select p).ToList<SanPham>()
                                 from s in db.HangSanXuat
                                 where m.HangSX == s.Id && m.GiaGoc>m.GiaTien
                                     orderby m.MaSP descending
                                     select new ImageLinkViewModel
                                 {
                                     MaSP = m.MaSP,
                                     Hinh = m.HinhAnh,
                                     TenSP = m.TenSP,
                                     HangSX = s.TenHang,
                                     GiaGoc = m.GiaGoc,
                                     GiaTien = m.GiaTien,
                                     Link = m.GetUrl(),
                                     IsNew = (Boolean)m.IsNew,
                                     IsHot = (Boolean)m.IsHot,
                                     Phantram = ((double)m.GiaTien / m.GiaGoc) * 100
                                 }).ToList<ImageLinkViewModel>().Skip(0).Take(8).ToList();
            model.sanPhamBanChays= (
                                    from d in db.ChiTietDonHang
                                    from m in db.SanPham
                                     where d.MaSanPham == m.MaSP
                                    orderby d.SoLuong descending
                                    select new ImageLinkViewModel
                                    {
                                        MaSP = m.MaSP,
                                        Hinh = m.HinhAnh,
                                        TenSP = m.TenSP,
                                        HangSX = m.HangSanXuat.TenHang,
                                        GiaGoc = m.GiaGoc,
                                        GiaTien = m.GiaTien,
                                        Link = "san-pham"+"/"+m.LoaiSanPham.TuKhoa+"/"+m.MaSP+"/"+m.TuKhoa,
                                        IsNew = (Boolean)m.IsNew,
                                        IsHot = (Boolean)m.IsHot,
                                        Phantram = ((double)m.GiaTien / m.GiaGoc) * 100,
                                    }).Distinct().ToList<ImageLinkViewModel>().Take(8).ToList();
            return View(model);
        }
      }
}