using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceSystem.ViewComponents
{
   public class CategoryEditViewComponent : ViewComponent
   {
      private readonly DbModels _context;
      public CategoryEditViewComponent(DbModels context)
      {
         this._context = context;
      }
      public async Task<IViewComponentResult> InvokeAsync(int id)
      {
         var data = _context.Categories.Where(c=>c.Id == id).FirstOrDefault();
         ViewBag.ParentID = new SelectList(_context.Categories.OrderBy(c => c.Name).ToList(), "Id", "Name",data.ParentID);
         return View(data);
      }
   }
}
