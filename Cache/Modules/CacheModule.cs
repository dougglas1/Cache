using Cache.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Cache.Modules
{
    public static class CacheModule
    {
        public static void AddDependencyInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDefaultConfig, DefaultConfig>();
        }
    }
}
