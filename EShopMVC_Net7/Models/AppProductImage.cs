namespace EShopMVC_Net7.Models
{
    public class AppProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }

        //1 hình ảnh có 1 sản phẩm
        public AppProduct Product { get; set; }
    }
}
