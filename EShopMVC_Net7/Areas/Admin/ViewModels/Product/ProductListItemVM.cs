namespace EShopMVC_Net7.Areas.Admin.ViewModels.Product
{
    public class ProductListItemVM
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string CoverImg { get; set; } // Ảnh danh sách đầu trang

        public int InStock { get; set; } // Tồn kho

        public double? Price { get; set; }

        public double? DiscountPrice { get; set; } // Giá khuyến mãi

        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }

        public int? View { get; set; }

        public int? CategoryId { get; set; } // Khóa ngoại, danh mục sản phẩm

        public   string CategoryName { get; set; }
    }
}
