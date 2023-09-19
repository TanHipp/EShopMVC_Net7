using Microsoft.EntityFrameworkCore;
using EShopMVC_Net7.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EShopMVC_Net7.Models
{

    [Index("Username", IsUnique = true)] // 2 dong nay goi la unique de khong bi trung lap du lieu
    [Index("Email", IsUnique = true)]






    public class AppUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ten Dang Nhap Bat Buoc")]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mat Khau Khong Khop")]
        [MaxLength(200)]
        [MinLength(4, ErrorMessage = "Vui long nhap tren 4 ki tu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mat Khau Khong Khop")]
        [NotMapped]
        [Compare("Password")]
        public string CfmPassword { get; set; }

        public UserRole Role { get; set; } // Phân quyền khách hàng
        [MaxLength(20)]
        [Required(ErrorMessage = "Vui long nhap dung so dien thoai")]
        public string Phone { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Vui long nhap dung dia chi")]
        public string Address { get; set; }
        [MaxLength(300)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Vui long nhap dung Email")]
        public string Email { get; set; }

        public DateTime? BlockedTo { get; set; } // Chức năng khóa tài khoản 
    }
}
