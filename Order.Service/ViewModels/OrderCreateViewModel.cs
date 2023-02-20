using System.ComponentModel.DataAnnotations;

namespace Order.Service.ViewModels
{
    public class OrderCreateViewModel
    {
        [Required]
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public int PaymentTerms { get; set; }
        public string ShippingAddress { get; set; }

        /*[Required]
        public int ItemId { get; set; }*/
    }
}
