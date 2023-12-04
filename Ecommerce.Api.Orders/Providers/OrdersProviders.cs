using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order = Ecommerce.Api.Orders.Db.Order;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        

        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Items).ToListAsync();
                if (orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                var order1 = new Order
                {
                    Id = 1,
                    CustomerId = 101,
                    OrderDate = DateTime.Now,
                    Total = 50,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 15 },
                        new OrderItem { ProductId = 2, Quantity = 1, UnitPrice = 35 }
                    }
                };

                var order2 = new Order
                {
                    Id = 2,
                    CustomerId = 102,
                    OrderDate = DateTime.Now,
                    Total = 120,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 3, Quantity = 1, UnitPrice = 100 },
                        new OrderItem { ProductId = 4, Quantity = 1, UnitPrice = 20 }
                    }
                };

                var order3 = new Order
                {
                    Id = 3,
                    CustomerId = 103,
                    OrderDate = DateTime.Now,
                    Total = 200,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 5, Quantity = 1, UnitPrice = 50 },
                        new OrderItem { ProductId = 6, Quantity = 1, UnitPrice = 150 }
                    }
                };

                dbContext.Orders.AddRange(order1, order2, order3);
                dbContext.SaveChanges();
            }
        }

    }
}