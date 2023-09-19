using EShopMVC_Net7.Common;
using EShopMVC_Net7.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EShopMVC_Net7.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        public UserController(EShopDbContext db) : base(db)
        {

        }
        public IActionResult Index(int page = 1)    // Phan trang
        {
            var data = _db.AppUsers
                .OrderByDescending(x => x.Id)
                .ToPagedList(page, PER_PAGE);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AppUser user, [FromServices] EShopDbContext db)
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
		    user.Role = user.Role;
			user.BlockedTo = null;

            db.AppUsers.Add(user);
            db.SaveChanges();





            return RedirectToAction(nameof(Index));
        }

    public IActionResult Update(int id)
        {
            var data = _db.AppUsers.Find(id);   // id trong Find(id) là khóa chính, ctr tự tìm

            if (data == null)
            {
                SetErrorMesg("Thông tin tài khoản không hệ lệ");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(int id, AppUser user)
        {
            var oldUser = _db.AppUsers.Find(id);

            if (oldUser == null) // Neu null quay ve trang Index
            {
                return RedirectToAction(nameof(Index));
            }
            //Validate Update
            ModelState.Remove("Username");                                      
            ModelState.Remove("Password");
            ModelState.Remove("CfmPassword");

            if (ModelState.IsValid == false) // Neu null quay ve trang Index
            {
                return View(user);
            }
            //Chuan hoa Email
            user.Email = user.Email.ToLower().Trim();
            //Kiem tra Email da ton tai chua
            var exists = _db.AppUsers.Any(u => u.Email == user.Email && u.Id != id);
            if (exists)
            {
                ModelState.AddModelError("", "Email đã tồn tại trong hệ thống");
                return View(user);
            }
            //Update
            oldUser.Role = user.Role; // oldUser data duoc lay ra tu database
            oldUser.Email = user.Email;
            oldUser.Address = user.Address;
            oldUser.Phone = user.Phone;
            //Save
            _db.SaveChanges();

            SetSuccesMesg("Cập nhật tài khoản thành công");

            //===Ket thuc===
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var data = _db.AppUsers.Find(id);   // id trong Find(id) là khóa chính, ctr tự tìm

            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _db.Remove(data);
            _db.SaveChanges();
            SetSuccesMesg ($"Xóa tài khoản [{ data.Username}] thành công");
            return RedirectToAction(nameof(Index));
        }
    }
}
