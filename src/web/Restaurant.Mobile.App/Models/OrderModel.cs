using System;
using System.Collections.Generic;

namespace Restaurant.Mobile.App.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime RegisterDate { get; set; }

        public List<ProductModel> Products { get; set; }

        public decimal TotalValue { get; set; }

        public OrderModel()
        {
            Products = new List<ProductModel>();
        }

        public string StatusDescription => OrderStatus switch
        {
            OrderStatus.Created => "Criado",
            OrderStatus.InProgress => "Em andamento",
            OrderStatus.Delivered => "Entregue",
            OrderStatus.Canceled => "Cancelado",
            _ => "Desconhecido"
        };
    }

    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }

    public enum OrderStatus
    {
        Created = 1,
        InProgress = 2,
        Delivered = 3,
        Canceled = 4
    }
}
