using System.ComponentModel.DataAnnotations;

namespace Customer.Service.ViewModels
{
    public class CustomerUpdateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
