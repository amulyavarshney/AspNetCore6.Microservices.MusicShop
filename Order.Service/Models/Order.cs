namespace Order.Service.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int PaymentTerms { get; set; }
        public string ShippingAddress { get; set; }
        // public int ItemId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
