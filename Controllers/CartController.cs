using QLBANDTDD.Models;
using QLBANDTDD.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QLBANDTDD.Controllers
{
    public class CartController : Controller
    {
        private QLBANDTDDData db = new QLBANDTDDData();
        public ActionResult Giohang()
        {
            var cart = Session["CartSession"];
            var list = new List<CartItem>();
            double countTotal = 0;
            int countQuantity = 0;
            if (cart!=null)
            {
                list = (List<CartItem>)cart;
            }
            foreach (var total in list)
            {
                countTotal += (double)(total.Quantity*total.sanpham.GiaTien);
            }
            foreach (var sl in list)
            {
                countQuantity += sl.Quantity;
            }
            Session["Total"] = countTotal;
            Session["Quantity"] = countQuantity;
            ViewBag.TieuDe = "Giỏ hàng";
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session["CartSession"] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session["CartSession"];
            sessionCart.RemoveAll(x => x.sanpham.MaSP == id);
            Session["CartSession"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session["CartSession"];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x=>x.sanpham.MaSP==item.sanpham.MaSP);
                if(jsonItem!=null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session["CartSession"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddCart(int maSp,int soluong)
        {
            var sp = db.SanPham.Find(maSp);
            var cart = Session["CartSession"];
            double countTotal = 0;
            int countQuantity = 0;
            if (cart!=null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=>x.sanpham.MaSP==maSp))
                {
                    foreach(var item in list)
                    {
                        if(item.sanpham.MaSP==maSp)
                        {
                            item.Quantity += soluong;
                            
                        }
                        item.Totalprice = (double)(item.Quantity * item.sanpham.GiaTien);
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.sanpham = sp;
                    item.Quantity = soluong;
                    item.Totalprice = (double)(sp.GiaTien * soluong);
                    list.Add(item);
                }
                Session["CartSession"] = list;
                foreach (var total in list)
                {
                    countTotal += (double)(total.Quantity * total.sanpham.GiaTien);
                }
                foreach (var sl in list)
                {
                    countQuantity += sl.Quantity;
                }
            }
            else
            {
                var item = new CartItem();
                item.sanpham = sp;
                item.Quantity = soluong;
                item.Totalprice = (double)(sp.GiaTien * soluong);
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session["CartSession"] = list;
                foreach (var total in list)
                {
                    countTotal += total.Totalprice;
                }
                foreach (var sl in list)
                {
                    countQuantity += sl.Quantity;
                }
            }
            Session["Total"] = countTotal;
            Session["Quantity"] = countQuantity;
            return PartialView("_AddCart");
        }
        public ActionResult CartList()
        {
            return PartialView("_AddCart");
        }
        [HttpGet]
        public ActionResult Thanhtoan()
        {
            ViewBag.TieuDe = "Thanh toán";
            return View();
        }
        [HttpPost]
        public ActionResult Thanhtoan(DonHangModel model)
        {
            var CartList = (List<CartItem>)Session["CartSession"];
            var OrdetailsList = new List<ChiTietDonHang>();
            CartList.ForEach(m =>
            {
                var sp = db.SanPham.Find(m.sanpham.MaSP);
                OrdetailsList.Add(new ChiTietDonHang
                {
                    MaSanPham=sp.MaSP,
                    SoLuong = m.Quantity
                });
            });
            DonHang dh = new DonHang();
            dh.TenKH = model.TenKH;
            dh.SDT = model.SDT;
            dh.Email = model.Email;
            dh.DiaChi = model.DiaChi;
            dh.TinhTrang = false;
            dh.NgayDat = DateTime.Now;
            // thêm danh sách ChiTietDonHang
            dh.ChiTietDonHang = OrdetailsList;

            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/sendmail/MailBody.html"));

            content = content.Replace("{{CustomerName}}", model.TenKH);
            content = content.Replace("{{Phone}}", model.SDT);
            content = content.Replace("{{Email}}", model.Email);
            content = content.Replace("{{Address}}", model.DiaChi);
            //foreach (var item in CartList)
            //{
            //    content = content.Replace("{{NameSP}}", item.sanpham.TenSP);
            //    content = content.Replace("{{Quantity}}", item.Quantity.ToString());
            //    content = content.Replace("{{Price}}", item.sanpham.GiaTien.ToString());
            //    content = content.Replace("{{Money}}", (item.sanpham.GiaTien * item.Quantity).ToString());
            //}
            content = content.Replace("{{Quantity}}", Session["Quantity"].ToString());
            content = content.Replace("{{Total}}",string.Format("{0:0,0}", Session["Total"].ToString()));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(model.Email, "Đơn hàng mới từ MediaCenter", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ MediaCenter", content);

            // Thêm bảng DonHang
            db.DonHang.Add(dh);
            db.SaveChanges();
            Session["CartSession"] = null;

            
            return RedirectToAction("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}