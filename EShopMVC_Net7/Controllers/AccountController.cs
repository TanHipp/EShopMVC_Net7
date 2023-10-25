using EShopMVC_Net7.Areas.Admin.Controllers;
using EShopMVC_Net7.Common;
using EShopMVC_Net7.Models;
using EShopMVC_Net7.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Controllers
{
	public class AccountController : ClientBaseController
	{
		public AccountController(EShopDbContext db)	: base(db)
		{
		
		}

        // => Code logic xử lí đăng nhập tài khoản
        [HttpGet]
		public IActionResult Login() { 
              return View();
        } 
		[HttpPost]
		public IActionResult Login(LoginVM loginVM) { 
			if(ModelState.IsValid == false)
			{
				ModelState.AddModelError("", "Dữ liệu không hợp lệ");
				return View(loginVM);
			}

            var user = _db.AppUsers.SingleOrDefault(u => u.Username == loginVM.Username);

            if (user == null)
			{
				ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ");
                return View(loginVM);
            }

			if(BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password) == false)
			{
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ");
                return View(loginVM);
            }

			HttpContext.SetUserId(user.Id);
            HttpContext.SetUsername(user.Username);
			HttpContext.SetRole(user.Role);

            return RedirectToAction("Index","Home");//=> Đăng nhập xong chuyển về trang chủ
		}

        //=> Code logic xử lí đăng xuất tài khoản

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}



        // => Code logic xử lí đăng kí tài khoản
        [HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public IActionResult Register(AppUser user)
		{
			if (ModelState.IsValid == false)
			{
				return View(user);
			}



			// Chuan hoa ten va mail (chuyen thanh chu Thuong khi chuyen len database)
			/*user.Username = user.Username.ToLower().Trim();
			user.Email = user.Email.ToLower().Trim();*/

			// Chuan hoa ten va mail (chuyen thanh chu Hoa khi chuyen len databse )
			/*user.Username = user.Username.ToUpper().Trim();
			user.Email = user.Email.ToUpper().Trim();*/


			// Kiem tra username va email da ton tai trong database chua
			var exists = _db.AppUsers.Any(u => u.Email == user.Email || u.Username == user.Username);

			if (exists)
			{
				ModelState.AddModelError("Err", "Email da duoc nhap hoac su dung!");
				return View(user);
			}


			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);  // Doan code de Hash mat khau


			/*EShopDbContext db = new();*/
			user.Role = UserRole.ROLECUSTOMER;
			user.BlockedTo = null;

			_db.AppUsers.Add(user);
			_db.SaveChanges();



			return RedirectToAction(nameof(Register));
		}
	}
}
