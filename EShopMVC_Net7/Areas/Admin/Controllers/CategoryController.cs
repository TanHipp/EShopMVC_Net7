using EShopMVC_Net7.Areas.Admin.ViewModels.Category;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private List<AppCategory> data;

        public CategoryController(EShopDbContext db) : base(db)
        {

        }
        public IActionResult Index() => View();

        public IActionResult ListAll2()
        {
            var data = _db.AppCategorys
                  .OrderByDescending(x => x.Id)
                  .ToList();

            return Ok(data);
        }

        public List<CategoryListItemVM> ListAll()
        {
            var data = _db.AppCategorys
                .Select(x => new CategoryListItemVM
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderByDescending(x => x.Id)
                .ToList();
            return data;
        }

    }
}
