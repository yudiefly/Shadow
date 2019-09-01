using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;

namespace Shadow.Tool.Redis
{
    public class RedisSentinelProvider : IRedisProvider
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly RedisSentinelOptions _options;

        public RedisSentinelProvider(IOptions<RedisSentinelOptions> options, ILoggerFactory loggerFactory)
        {
            _options = options.Value;
            _loggerFactory = loggerFactory;
        }

        public IRedisClientsManager CreateRedisClientsManager()
        {
            var sentinel = new Sentinel(_options.Hosts, _options.MasterName, _options.HostFilter, _loggerFactory);
            return sentinel.GetRedisClientsManager(_options.ManualConnectionTimeout.GetValueOrDefault());
        }
    }
}
