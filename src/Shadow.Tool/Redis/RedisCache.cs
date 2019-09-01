using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using Shadow.Infrastructure.Runtime.Caching;
using System;
using System.Threading;

namespace Shadow.Tool.Redis
{
    /// <summary>
    /// Redis 缓存
    /// </summary>
    public class RedisCache : CacheBase
    {
        private readonly IRedisClientsManager _redisClientsManager;

        private IRedisClient _redisClient;
        private bool _hasConnected;

        /// <summary>
        /// 手动地设置的连接服务器超时时长，milliseconds
        /// 若设置该值会覆盖 host 中配置的时长或默认时长(20s)。若设置的超时时长超过组件配置时长或默认时长，则不会生效。
        /// </summary>
        public int? ManualConnectionTimeout { get; }

        public RedisCache(IRedisClientsManager redisClientsManager, CacheExpireTimeConfig cacheExpireTimeConfig, RedisManualConfig redisManualConfig, ILoggerFactory loggerFactory)
        {
            _redisClientsManager = redisClientsManager;
            if (cacheExpireTimeConfig != null)
            {
                if (cacheExpireTimeConfig.DefaultSlidingExpireTime.HasValue)
                {
                    DefaultSlidingExpireTime = cacheExpireTimeConfig.DefaultSlidingExpireTime.Value;
                }
                if (cacheExpireTimeConfig.DefaultAbsoluteExpireTime.HasValue)
                {
                    DefaultAbsoluteExpireTime = cacheExpireTimeConfig.DefaultAbsoluteExpireTime.Value;
                }
            }
            ManualConnectionTimeout = redisManualConfig.ManualConnectionTimeout;
            Logger = loggerFactory.CreateLogger<RedisCache>();
        }

        public override T GetOrDefault<T>(string key)
        {
            EnsureConnectRedis();

            return _redisClient.Get<T>(key);
        }

        public override void Set<T>(string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            EnsureConnectRedis();

            var expiresIn = absoluteExpireTime ?? slidingExpireTime ?? DefaultAbsoluteExpireTime ?? DefaultSlidingExpireTime;
            _redisClient.Set(key, value, expiresIn);
        }

        public override void Remove(string key)
        {
            EnsureConnectRedis();

            _redisClient.Remove(key);
        }

        public override void Clear()
        {
            EnsureConnectRedis();

            var keys =  _redisClient.GetAllKeys();
            if (keys.Count > 0)
            {
                _redisClient.RemoveAll(keys);
            }
        }

        public override void Dispose()
        {
            _redisClient?.Dispose();
            _hasConnected = false;
        }

        private void EnsureConnectRedis()
        {
            if (!_hasConnected) // single thread
            {
                if (ManualConnectionTimeout != null)
                {
                    ConnectRedisServer(ManualConnectionTimeout.Value);
                }
                else
                {
                    ConnectRedisServerOrigin();
                }

                _hasConnected = true;  // set ture when connect successfully.
            }
        }

        private void ConnectRedisServer(int millisecondsTimeout, CancellationToken cancellationToken = default(CancellationToken))
        {
            _redisClient = ConnectionUtil.Connect(() => _redisClientsManager.GetClient(), millisecondsTimeout, Logger, cancellationToken);
        }

        private void ConnectRedisServerOrigin()
        {
            try
            {
                _redisClient = _redisClientsManager.GetClient();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Connect the redis server error.");
                throw;
            }
        }
    }
}
