using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.ViewComponents
{
   public class CategoryViewComponent : ViewComponent
   {
      public readonly DbModels _context;
      public CategoryViewComponent(DbModels context)
      {
         this._context = context;
      }
      public async Task<IViewComponentResult> InvokeAsync()
      {
         var data = _context.Categories.OrderBy(c => c.Id).ToList();
         return View(data);
      }
   }
}
