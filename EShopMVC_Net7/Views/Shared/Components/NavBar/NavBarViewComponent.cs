using EShopMVC_Net7.Areas.Admin.ViewModels.Category;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EShopMVC_Net7.Views.Shared.Components.NavBar
{
    public class NavBarViewComponent :  ViewComponent
    {
        private EShopDbContext _db;

        public NavBarViewComponent(EShopDbContext db)
        {
            _db = db;
            
        }

        public IViewComponentResult Invoke()
        {
            var category = _db.AppCategorys              
                              .OrderByDescending(c => c.Id)
                              .ToList();

            return View(category);
        }
    }
}
