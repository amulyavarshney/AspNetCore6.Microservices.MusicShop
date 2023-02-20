namespace Order.Service.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime Date { get; set; }
        public int PaymentTerms { get; set; }
        public string ShippingAddress { get; set; }
        // public int ItemId { get; set; }
    }
}
