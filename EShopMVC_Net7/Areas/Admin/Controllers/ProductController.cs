using EShopMVC_Net7.Areas.Admin.ViewModels.Product;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {


        public ProductController(EShopDbContext db) : base(db)
        {
        }
        public IActionResult Index(int page = 1)
        {
            var products = _db.AppProducts
                               .Select(p => new ProductListItemVM
                               {
                                   CategoryId = p.CategoryId,
                                   CoverImg = p.CoverImg,
                                   DiscountFrom = p.DiscountFrom,
                                   DiscountPrice = p.DiscountPrice,
                                   DiscountTo = p.DiscountTo,
                                   Id = p.Id,
                                   Name = p.Name,
                                   Price = p.Price,
                                   View = p.View,
                                   CategoryName = p.Category.Name,
                               })
                               .OrderByDescending(p => p.Id)
                               .ToPagedList(page, PER_PAGE);  // Phân trang sản phẩm

            return View(products);
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
            //Xác thục dữ liệu
            if (ModelState.IsValid == false)
            {
                return View(productVM);
            }
            // Sao chép từ wiewModal sang modal
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
            product.CoverImg = UploadFile(productVM.CoverImg, env.WebRootPath);

            //Upload ảnh sản phẩm (Ảnh sản phẩm chi tiết)
            foreach (var img in productVM.ProductImages)
            {
                if (img != null)
                {
                    // Tạo model cho ảnh sản phẩm và thêm vào cùng lúc với sản phẩm
                    var productImg = new AppProductImage();
                    productImg.Path = UploadFile(img, env.WebRootPath);
                    product.ProductImages.Add(productImg);
                }
            }
            try
            {
                _db.Add(product);
                _db.SaveChanges();
                SetSuccesMesg("Thêm sản phẩm thành công");
            }
            catch (Exception ex)
            {
                SetErrorMesg(ex.Message);
                SetSuccesMesg("Đã lưu...!");
            }
            return RedirectToAction(nameof(Create));
        }

        private string UploadFile(IFormFile file, string dir)
        {
            var fName = file.FileName;
            fName = Path.GetFileNameWithoutExtension(fName)
                    + DateTime.Now.Ticks
                    + Path.GetExtension(fName);
            var res = "/upload/" + fName;
            //Đường dẫn tuyệt đối (Ví dụ E:/Project/wwwroot/upload/xxx.jpg)
            fName = Path.Combine(dir, "upload", fName);
            //Tạo tream để lưu file
            var stream = System.IO.File.Create(fName);
            file.CopyTo(stream);
            stream.Dispose();   // Giả phóng bộ nhớ
            return res;
        }
    }
}


