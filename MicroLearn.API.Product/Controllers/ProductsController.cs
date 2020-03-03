using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroLearn.API.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroLearn.API.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductsProvider _productsProvider, ILogger<ProductsController> _logger)
        {
            this.productsProvider = _productsProvider;
            this.logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsyn()
        {
            var result = await productsProvider.GetProductsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductAsyn(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}