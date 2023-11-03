namespace EShopMVC_Net7.Common
{
   public enum UserRole
   {
      ROLECUSTOMER = 1, // Gang gia tri 1 va 2 nhin vao de hieu hon
      ROLEADMIN = 2
   }

    public enum OrderStatus
    {
        PENDING,         //Đang chờ

        APPROVED,       //Đang duyệt

        DELIVERED,      //Đang giao 

        CANCELED,         // Đang hủy
    }
}
