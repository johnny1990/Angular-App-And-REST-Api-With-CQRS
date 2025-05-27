namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
