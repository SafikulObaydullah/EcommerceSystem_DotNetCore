using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceSystem.ViewComponents
{
   public class CategoryListViewComponent : ViewComponent
   {
      private readonly DbModels _context;

      public CategoryListViewComponent(DbModels context)
      {
         this._context = context;
      }
      public async Task<IViewComponentResult> InvokeAsync()
      {
         var data = _context.Categories.OrderBy(c => c.Name).ToList();

         return View(data);
      }
   }
}
