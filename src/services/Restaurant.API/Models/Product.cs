using System.Text.Json.Serialization;

namespace Restaurant.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public ProductType Type { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public Product(string name, string description, decimal price, string image, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            Stock = stock;
            RegisterDate = DateTime.UtcNow;
            Active = true;
        }

        public void UpdateDetails(string name, string description, decimal price, string image, int stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            Stock = stock;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }

    public enum ProductType
    {
        Food,
        Drink
    }
}
