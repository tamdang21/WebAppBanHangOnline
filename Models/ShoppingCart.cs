using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBanHangOnlineNhomNBTPQ.Models
{
    
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart(){
            this.Items = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem product, int quantity)
        {
            var check_exit = Items.FirstOrDefault(x => x.Productid == product.Productid);
            if(check_exit != null)
            {
                check_exit.ProductQuantity += quantity;
                check_exit.ProductTotalprice = check_exit.ProductPrice * check_exit.ProductQuantity;
            }
            else
            {
                Items.Add(product );
            }
          }
        public void Remove (int id)
            {
                var check_exit = Items.SingleOrDefault(x => x.Productid == id);
            if (check_exit != null)
            {
                Items.Remove(check_exit);
                
            }
        }
        public void UpdateQuantity(int id,int quantity)
        {
            var check_exit = Items.SingleOrDefault(x => x.Productid == id);
            if (check_exit != null)
            {
                check_exit.ProductQuantity = quantity;
                check_exit.ProductTotalprice = check_exit.ProductQuantity * check_exit.ProductPrice;
            }
        }
        public Decimal getTotalPrice()
        {
            return Items.Sum(x => x.ProductTotalprice);
        }
        public int getTotalQuantity()
        {
            return Items.Sum(x => x.ProductQuantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }
    
    public class ShoppingCartItem
    {
        public int Productid { get; set; }
        public string Productname { get; set; }
        public string ProductImage { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }

        public int ProductQuantity { get; set; }
        public Decimal ProductPrice { get; set; }
        public Decimal ProductTotalprice { get; set; }
    }
}