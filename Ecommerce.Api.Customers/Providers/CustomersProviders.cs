using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Customer = Ecommerce.Api.Customers.Models.Customer;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var Customers = await dbContext.Customers.ToListAsync();
                if (Customers != null && Customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(Customers);
                    return (true, result, null);
                }
                return(false,null,"Not Found");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool isSuccess, Customer Customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var Customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (Customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(Customer);
                    return (true, result, null);
                }
                return(false,null,"Not Found");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 101, Name = "John", Address = "1234 rue Maple", });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "John Keyboard", Address = "123334 rue Maple", });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "John Monitor",Address = "123 rue Maple",});
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "John CPU",Address = "12 rue Maple",});
                dbContext.SaveChanges();
            }
        }
    }
}