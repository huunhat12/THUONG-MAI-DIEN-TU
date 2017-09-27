using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLBANDTDD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute("chi-tiet-san-pham", "{sanpham}/{loaisp}/{maloai}/{tukhoa}", new { controller = "SanPham", action = "chitietsanpham" }, new { sanpham = "san-pham" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute("thong-so-ky-thuat", "{masp}", new { controller = "SanPham", action = "thongsokythuat" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute("nhom-san-pham", "{sanpham}/{hangsanxuat}", new { controller = "SanPham", action = "nhomsanpham" }, new { sanpham = "san-pham" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute(
               name: "Tim Kiem",
               url: "tim-kiem/{name}/{loai}/{hangsx}/{minprice}/{maxprice}",
               defaults: new { controller = "Search", action = "SearchView", id = UrlParameter.Optional }
           );
            routes.MapRoute("nhom-tin-tuc", "{tintuc}/{loaitin}", new { controller = "TinTuc", action = "nhomtintuc" }, new { tintuc = "tin-tuc" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute("chi-tiet-tin-tuc", "{tin}/{loaitin}/{id}/{tieude}", new { controller = "TinTuc", action = "chitiettintuc" }, new { tin = "tin-tuc" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute("hoi-dap", "{controller}/{action}/{id}", new { controller = "HoiDap", action = "Index" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
                namespaces: new[] { "QLBANDTDD.Controllers" }
           );
            routes.MapRoute("Gio hang", "{giohang}", new { controller = "Cart", action = "Giohang" }, new { giohang = "gio-hang" }, new string[] { "QLBANDTDD.Controllers" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new []{ "QLBANDTDD.Controllers" }
            );
        }
    }
}
