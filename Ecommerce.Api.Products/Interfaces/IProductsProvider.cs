using System.Collections.Generic;
using System.Threading.Tasks;


namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Product> Products, string errorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Models.Product Product, string errorMessage)> GetProductAsync(int id);
    }
}