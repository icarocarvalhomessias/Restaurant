using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [NotMapped]
        public List<Guid> ProductIds;

        public List<Product> Products { get; set; } = new List<Product>();

        public decimal TotalValue { get; set; }


        public Order()
        {
        }

        public Order(string clientName, string clientAddress, string clientPhone, List<Guid> productIds)
        {
            ClientAddress = clientAddress;
            ClientName = clientName;
            ClientPhone = clientPhone;
            ProductIds = productIds;

            OrderStatus = OrderStatus.Created;
            RegisterDate = DateTime.UtcNow;
        }
    }
}
