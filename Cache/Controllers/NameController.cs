using Cache.Configurations;
using Cache.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IDefaultConfig _config;

        public NameController(IDefaultConfig config)
        {
            _config = config;
        }

        [HttpGet, Route("MemoryCache")]
        public IActionResult GetNames([FromServices] IMemoryCache memoryCache, string name)
        {
            var cacheKey = name.ToUpper();

            if (memoryCache.TryGetValue(cacheKey, out IEnumerable<string> names))
                return Ok(names);

            names = GetNames(name);

            memoryCache.Set(cacheKey, names, _config.GetMemoryCacheEntryOptions());

            return Ok(names);
        }

        [HttpGet, Route("DistributedCache")]
        public async Task<IActionResult> GetNames([FromServices] IDistributedCache distributedCache, string name)
        {
            var cacheKey = name.ToUpper();

            IEnumerable<string> names;
            string serializedNames;

            var encodedNames = await distributedCache.GetAsync(cacheKey);

            if (encodedNames != null)
            {
                serializedNames = Encoding.UTF8.GetString(encodedNames);
                names = JsonConvert.DeserializeObject<List<string>>(serializedNames);
                return Ok(names);
            }

            names = GetNames(name);

            serializedNames = JsonConvert.SerializeObject(names);
            encodedNames = Encoding.UTF8.GetBytes(serializedNames);
            
            await distributedCache.SetAsync(cacheKey, encodedNames, _config.GetDistributedCacheEntryOptions());
            return Ok(names);
        }
        
        private IEnumerable<string> GetNames(string name)
        {
            return new List<string> { "João Pedro", "Paulo da Silva", "Paulo Figueiredo" }
                .Where(x => x.Contains(name));
        }
    }
}
