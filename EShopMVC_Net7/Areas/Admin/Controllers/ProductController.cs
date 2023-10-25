using EShopMVC_Net7.Areas.Admin.ViewModels.Product;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                       InStock = p.InStock,
                                       Slug = p.Slug,   
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

        //Update sản phẩm
        [HttpPost]
        public IActionResult Update(int id, ProductUpdinVM productVM, [FromServices] IWebHostEnvironment env)
        {
            ModelState.Remove("CoverImg");
            ModelState.Remove("ProductImages");

            //xác thực dữ liệu
            if (ModelState.IsValid == false)
            {
                return View(productVM);
            }


            var oldProduct = _db.AppProducts.Find(id);
            if (oldProduct == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(Index));

            }

            //coppy từ view model sang model
            oldProduct.Name = productVM.Name;
            oldProduct.Slug = productVM.Slug;
            oldProduct.Content = productVM.Content;
            oldProduct.Summary = productVM.Summary;
            oldProduct.InStock = productVM.InStock;
            oldProduct.Price = productVM.Price;
            oldProduct.CategoryId = productVM.CategoryId;
            oldProduct.DiscountPrice = productVM.DiscountPrice;
            oldProduct.DiscountFrom = productVM.DiscountFrom;
            oldProduct.DiscountTo = productVM.DiscountTo;

            //Upload ảnh bìa (CoverImg)

            if (productVM.CoverImg != null)
            {
                //Xoa anh bia cu
                System.IO.File.Delete(env.WebRootPath + oldProduct.CoverImg);
                oldProduct.CoverImg = UploadFile(productVM.CoverImg, env.WebRootPath);
            }

            if (productVM.ProductImages != null)
            {
                //Xóa ảnh trong database
                var pImgs = _db.AppProductImages.Where(i => i.ProductId == id).ToList();
                // Xoa file
                foreach (var img in pImgs)
                {
                    //Xóa ảnh bia cũ
                    System.IO.File.Delete(env.WebRootPath + img.Path);
                }
                _db.RemoveRange(pImgs);


                //Upload ảnh sản phẩm (Ảnh sản phẩm chi tiết)
                foreach (var img in productVM.ProductImages)
                {
                    if (img != null)
                    {
                        // Tạo model cho ảnh sản phẩm và thêm vào cùng lúc với sản phẩm

                        var productImg = new AppProductImage();
                        productImg.Path = UploadFile(img, env.WebRootPath);
                        oldProduct.ProductImages.Add(productImg);
                    }
                }

            }

            try
            {
                _db.SaveChanges();
                SetSuccesMesg("Cập nhật thông tin sản phẩm thành công");

            }
            catch (Exception ex)
            {
                SetErrorMesg("Đã xảy ra lỗi trong quá trình xử lí. Chi tiết: " + ex.Message);

            }


            return RedirectToAction(nameof(Index));
        }





        public IActionResult Update(int id)
        {
            var data = _db.AppProducts
                .Select(p => new ProductUpdinVM
                {
                    CategoryId = p.CategoryId,
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Content = p.Content,
                    DiscountFrom = p.DiscountFrom,
                    DiscountTo = p.DiscountTo,
                    DiscountPrice = p.DiscountPrice,
                    InStock = p.InStock,
                    Slug = p.Slug,
                    Summary = p.Summary,
                    CoverImgPath = p.CoverImg,
                    ProductImgPath = p.ProductImages.Select(pi => pi.Path).ToList(),
                })
                .Where(p => p.Id == id)   // id trong Find(id) là khóa chính, ctr tự tìm
                .SingleOrDefault();

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm cần chỉnh sửa...!");
                return RedirectToAction(nameof(Index));
            }
            //Lấy data từ db
            var cate = _db.AppCategorys
                          .OrderByDescending(c => c.Id)
                          .ToList();

            //Ép kiểu để sử dụng được với asp-items
            ViewBag.Category = new SelectList(cate, "Id", "Name", data.CategoryId); // => data.CategoryId để biết cập nhật mục nào
            return View(data);



        }



        public IActionResult Delete(int id, [FromServices] IWebHostEnvironment env)
        {
            var data = _db.AppProducts.Find(id);   // id trong Find(id) là khóa chính, ctr tự tìm

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm cần xóa...!");
                return RedirectToAction(nameof(Index));
            }

            //Lấy ảnh của sản phẩm vừa bị xóa
            var listImgs = _db.AppProductImages
                               .Where(p => p.Id == id)
                               .ToList();
            try
            {
                // Xóa dữ liệu trong DATABASE
                _db.Remove(data);
                // Xóa ảnh trong DISK
                //Xóa ảnh COVER
                System.IO.File.Delete(Path.Combine(env.WebRootPath, data.CoverImg.TrimStart('/')));   // Xử lí đường dẫn dấu / \ cùng 
                //Xóa ảnh CHI TIẾT
                foreach (var img in listImgs)
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath, img.Path.TrimStart('/')));
                }
                _db.SaveChanges();
                SetSuccesMesg($"Xóa sản phẩm [{data.Name}] thành công");

            }
            catch (Exception ex)
            {
                SetErrorMesg("Xóa không thành công. Chi tiết..." + ex.Message);
            }
            return RedirectToAction(nameof(Index));
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


