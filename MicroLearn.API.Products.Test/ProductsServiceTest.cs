using AutoMapper;
using MicroLearn.API.Products.Db;
using MicroLearn.API.Products.Profiles;
using MicroLearn.API.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroLearn.API.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productProvider = new ProductsProvider(dbContext,null, mapper);

            var results = await productProvider.GetProductsAsync();

            Assert.True(results.IsSuccess);
            Assert.True(results.Products.Any());
            
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var results = await productProvider.GetProductAsync(1);

            Assert.True(results.IsSuccess);
            Assert.NotNull(results.Product);

        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var results = await productProvider.GetProductAsync(13);

            Assert.False(results.IsSuccess);
            Assert.Null(results.Product);

        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = i + 5
                });
            }

            dbContext.SaveChanges();
        }
    }


}
