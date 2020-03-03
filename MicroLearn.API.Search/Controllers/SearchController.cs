using MicroLearn.API.Search.Interfaces;
using MicroLearn.API.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroLearn.API.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController:ControllerBase
    {
        private readonly ISearchService searchService;
        public SearchController(ISearchService _searchService)
        {
            this.searchService = _searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await searchService.SearchAsync(term.CustomerId);

            if (result.IsSuccess)
            {
                return Ok(result.SearchResults.Item2);
            }
            return NotFound();
        }

    }
}
