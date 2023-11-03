using EShopMVC_Net7.Common;

namespace EShopMVC_Net7.Models
{
   public class AppOrder
   {
        public AppOrder() 
        {
            Details = new HashSet<AppOrderDetail>();
        }
      public int Id { get; set; }
      public double TotalPrice { get; set; }
      public string CustomerName { get; set; }
      public string CustomerPhone { get; set; }
      public string CustomerEmail { get; set; }
      public string CustomerAddress { get; set; }
      public int? CustomerId { get; set; }
      public OrderStatus? Status { get; set; }      // Chờ tiếp nhận, đã tiếp nhận, đã giao!
      public DateTime? CreateAt { get; set; }

      virtual public ICollection<AppOrderDetail> Details {  get; set; } 
   }
}
