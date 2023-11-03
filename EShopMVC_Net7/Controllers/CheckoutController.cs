using Microsoft.AspNetCore.Mvc;
using EShopMVC_Net7.ViewModels.Cart;
using EShopMVC_Net7.Models;
using EShopMVC_Net7.Areas.Admin.Controllers;

namespace EShopMVC_Net7.Controllers
{
    public class CheckoutController : ClientBaseController
    {

        public CheckoutController(EShopDbContext db) : base(db)
        {
        }

        [HttpPost]
        public IActionResult Checkout(CustomerInforVM customer)
        {
            if (ModelState.IsValid == false)
            {
                SetErrorMesg("Dữ liệu không hợp lệ");
                return RedirectToAction("Index", "Cart");
            }
            AppOrder order = new()
            {
                CustomerAddress = customer.CustomerAddress,
                CustomerEmail = customer.CustomerEmail,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                Status = Common.OrderStatus.PENDING,
                CreateAt = DateTime.Now,
            };
            //Trích xuất thông tin từ giỏ hàng  
            var productIds = HttpContext.Session.Keys
                           .Where(c => c.StartsWith("Cart_"))
                           .Select(c => Convert.ToInt32(c.Substring(5)))
                           .ToList();
            if (productIds != null)
            {
                //Lấy thông tin sản phẩm từ Database lên !
                var products = _db.AppProducts
                                  .Where(p => productIds.Contains(p.Id))
                                  .Select(p => new CartListItemVM
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      CoverImg = p.CoverImg,
                                      Price = p.Price.Value,
                                      DiscountPrice = p.DiscountPrice,
                                      DiscountFrom = p.DiscountFrom,
                                      DiscountTo = p.DiscountTo,
                                      QuantityInCart = HttpContext.Session.GetInt32("Cart_" + p.Id) ?? 0
                                  })
                                  .ToList();
                //Thêm sản phẩm vào OrderDetail
                foreach (var p in products)
                {
                    order.Details.Add(new AppOrderDetail
                    {
                        Price = p.FinalPrice,
                        ProductId = p.Id,
                        ProductName = p.Name,
                        Quality = p.QuantityInCart

                    });
                }

                //Tính tổng giá
                order.TotalPrice = order.Details.Sum(o => o.Quality * o.Price);

                //Lưu vào db
                _db.Add(order);
                _db.SaveChanges();
                SetSuccesMesg("Đặt hàng thành công");

            }
            return RedirectToAction("Index", "Home");
        }
    }
}