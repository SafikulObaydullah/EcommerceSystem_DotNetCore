using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.ViewComponents
{
   public class CategoryAddViewComponent : ViewComponent
   {
      private readonly DbModels _context;
      public CategoryAddViewComponent(DbModels context)
      {
         this._context = context;
      }
      public async Task<IViewComponentResult> InvokeAsync()
      {
         ViewBag.ParentID = new SelectList(_context.Categories.OrderBy(c => c.Name).ToList(), "Id", "Name");
         return View();
      }
   }
}
