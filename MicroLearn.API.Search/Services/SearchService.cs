using MicroLearn.API.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroLearn.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IProductsService productsService;
        public SearchService(IProductsService _productsService)
        {
            this.productsService = _productsService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerid)
        {
            var products = await productsService.GetProductsAsync();

            if (products.IsSuccess)
            {
                return (true, products);
            }
            return (false, "Products info not available");
        }
    }
}
