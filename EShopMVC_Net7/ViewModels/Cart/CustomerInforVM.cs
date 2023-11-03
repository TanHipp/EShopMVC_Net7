using EShopMVC_Net7.Common;
using System.ComponentModel.DataAnnotations;

namespace EShopMVC_Net7.ViewModels.Cart
{
    public class CustomerInforVM
    {
        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(15)]
        public string CustomerPhone { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }
        [Required]
        [MaxLength(300)]
        public string CustomerAddress { get; set; }

    }
}
