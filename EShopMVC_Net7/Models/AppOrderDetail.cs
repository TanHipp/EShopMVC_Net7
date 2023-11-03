namespace EShopMVC_Net7.Models
{
   public class AppOrderDetail
   {
      public int Id { get; set; }
      public int OrderId { get; set; }
      public int ProductId { get; set; }
      public string ProductName { get; set; }
      public double Price { get; set; }
      public int Quality { get; set; }

        public AppOrder? AppOrder { get; set; }

   }
}
