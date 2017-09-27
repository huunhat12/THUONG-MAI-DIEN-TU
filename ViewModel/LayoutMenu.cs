using QLBANDTDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANDTDD.ViewModel
{
    public class LayoutMenu
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        private UrlHelper Url;
        public LayoutMenu(UrlHelper url)
        {
            this.Url = url;
        }
        private List<Banner> DanhSachBanner(string tukhoa)
        {
            BannerGroup banner = db.BannerGroup.SingleOrDefault<BannerGroup>(d => d.TuKhoa == tukhoa);
            return (from d in db.Banner
                    where ((d.BannerGroupId == banner.BannerGroupId) && d.HienThi == true)
                    select d).ToList<Banner>();
        }
        public List<ImagesLink> DanhSachSlider()
        {
            return (from d in this.DanhSachBanner("banner")
                    orderby d.ThuTu descending, d.BannerId descending
                    select new ImagesLink { Hinh = d.HinhAnh,Link=d.Link,TieuDe=d.TieuDe }).ToList<ImagesLink>();
        }
        public List<ImagesLink> DanhSachDoiTac()
        {
            return (from d in this.DanhSachBanner("doi-tac")
                    orderby d.ThuTu descending, d.BannerId descending
                    select new ImagesLink { Hinh = d.HinhAnh, Link = d.Link, TieuDe = d.TieuDe }).ToList<ImagesLink>();
        }
        public List<MenuItem> GetMenuList()
        {
            List<MenuItem> mlist = new List<MenuItem>();
            var loaisp = db.LoaiSanPham.OrderBy(a => a.MaLoai).ToList();
            foreach(var item in loaisp)
            {
                MenuItem mnitem = new MenuItem();
                mnitem.loaiSp = item;
                mnitem.hangSX = this.GetHangSX(item.MaLoai);
                mlist.Add(mnitem);
            }
            return mlist;
        }
        private List<HangSanXuat> GetHangSX(int maloai)
        {
            List<HangSanXuat> hsxlist = (from p in db.SanPham where p.LoaiSP == maloai select p.HangSanXuat).Distinct().ToList();
            return hsxlist;
        }
        public List<HangSX> DanhSachHangSX()
        {
            var hsx = new List<HangSX>();
            hsx = (from h in db.HangSanXuat
                   select new HangSX
                   {
                       Id=h.Id,
                       TenHang = h.TenHang
                   }).Distinct().ToList();
            return hsx;
        }
        public List<LoaiSP> DanhSachLoaiSP()
        {
            var loai = new List<LoaiSP>();
            loai = (from l in db.LoaiSanPham
                   select new LoaiSP
                   {
                       Id = l.MaLoai,
                       Tenloai = l.TenLoai
                   }).Distinct().ToList();
            return loai;
        }
        public List<NhomTinTuc> GetMenuTinTuc()
        {
            List<NhomTinTuc> list = new List<NhomTinTuc>();
            list = (from tt in db.NhomTinTuc
                    where tt.HienThi==true
                    select tt).ToList();
            return list;
        }
        public List<ImageLinkViewModel> Sanphamnoibat()
        {
            List<ImageLinkViewModel> list = new List<ImageLinkViewModel>();
            list = (from s in (from d in this.db.SanPham
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
            return list;
        }
        public List<TinTucModel> Tintucmoinhats()
        {
            List<TinTucModel> list = new List<TinTucModel>();
            list = (from t in (from s in db.TinTuc
                               select s).ToList<TinTuc>()
                    from l in db.NhomTinTuc
                    where t.MaNhom == l.Id
                    select new TinTucModel
                    {
                        Hinh = t.HinhAnh,
                        Link = t.GetUrl(),
                        NgayDang = (DateTime)t.NgayDang,
                        NoiDungTT = t.NoiDungTT,
                        TenNhomTT = l.TieuDe,
                        TieuDe = t.TieuDe
                    }).ToList<TinTucModel>().Take(5).ToList();
            return list;
        }
    }
}