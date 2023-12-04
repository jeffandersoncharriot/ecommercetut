using System.Threading.Tasks;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    /**
     * Customer controller that can return one or all the customers depending on the request. If no ID is specified, all the customers are returned.
     * The id parameter is optional and represents the customer ID that is returned. All fields of the entity is returned.
     */
    [ApiController]
    [Route("api/customers")]
    public class ProductsController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public ProductsController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if (result.isSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }
    }
}