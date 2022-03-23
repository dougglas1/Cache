using Cache.Configurations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Cache.Extensions
{
    public static class MemoryOptionsExtensions
    {
        public static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(this IDefaultConfig config) =>
            new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddHours(config.GetDefaultConfig().CacheExpirationTimeHours)
            };

        public static DistributedCacheEntryOptions GetDistributedCacheEntryOptions(this IDefaultConfig config) =>
            new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(config.GetDefaultConfig().CacheExpirationTimeHours));
    }
}
