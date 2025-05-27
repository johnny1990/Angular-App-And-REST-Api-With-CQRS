using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }

        [JsonIgnore]
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
