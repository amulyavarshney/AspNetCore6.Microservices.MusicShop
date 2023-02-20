using System.ComponentModel.DataAnnotations;

namespace Customer.Service.ViewModels
{
    public class CustomerCreateViewModel
    {
        [Required]
        public string Name { get; set; }

       [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
