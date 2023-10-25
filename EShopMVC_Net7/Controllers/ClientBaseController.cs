using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class ClientBaseController : Controller
    {
        protected const int PER_PAGE = 15;

        protected EShopDbContext _db;
        public ClientBaseController(EShopDbContext db)
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
