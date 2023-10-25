using EShopMVC_Net7.Common;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected const int PER_PAGE = 15;

        protected EShopDbContext _db;    

        public AdminBaseController(EShopDbContext db)
        {
            _db = db;
        }


        //Chức năng phân quyền Admin và Khách Hàng
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check nếu chưa đăng nhập
            if (context.HttpContext.GetUserId() == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
                SetErrorMesg("Vui lòng đăng nhập để sử dụng tính năng này");
                return;
            }

            //Check quyền đăng nhập Admin
            if (context.HttpContext.IsAdmin() == false)
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
                SetErrorMesg("Không có quyền truy cập trang này...");
                return;
            }
        }


        // Chuc nang UPDATE
        protected void SetSuccesMesg(string msg)
        {
            TempData["_SucessMesg"] = msg;
        }
        protected void SetErrorMesg(string msg)
        {
            TempData["_ErrorMesg"] = msg;
        }

    }
}
