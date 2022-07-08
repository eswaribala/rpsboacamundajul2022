namespace OrderAPI.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public long OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
