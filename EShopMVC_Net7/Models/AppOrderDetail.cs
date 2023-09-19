namespace EShopMVC_Net7.Models
{
   public class AppOrderDetail
   {
      public int Id { get; set; }
      public int OrderId { get; set; }
      public string ProductId { get; set; }
      public string ProductName { get; set; }
      public int Price { get; set; }
      public int Quality { get; set; }

   }
}
