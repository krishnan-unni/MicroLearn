using MicroLearn.API.Search.Interfaces;
using MicroLearn.API.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroLearn.API.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory _httpClientFactory, ILogger<ProductsService> _logger)
        {
            httpClientFactory = _httpClientFactory;
            logger = _logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
