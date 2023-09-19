using System.ComponentModel.DataAnnotations;

namespace EShopMVC_Net7.Areas.Admin.ViewModels.Category
{
    public class CategoryUpdinVM
    {
        public int Id { get; set; }  
        [MaxLength(200)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(100)]
        [MinLength(3)]
        public string Slug { get; set; }
    }
}
