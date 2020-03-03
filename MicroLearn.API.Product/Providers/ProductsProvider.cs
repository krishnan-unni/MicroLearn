using AutoMapper;
using MicroLearn.API.Products.Db;
using MicroLearn.API.Products.Interfaces;
using MicroLearn.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroLearn.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext _dbContext, ILogger<ProductsProvider> _logger, IMapper _mapper)
        {
            this.dbContext = _dbContext;
            this.logger = _logger;
            this.mapper = _mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 1,
                    Name = "A",
                    Inventory = 10,
                    Price = 20
                });

                dbContext.Products.Add(new Db.Product()
                {
                    Id = 2,
                    Name = "B",
                    Inventory = 10,
                    Price = 30
                });

                dbContext.Products.Add(new Db.Product()
                {
                    Id = 3,
                    Name = "C",
                    Inventory = 10,
                    Price = 40
                });

                dbContext.Products.Add(new Db.Product()
                {
                    Id = 4,
                    Name = "D",
                    Inventory = 10,
                    Price = 50
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {

                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
