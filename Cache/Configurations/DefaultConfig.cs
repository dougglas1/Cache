using Microsoft.Extensions.Configuration;

namespace Cache.Configurations
{
    public class DefaultConfig : IDefaultConfig
    {
        private readonly IConfiguration _configuration;
        
        public DefaultConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DefaultOptions GetDefaultConfig()
        {
            var options = new DefaultOptions();
            _configuration.GetSection(DefaultOptions.Default).Bind(options);

            return options;
        }
    }
}
