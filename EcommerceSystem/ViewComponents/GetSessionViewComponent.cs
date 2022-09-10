using EcommerceSystem.Helper;
using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.ViewComponents
{
   public class GetSessionViewComponent : ViewComponent
   {
      private DbModels _context;
      public GetSessionViewComponent(DbModels context)
      {
         this._context = context;
      }
      public async Task<IViewComponentResult> InvokeAsync()
      {
         List<ShopingCart> data = null;
         if(HttpContext.Session.GetObjInSession<ShopingCart>("cart") != null)
         {
            data = HttpContext.Session.GetObjInSession<ShopingCart>("cart");
         }
         else
         {
            data = new List<ShopingCart>();
         }
         return View(data);
      }
   }
}
