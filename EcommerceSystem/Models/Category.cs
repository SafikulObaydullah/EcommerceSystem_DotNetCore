using System.ComponentModel.DataAnnotations;

namespace EcommerceSystem.Models
{
   public class Category
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      [Display(Name = "Parent Category")]
      public int ParentID { get; set; }
      public ICollection<Product> Products { get; set; }

   }
}
