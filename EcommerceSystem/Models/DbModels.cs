using Microsoft.EntityFrameworkCore;

namespace EcommerceSystem.Models
{
   public class DbModels : DbContext
   {
      public DbModels(DbContextOptions<DbModels> options) : base(options)
      {

      }
      public DbSet<Category> Categories { get; set; }
      public DbSet<Product> Products { get; set; }
   }
}
