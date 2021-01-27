using LibraryApi.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class CachingController : ControllerBase
    {
        private readonly ICatalog _catalog;
        private readonly IOptions<MessagesOptions> _options;

        public CachingController(ICatalog catalog, IOptions<MessagesOptions> options)
        {
            _catalog = catalog;
            _options = options;
        }

        [HttpGet("caching/catalog")]
        public async Task<ActionResult> GetTheCatalog()
        {
            CatalogModel catalog = await _catalog.GetTheCatalog();
            return Ok(catalog);
        }

        [HttpGet("caching/info")]
        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any)]
        public ActionResult GetSomeInfo()
        {
            return Ok(new
            {
                Message = _options.Value.CacheMessage,
                CreatedAt = DateTime.Now
            });
        }
    }
}
