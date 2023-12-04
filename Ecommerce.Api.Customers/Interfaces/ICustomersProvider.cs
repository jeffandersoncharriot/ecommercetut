using System.Collections.Generic;
using System.Threading.Tasks;


namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Models.Customer Customer, string errorMessage)> GetCustomerAsync(int id);
    }
}