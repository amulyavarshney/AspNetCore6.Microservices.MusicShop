using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Service.Models
{
    [Table("Customer")]
    public class CustomerData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
