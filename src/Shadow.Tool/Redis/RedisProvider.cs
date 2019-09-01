using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Shadow.Tool.Redis
{
    public class RedisProvider : IRedisProvider
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly RedisOptions _options;

        public RedisProvider(IOptions<RedisOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _loggerFactory = loggerFactory;
        }

        public IRedisClientsManager CreateRedisClientsManager()
        {
            var redisConfig = new RedisPoolConfig();
            if (_options.MaxPoolSize.HasValue)
            {
                redisConfig.MaxPoolSize = _options.MaxPoolSize.Value;
            }

            return new RedisManagerPool(_options.Hosts, redisConfig);
        }
    }
}
