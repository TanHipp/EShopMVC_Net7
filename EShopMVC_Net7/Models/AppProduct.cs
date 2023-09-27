namespace EShopMVC_Net7.Models
{
    public class AppProduct
    {
        public AppProduct()
        {
            ProductImages = new HashSet<AppProductImage>();
        }





        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string CoverImg { get; set; } // Ảnh danh sách đầu trang

        public string Summary { get; set; }    // Mô tả ngắn

        public string Content { get; set; }   // Mô tả đầy đủ

        public int InStock { get; set; } // Tồn kho

        public double? Price { get; set; }

        public double? DiscountPrice { get; set; } // Giá khuyến mãi

        public DateTime? DiscountFrom { get; set; }

        public DateTime? DiscountTo { get; set; }

        public int? View { get; set; }

        public int? CategoryId { get; set; } // Khóa ngoại, danh mục sản phẩm

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        // 1 sản phẩm có 1 danh mục
        public AppCategory Category { get; set; }
        //  1 sản phẩm có nhiều hình ảnh 
        public ICollection<AppProductImage> ProductImages { get; set; }
    }
  
}
