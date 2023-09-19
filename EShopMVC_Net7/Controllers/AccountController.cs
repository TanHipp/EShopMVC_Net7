using EShopMVC_Net7.Common;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVC_Net7.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public IActionResult Register(AppUser user, [FromServices] EShopDbContext db)
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
			var exists = db.AppUsers.Any(u => u.Email == user.Email || u.Username == user.Username);

			if (exists)
			{
				ModelState.AddModelError("Err", "Email da duoc nhap hoac su dung!");
				return View(user);
			}


			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);  // Doan code de Hash mat khau


			/*EShopDbContext db = new();*/
			user.Role = UserRole.ROLECUSTOMER;
			user.BlockedTo = null;

			db.AppUsers.Add(user);
			db.SaveChanges();



			return RedirectToAction(nameof(Register));
		}
	}
}
