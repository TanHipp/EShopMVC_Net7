using System.ComponentModel.DataAnnotations;

namespace EShopMVC_Net7.Models
{
    public class AppCategory
    {
        public AppCategory()
        {
            Products = new HashSet<AppProduct>();
        }


        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Slug { get; set; }



        // 1 danh mục có nhiều sản phẩm
        public ICollection<AppProduct> Products { get; set; }
    }
}
