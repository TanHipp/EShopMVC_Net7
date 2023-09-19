namespace EShopMVC_Net7.Models
{
   public class AppOrderProduct
   {
      public int Id { get; set; }
      public int TotalPrice { get; set; }
      public string CustomerName { get; set; }
      public int CustomerPhone { get; set; }
      public string CustomerEmail { get; set; }
      public string CustomerAddress { get; set; }
      public int CustomerId { get; set; }
      public string Status { get; set; }
      public DateTime? CreateAt { get; set; }
   }
}
