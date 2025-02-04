using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.API.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public Guid? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [NotMapped]
        public decimal TotalValue => OrderProducts.Sum(op => op.Product.Price * op.Quantity);

        [NotMapped]
        public List<Product> products => OrderProducts.Select(op => op.Product).ToList();

        public Order()
        {
            Id = Guid.NewGuid();
        }

        public Order(string clientName, string clientAddress, string clientPhone, Dictionary<Guid, int> productQuantities)
        {
            Id = Guid.NewGuid();
            ClientAddress = clientAddress;
            ClientName = clientName;
            ClientPhone = clientPhone;

            OrderStatus = OrderStatus.Created;
            RegisterDate = DateTime.UtcNow;

            foreach (var productQuantity in productQuantities)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = productQuantity.Key,
                    Quantity = productQuantity.Value
                };
                OrderProducts.Add(orderProduct);
            }
        }

        public static Order GenerateOrder(string clientName, string clientAddress, string clientPhone, Dictionary<Guid, int> productQuantities)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                ClientName = clientName,
                ClientAddress = clientAddress,
                ClientPhone = clientPhone,
                OrderStatus = OrderStatus.Created,
                RegisterDate = DateTime.UtcNow,
                OrderProducts = productQuantities.Select(pq => new OrderProduct
                {
                    ProductId = pq.Key,
                    Quantity = pq.Value
                }).ToList()
            };
        }
    }
}
