using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        public OrderController(EShopDbContext db) : base (db) 
        {
        
        }
        public IActionResult Index(int page = 1)    // Phan trang
        {
            var data = _db.AppOrders
                .OrderByDescending(o => o.Id)
                .ToPagedList(page, PER_PAGE);
            return View(data);
        }

    
    }
}
