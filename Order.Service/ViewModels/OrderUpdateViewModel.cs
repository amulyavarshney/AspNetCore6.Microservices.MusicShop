using System.ComponentModel.DataAnnotations;

namespace Order.Service.ViewModels
{
    public class OrderUpdateViewModel
    {
        [Required]
        public int PaymentTerms { get; set; }
        public string ShippingAddress { get; set; }
    }
}
