namespace EShopMVC_Net7.ViewModels.Cart
{
    public class CartListItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CoverImg { get; set; } // Ảnh danh sách đầu trang

        public double Price { get; set; }

        public double? DiscountPrice { get; set; } // Giá khuyến mãi

        public DateTime? DiscountFrom { get; set; }

        public DateTime? DiscountTo { get; set; }

        public string CategoryName { get; set; }


        //Số lượng sản phẩm trong giỏ hàng
        public int QuantityInCart { get; set; }


        //Logic xác định giá khuyến mãi hay không
        public bool IsDiscount
        {
            get
            {
                var isDiscount = false;
                if (DiscountPrice.HasValue)
                {
                    //Đoạn code thực hiện GIẢM GIÁ sản phẩm
                    var starDate = DiscountFrom ?? DateTime.MinValue;
                    var endDate = DiscountTo ?? DateTime.MaxValue;
                    isDiscount = starDate <= DateTime.Now && endDate >= DateTime.Now;
                }
                return isDiscount;

            }
        }

        // GIá cuối cùng
        public double FinalPrice
        {
            get
            {
                return IsDiscount ? DiscountPrice.Value : Price;
            }
        }
    }
}
