using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EcommerceSystem.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private readonly DbModels _context;

      public HomeController(ILogger<HomeController> logger,DbModels context)
      {
         _logger = logger;
         _context = context;
      }
      public IActionResult Index(int? catid)
      {
         List<Product> allProduct = new List<Product>();
         allProduct = _context.Products.Include("Category").OrderBy(p => p.Name).ToList();
         if(catid != null)
         {
            allProduct = _context.Products.Include("Category").OrderBy(p=>p.Name).Where(p=>p.CatID == catid).ToList();
         }
         return View(allProduct);
      }
      [HttpGet]
      public IActionResult ProductDetails(int? id)
      {
         Product product = new Product();
         if (id != null)
         {
            product = _context.Products.Include(p => p.Id == id).Include("Category").OrderBy(p => p.Name).SingleOrDefault();
         }
         else
         {
            int pid = (int)HttpContext.Session.GetInt32("prdID");
            product = _context.Products.Where(p => p.Id == pid).Include("Category").OrderBy(p => p.Name).SingleOrDefault();
         }
         return View(product);
      }

      public IActionResult Privacy()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}