using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAL;

namespace OnLineShop.Controllers
{
    public class CartController : Controller
    {
       
        // GET: Cart
        public PartialViewResult Index()
        {
            
            var cartItems = (List<CartItem>)Session[Common.CommonConstaint.CartSession];
            if (cartItems == null)
            {
                cartItems=new List<CartItem>();
                //return PartialView("_Cart",cartItems);
            }
            return PartialView("_Cart",cartItems);

        }
        public JsonResult AddToCart(CartItem cartItem)
        {
            decimal? totalPrice = 0;
            List<CartItem> cartItems = new List<CartItem>();

            var cartSession = Session[Common.CommonConstaint.CartSession];
            Model.DAL.Product product = new Model.DAL.Product();
            Model.EF.Product p = product.GetProductById(cartItem.ID.ToString());
            if (cartSession == null)
            {
                cartItem.TotalPrice = cartItem.Quantity * p.Price;
                cartItems.Add(cartItem);
                totalPrice = cartItems.Sum(c => c.TotalPrice);
                Session.Add(Common.CommonConstaint.CartSession, cartItems);
            }
            else
            {
                cartItems = (List<CartItem>)Session[Common.CommonConstaint.CartSession];
                if (cartItems.Exists(c => c.ID == cartItem.ID))
                {
                    foreach (CartItem item in cartItems)
                    {
                        if (item.ID == cartItem.ID)
                        {
                            item.Quantity += cartItem.Quantity;
                            item.TotalPrice = item.Quantity * p.Price;
                        }
                    }
                    //Session.Add(Common.CommonConstaint.CartSession, cartItems);

                }
                else
                {
                    cartItem.TotalPrice = cartItem.Quantity * p.Price;
                    cartItems.Add(cartItem);
                    Session.Add(Common.CommonConstaint.CartSession, cartItems);
                }

                totalPrice = cartItems.Sum(c => c.TotalPrice);

            }

            return new JsonResult
            {
                Data = totalPrice,
                ContentType = "application/json;charset=utf-8"
            };
        }
    }
}