using EcommerceSystem.Helper;
using EcommerceSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace EcommerceSystem.Areas.Customer.Controllers
{
   public class ShoppingCartController : Controller
   {
      private readonly DbModels _context;
      public ShoppingCartController(DbModels context)
      {
         this._context = context;
      }
      public IActionResult Index()
      {
         return View();
      }
      public IActionResult AddToCart(int productId)
      {
         List<ShopingCart> cartItems = new List<ShopingCart>();
         var cart = HttpContext.Session.GetObjInSession<ShopingCart>("cart");
         if(cart != null)
         {
            cartItems = cart as List<ShopingCart>;
         }
         if(cartItems.Count > 0)
         {
            var citem = cartItems.Where(p => p.ProductId == productId).SingleOrDefault();
            if(citem != null)
            {
               citem.Quantity = citem.Quantity + 1;
               HttpContext.Session.SetObjInSession("cart", cartItems);
            }
         }
         return View();
      }
   }
}
