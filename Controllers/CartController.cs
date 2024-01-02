using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppBanHangOnlineNhomNBTPQ.Models;

namespace WebAppBanHangOnlineNhomNBTPQ.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            
            return View();
        }
        public ActionResult Partial_cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCountitem()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new {  Count = cart.Items.Count },JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
           
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1 ,Count = 0};
            var db = new dbQUANLYBANQUANAOEntities();
            var checkProduct = db.tbSANPHAMs.FirstOrDefault(x => x.MASANPHAM == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem product = new ShoppingCartItem
                {
                    Productid = checkProduct.MASANPHAM,
                    Productname = checkProduct.TENSANPHAM,
                    ProductSize = checkProduct.SIZE,
                    ProductPrice = (decimal)checkProduct.DONGIA,
                    ProductColor = checkProduct.MAU,
                    ProductQuantity = quantity
                };
                var image = db.tbHINHANHs.FirstOrDefault(x => x.MASANPHAM == id);
                if (image != null)
                {
                    product.ProductImage = image.HINHANH;
                   
                }
                product.ProductTotalprice = product.ProductPrice * product.ProductQuantity;
                cart.AddToCart(product, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm sản phẩm thành công", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.Productid == id);
                if(checkProduct != null)
                {
                    cart.Remove(id);
                    ViewBag.total = cart.getTotalPrice();
                    code = new { Success = false, msg = "", code = -1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public ActionResult Update(int id,int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult DeleteSelected(List<int> ids)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                foreach (var id in ids)
                {
                    var item = cart.Items.FirstOrDefault(i => i.Productid == id);
                    if (item != null)
                    {
                        cart.Remove(id);
                    }
                }


                return Json(new { Success = true });
            }
            return Json(new { Success = false});
        }
    }
   


}