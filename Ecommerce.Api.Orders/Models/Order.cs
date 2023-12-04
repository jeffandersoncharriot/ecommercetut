using System;
using System.Collections.Generic;

namespace Ecommerce.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<Db.OrderItem> Items { get; set; }
    }
}