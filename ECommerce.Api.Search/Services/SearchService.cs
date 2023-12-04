using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductService _productsService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrdersService ordersService, IProductService productService,
            ICustomerService customerService)
        {
            _ordersService = ordersService;
            _productsService = productService;
            _customerService = customerService;
        }

        public async Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);
            var productsResult = await _productsService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomerAsync(customerId);

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                foreach (var item in order.Items)
                    item.ProductName = productsResult.IsSuccess
                        ? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                        : "Products information not available";
                var result = new
                {
                    Customer = customerResult.IsSuccess
                        ? customerResult.Customer
                        : new { Name = "Customer information not found" },
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}