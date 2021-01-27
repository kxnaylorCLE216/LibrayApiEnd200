using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class RedisCatalog : ICatalog
    {
        private readonly IDistributedCache _cache;

        public RedisCatalog(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<CatalogModel> GetTheCatalog()
        {
            //Check to see if it is in the cache and still valid.
            var catalog = await _cache.GetAsync("catalog");
            if (catalog != null)
            {
                // if it is there, return it.
                var storedCatalog = Encoding.UTF8.GetString(catalog);
                var response = JsonSerializer.Deserialize<CatalogModel>(storedCatalog);
                return response;
            }
            else
            {
                // if it is NOT there
                //   - create the thing new again.
                var catalogToSave = new CatalogModel
                {
                    CreatedAt = DateTime.Now,
                    Items = new List<string> { "Beer", "Chips", "Pizza" }
                };
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));
                var serializedCatalog = JsonSerializer.Serialize(catalogToSave);
                var encodedCatalog = Encoding.UTF8.GetBytes(serializedCatalog);
                //   - put it in the cache
                await _cache.SetAsync("catalog", encodedCatalog, options);
                return catalogToSave;
                //   - return that.
            }
           
        }
    }
}
