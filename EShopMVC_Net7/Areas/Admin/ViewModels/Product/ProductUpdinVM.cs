using EShopMVC_Net7.Models;
using System.ComponentModel.DataAnnotations;

namespace EShopMVC_Net7.Areas.Admin.ViewModels.Product
{
    public class ProductUpdinVM
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]

        public string Name { get; set; }
        [MaxLength(150)]
        [Required]
        public string Slug { get; set; }

        [Required]
        public IFormFile CoverImg { get; set; }
        [MaxLength(500)]
        [Required]                
        public string Summary { get; set; }

        public string Content { get; set; }

        public int InStock { get; set; }
        [Range(0, long.MaxValue)]
        [Required]

        public double? Price { get; set; }
        [Range(0, long.MaxValue)]

        public double? DiscountPrice { get; set; }

        public DateTime? DiscountFrom { get; set; }

        public DateTime? DiscountTo { get; set; }
        [Required]


        public int? CategoryId { get; set; }


        public DateTime? CreatedAt { get; set; }

        //  1 sản phẩm có nhiều hình ảnh 
        public IFormFileCollection ProductImages { get; set; }

        public string? CoverImgPath { get; set; }  // Hiển thị ảnh ra khi Update

        public List<string>? ProductImgPath { get; set; } = new();
    }
}
