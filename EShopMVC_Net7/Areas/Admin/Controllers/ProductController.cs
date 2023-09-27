using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        public ProductController(EShopDbContext db): base(db) 
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create() => View();
    }
}
