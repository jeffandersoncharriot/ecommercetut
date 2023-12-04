using System.Threading.Tasks;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products.Controllers
{
    /**
     * Product controller that can return one or all the products depending on the request. If no ID is specified, all the products are returned
     * The id parameter is optional and represents the product ID that is returned. All fields of the entity is returned.
     */
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();
            if (result.isSuccess)
            {
                return Ok(result.Products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound();
        }
    }
}