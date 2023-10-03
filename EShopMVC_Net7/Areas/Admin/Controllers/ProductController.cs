using EShopMVC_Net7.Areas.Admin.ViewModels.Product;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {


        public ProductController(EShopDbContext db) : base(db)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            //Lấy data từ db
            var cate = _db.AppCategorys
                          .OrderByDescending(c => c.Id)
                          .ToList();

            //Ép kiểu để sử dụng được với Asp-items
            ViewBag.Category = new SelectList(cate, "Id", "Name");

            return View();
        }

        [HttpPost]

        public IActionResult Create(ProductUpdinVM productVM, [FromServices] IWebHostEnvironment env)
        {
            //Xac thuc du lieu
            if (ModelState.IsValid == false)
            {
                return View(productVM);
            }
            // Sao chep tu wiewModal sang modal
            var product = new AppProduct
            {
                Name = productVM.Name,
                Slug = productVM.Slug,
                Content = productVM.Content,
                Summary = productVM.Summary,
                InStock = productVM.InStock,
                Price = productVM.Price,
                CategoryId = productVM.CategoryId,
                DiscountPrice = productVM.DiscountPrice,
                DiscountFrom = productVM.DiscountFrom,
                DiscountTo = productVM.DiscountTo,
                View = 0,
                CreatedAt = DateTime.Now,
            };
            // Upload anh bia (CoverImg)
            var fName = productVM.CoverImg.FileName;
            fName = Path.GetFileNameWithoutExtension(fName)
                    + DateTime.Now.Ticks
                    + Path.GetExtension(fName);
            product.CoverImg = "/upload/" + fName;
            fName = Path.Combine(env.WebRootPath, "upload", fName);

            var stream = System.IO.File.Create(fName);
            productVM.CoverImg.CopyTo(stream);
            stream.Dispose();

            _db.Add(product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Create));
        }
    }
}


