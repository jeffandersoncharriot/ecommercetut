using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerce.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
          

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            
            var productsProvider = new ProductsProvider(dbContext,null,mapper);

            var products = await productsProvider.GetProductsAsync();
            
            Assert.True(products.isSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.errorMessage);
        }
        
        [Fact]
        public async Task GetProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
          

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            
            var productsProvider = new ProductsProvider(dbContext,null,mapper);

            var product = await productsProvider.GetProductAsync(1);
            
            Assert.True(product.isSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.errorMessage);
        }
        
        [Fact]
        public async Task GetProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductsDbContext(options);
          

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            
            var productsProvider = new ProductsProvider(dbContext,null,mapper);

            var product = await productsProvider.GetProductAsync(-1);
            
            Assert.False(product.isSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.errorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i*3.14),

                });
            }

            dbContext.SaveChanges();
        }
    }
}