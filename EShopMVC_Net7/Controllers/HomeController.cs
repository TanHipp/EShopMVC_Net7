using EShopMVC_Net7.Areas.Admin.Controllers;
using EShopMVC_Net7.Models;
using EShopMVC_Net7.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace EShopMVC_Net7.Controllers
{
    public class HomeController : ClientBaseController
    {
        private object p;

        public HomeController(EShopDbContext db) : base(db)
        {

        }




        [Route("/san-pham/{slug?}/{cateId:int?}")] // => Làm đẹp cho thanh Url
        [Route("/")]


        public IActionResult Index(int page = 1, int? cateId = null, string? slug = "")
        {
            ViewBag.Title = "Trang Chủ";
            var query = _db.AppProducts.AsQueryable();
            if (cateId != null)
            {
                var cateName = _db.AppCategorys.Find(cateId)?.Name;
                ViewBag.Title = "Sản phẩm " + cateName;
                query = query.Where(p => p.CategoryId == cateId);
            }
            var data = query.Select(p => new ProductListItemVM
            {
                Id = p.Id,
                Name = p.Name,
                CoverImg = p.CoverImg,
                Summary = p.Summary,
                Slug = p.Slug,
                Price = p.Price,
                DiscountFrom = p.DiscountFrom,
                DiscountPrice = p.DiscountPrice,
                DiscountTo = p.DiscountTo,
                CategoryName = p.Category.Name,
            })
                        .OrderByDescending(p => p.Id)
                        .ToPagedList(page, PER_PAGE);  // Phân trang sản phẩm
            return View(data);
        }

        //Trang Detail (Chi tiết sản phẩm)
        public IActionResult Detail(int id)
        {
            var data = _db.AppProducts
                          .Include(p => p.ProductImages)
                          .Where(p => p.Id == id)
                          .SingleOrDefault();
            if (data == null)
            {
                SetErrorMesg("Không tìm thấy vui lòng thử lại");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }



        // Chức năng tìm kiếm
        [Route("/tim-kiem")]
        public IActionResult Search(int page = 1, string keyword = "")
        {
            ViewBag.Title = $"Kết quả tìm kiếm cho '{keyword}'";
            var data = _db.AppProducts
                          .Where(p => p.Name.Contains(keyword) || p.Summary.Contains(keyword))
                          .Select(p => new ProductListItemVM
                          {
                              Id = p.Id,
                              Name = p.Name,
                              CoverImg = p.CoverImg,
                              Summary = p.Summary,
                              Slug = p.Slug,
                              Price = p.Price,
                              DiscountFrom = p.DiscountFrom,
                              DiscountPrice = p.DiscountPrice,
                              DiscountTo = p.DiscountTo,
                              CategoryName = p.Category.Name,
                          })
                        .OrderByDescending(p => p.Id)
                        .ToPagedList(page, PER_PAGE);  // Phân trang sản phẩm
            return View("Index", data);
        }






    }
}