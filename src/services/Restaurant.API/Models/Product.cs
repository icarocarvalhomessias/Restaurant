using System.Text.Json.Serialization;

namespace Restaurant.API.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Image { get; private set; }
        public int Stock { get; private set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; } = new List<Order>();

        public Product(string name, string description, decimal price, string image, int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Active = true;
            RegisterDate = DateTime.UtcNow;
            Image = image;
            Stock = stock;
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

        public void Activate()
        {
            Active = true;
        }

        public void UpdateStock(int stock)
        {
            Stock = stock;
        }
    }
}
