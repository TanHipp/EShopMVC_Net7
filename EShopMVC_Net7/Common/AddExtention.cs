using Microsoft.AspNetCore.Http;

namespace EShopMVC_Net7.Common
{
    public static class AddExtention
    {
        public static void SetUserId(this HttpContext context,int userId)
        {
            context.Session.SetInt32("userId", userId);
        }
        public static int? GetUserId(this HttpContext context)
        {
            return context.Session.GetInt32("userId");
        }

        public static string GetUserName(this HttpContext context)
        {
            return context.Session.GetString("Username") ?? "";
        }
        
        public static void SetUsername(this HttpContext context,string username)
        {
            context.Session.SetString("username", username);
        }

        public static void SetRole(this HttpContext context,UserRole role)
        {
            context.Session.SetInt32("userRole", (int)role);
        }
        public static bool IsAdmin(this HttpContext context)
        {
            return context.Session.GetInt32("userRole") == (int)UserRole.ROLEADMIN; //=> Quyền của Admin 
        }
    }
}
