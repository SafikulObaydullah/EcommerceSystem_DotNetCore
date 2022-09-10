namespace EcommerceSystem.Models
{
   public class ShopingCart
   {
      public ShopingCart()
      {
         Product = new Product();
      }
      public int Id { get; set; }
      public int ProductId { get; set; }
      public int Quantity { get; set; }
      public decimal Total
      {
         get
         {
            return Quantity * Product.SalePrice;
         }
      }
      public System.DateTime DateCreated { get; set; }
      public Product Product { get; set; }
   }
}
