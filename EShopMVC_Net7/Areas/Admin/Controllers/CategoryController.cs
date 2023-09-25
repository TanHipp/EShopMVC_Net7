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

        // Đường dẫn đến trang Upsert => /Admin/Category/Upsert/1
        public IActionResult Upsert(int? id, [FromBody] CategoryUpdinVM item)
        {
            if (id == null)
            {
                //Coppy dữ liệu từ view modal sang modal
                var category = new AppCategory
                {
                    Name = item.Name,
                    Slug = item.Slug,
                };
                _db.Add(category);
                _db.SaveChanges();
                return Ok($"Thêm danh mục [{item.Name}] thành công");
            }
            else
            {    //Update du lieu 
                var oldCategory = _db.Find<AppCategory>(id);
                if (oldCategory != null)
                {
                    oldCategory.Name = item.Name;
                    oldCategory.Slug = item.Slug;

                    _db.SaveChanges();
                }
                return Ok($"Cập nhật danh mục [{item.Name}] thành công");
            }
        }

        public AppCategory Detail(int id)
        {
            return _db.AppCategorys.Find(id);
        }

        // Chức năng xóa
        public IActionResult Delete(int id)
        {
            var data = _db.AppCategorys.Find(id);   // id trong Find(id) là khóa chính, ctr tự tìm
            if (data != null)
            {
                _db.Remove(data);
                _db.SaveChanges(true);
            }
            return Ok();
        }
    }
}
