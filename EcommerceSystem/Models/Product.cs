using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceSystem.Models
{
   public class Product
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      [Display(Name = "Purchase Price")]
      public int PurchasePrice { get; set; }
      [Display(Name = "Sales Price")]
      public int SalePrice { get; set; }
      [ValidateNever]
      public string ImagePath { get; set; }
      [NotMapped]
      public IFormFile ImageFile { get; set; }
      [ForeignKey("Category")]
      public int CatID { get; set; }
      [ValidateNever]
      public Category Category { get; set; }
     

   }
}
