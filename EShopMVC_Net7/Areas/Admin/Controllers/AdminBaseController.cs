using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;

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
