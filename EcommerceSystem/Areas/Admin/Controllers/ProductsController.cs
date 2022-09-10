using EcommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Areas.Admin.Controllers
{
   public class ProductsController: Controller
   {
      private readonly DbModels _context;
      IWebHostEnvironment _webhost;
      public ProductsController(DbModels context,IWebHostEnvironment webhost)
      {
         this._context = context;
         this._webhost = webhost;
      }
      public async Task<IActionResult> Index()
      {
         var dbmodels =  _context.Products.Include(p=>p.Category);
         return View(await dbmodels.ToListAsync());
      }
      public async Task<IActionResult> Details(int? id)
      {
         if(id==null || _context.Products == null)
         {
            return NotFound();
         }
         var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync();
         if(product==null)
         {
            return NotFound();
         }
         return View(product);
      }
      public IActionResult Create()
      {
         ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
         return View();
      }
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("Id,Name,Description,PurchasePrice,SalePrice,ImageFile,CatID")] Product product)
      {
         if (ModelState.IsValid)
         {
            if (product.ImageFile != null)
            {
               string ext = Path.GetExtension(product.ImageFile.FileName);
               if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
               {
                  string fName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                  string flName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\Images\\Products");
                  if (!Directory.Exists(flName))
                  {
                     Directory.CreateDirectory(flName);
                  }
                  string filetoSave = Path.Combine(flName, fName + "_" + product.Name + ext);
                  using (FileStream fs = new FileStream(filetoSave, FileMode.Create, FileAccess.Write))
                  {
                     product.ImageFile.CopyTo(fs);
                  }
                  product.ImagePath = "/Images/Products/" + fName + "_" + product.Name + ext;
                  _context.Add(product);
                  if (await _context.SaveChangesAsync() > 0)
                  {
                     return RedirectToAction(nameof(Index));
                  }

               }
               else
               {
                  ModelState.AddModelError("", "Please Provide valide image");
                  ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
                  return View(product);

               }


            }
            else
            {
               ModelState.AddModelError("", "Please Provide valide image");
               ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
               return View(product);
            }
         }

         ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
         return View(product);

      }
      public async Task<IActionResult> Edit(int? id)
      {
         if(id == null || _context.Products==null)
         {
            return NotFound();
         }
         var product = await _context.Products.FindAsync(id);
         if(product == null)
         {
            return NotFound();
         }
         ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
         return View(product);
      }
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PurchasePrice,SalePrice,ImagePath,ImageFile,CatID")] Product product)
      {
         if (id != product.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               //_context.Update(product);
               //await _context.SaveChangesAsync();
               if (product.ImageFile != null)
               {
                  string ext = Path.GetExtension(product.ImageFile.FileName);
                  if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                  {
                     string fName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                     string flName = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\Images\\Products");
                     if (!Directory.Exists(flName))
                     {
                        Directory.CreateDirectory(flName);
                     }
                     string filetoSave = Path.Combine(flName, fName + "_" + product.Name + ext);
                     using (FileStream fs = new FileStream(filetoSave, FileMode.Create, FileAccess.Write))
                     {
                        product.ImageFile.CopyTo(fs);
                     }
                     product.ImagePath = "/Images/Products/" + fName + "_" + product.Name + ext;


                  }
                  else
                  {
                     ModelState.AddModelError("", "Please Provide valide image");
                     ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
                     return View(product);

                  }


               }
               else if (product.ImagePath != null || product.ImagePath != "")
               {
                  product.ImagePath = product.ImagePath;
               }

               else
               {
                  ModelState.AddModelError("", "Please Provide valide image");
                  ViewData["CatID"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CatID);
                  return View(product);
               }
               _context.Update(product);
               if (await _context.SaveChangesAsync() > 0)
               {
                  return RedirectToAction(nameof(Index));
               }

            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ProductExists(product.Id))
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
         ViewData["CatID"] = new SelectList(_context.Categories, "Id", "Id", product.CatID);
         return View(product);
      }
      public async Task<IActionResult> Delete(int? id)
      {
         if(id == null || _context.Products == null)
         {
            return NotFound();
         }
         var product = _context.Products.Include(p => p.Category).FirstOrDefault(m=>m.Id==id);
         if(product == null)
         {
            return NotFound();
         }
         return View(product); 
      }
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         if(_context.Products == null)
         {
            return Problem("Entity set 'DbModel.Products' is null.");
         }
         var product = await _context.Products.FindAsync(id);
         if(product != null)
         {
            using(var tran = _context.Database.BeginTransaction())
            {
               try
               {
                  _context.Products.Remove(product);
                  await _context.SaveChangesAsync();
                  var t = _webhost.WebRootPath + product.ImagePath.Replace('/', '\\');
                  FileInfo fileInfo = new FileInfo(t);
                  if(fileInfo.Exists)
                  {
                     fileInfo.Delete();
                     tran.Commit();
                     return RedirectToAction("Index");
                  }
                  else
                  {
                     tran.Rollback();
                     ModelState.AddModelError("", "File not exist");
                     return View();
                  }
               }
               catch(Exception ex)
               {
                  tran.Rollback();
                  return Problem(ex.Message);
               }
            }
         }
         return Problem("Entity set 'DbModels.Products' can not delete");
      }

      private bool ProductExists(int id)
      {
         return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}
