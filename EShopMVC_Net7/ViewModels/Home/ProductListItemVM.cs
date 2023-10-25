namespace EShopMVC_Net7.ViewModels.Home
{
    public class ProductListItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Summary { get; set; }

        public string CoverImg { get; set; } // Ảnh danh sách đầu trang

        public double? Price { get; set; }

        public double? DiscountPrice { get; set; } // Giá khuyến mãi

        public DateTime? DiscountFrom { get; set; }

        public DateTime? DiscountTo { get; set; }

        public string CategoryName { get; set; }


    }
}
