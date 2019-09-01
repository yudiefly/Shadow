using Microsoft.Extensions.Logging;
using ServiceStack.Redis;

namespace Shadow.Tool.Redis
{
    /// <summary>
    /// Redis 哨兵
    /// </summary>
    public class Sentinel
    {
        private readonly ILogger _logger;
        private readonly RedisSentinel _redisSentinel;

        public Sentinel(string[] sentinelHosts, string masterName, ILoggerFactory loggerFactory)
            : this(sentinelHosts, masterName, null, loggerFactory)
        {
        }

        public Sentinel(string[] sentinelHosts, string masterName, string filter, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Sentinel>();

            _redisSentinel = new RedisSentinel(sentinelHosts, masterName)
            {
                OnFailover = manager =>
                {
                    _logger.LogWarning("Redis Managers were Failed Over to new hosts");
                },
                OnWorkerError = ex =>
                {
                    _logger.LogWarning(ex, "Redis Worker error");
                },
            };

            if (!string.IsNullOrEmpty(filter))
            {
                _redisSentinel.HostFilter = host => $"{host}?{filter}";
            }
        }

        public IRedisClientsManager GetRedisClientsManager(int millisecondsTimeout)
        {
            if (millisecondsTimeout <= 0)
            {
                return _redisSentinel.Start();
            }

            var clientsManager = ConnectionUtil.Connect(() => _redisSentinel.Start(), millisecondsTimeout, _logger);
            return clientsManager;
        }
    }
}
