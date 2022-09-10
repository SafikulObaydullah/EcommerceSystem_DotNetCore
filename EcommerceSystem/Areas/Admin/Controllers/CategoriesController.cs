using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Controllers
{
   public class CategoriesController : Controller
   {
      public DbModels _context;
      public IWebHostEnvironment _webHost;
      public CategoriesController(DbModels context,IWebHostEnvironment webHost)
      {
         this._context = context;
         this._webHost = webHost;
      }
      public async Task<IActionResult> Index()
      {
         return View(await _context.Categories.ToListAsync());
      }
      [HttpGet]
      public async Task<IActionResult> Create()
      {
         ViewBag.ParentID = new SelectList(await _context.Categories.OrderBy(c=>c.Name).ToListAsync(), "Id", "Name");
         return View();
      }
      [HttpPost]
      public async Task<IActionResult> Create(Category category)
      {
         if(category == null)
         {
            return NotFound();
         }
         if(ModelState.IsValid)
         {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewBag.ParentID = new SelectList(await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
         return View(category);
      }
      
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.Categories == null)
         {
            return NotFound();
         }

         var category = await _context.Categories.FindAsync(id);
         if (category == null)
         {
            return NotFound();
         }
         return View(category);
      }
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ParentID")] Category category)
      {
         if (id != category.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(category);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!CategoryExists(category.Id))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(category);
      }
      private bool CategoryExists(int id)
      {
         return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}
