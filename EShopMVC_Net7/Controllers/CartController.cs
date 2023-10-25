using EShopMVC_Net7.Areas.Admin.Controllers;
using EShopMVC_Net7.Models;
using EShopMVC_Net7.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Controllers
{
    public class CartController : ClientBaseController
    {
        public CartController(EShopDbContext db) : base(db)
        {

        }
        public IActionResult Index()
        {
            var cartIds = HttpContext.Session.Keys
                                  .Where(c => c.StartsWith("Cart_"))
                                  .Select(c => Convert.ToInt32 (c.Substring(5)))
                                  .ToList();
            
            if(cartIds != null )
            {
                //Lấy thông tin sản phẩm từ Database lên !
                var products = _db.AppProducts
                                  .Where(p=>cartIds.Contains(p.Id))
                                  .Select(p=> new CartListItemVM
                                  { 
                                      Id = p.Id,
                                      Name = p.Name,
                                      CoverImg = p.CoverImg,
                                      Price = p.Price,
                                      DiscountPrice = p.DiscountPrice,
                                      DiscountFrom = p.DiscountFrom,
                                      DiscountTo = p.DiscountTo,    
                                      QuantityInCart = HttpContext.Session.GetInt32("Cart_" + p.Id) ?? 0
                                  })
                                  .ToList();
                                  return View(products);
                



            }

            return View();
        }

        public IActionResult AddToCart([FromQuery] int productId)
        {
            //Trường hợp thêm sản phẩm vào giỏ hàng nếu CHƯA CÓ SẢN PHẨM !!

            var quantity = HttpContext.Session.GetInt32("Cart_" + productId) ?? 0;
            HttpContext.Session.SetInt32("Cart_" + productId, quantity + 1);

            var referer = HttpContext.Request.Headers["Referer"].ToString();
            return Redirect(referer);

        }
    }
}
