using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.Areas.Admin.Controllers
{
   public class AdminDashboardController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
