using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shadow.Infrastructure.Runtime.Caching;
using Shadow.Tool.Redis;
using System;

namespace Shadow.Tool.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Redis 缓存服务
        /// </summary>
        /// <param name="cacheAction">缓存的过期参数配置</param>
        /// <returns></returns>
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration, Action<CacheExpireTimeConfig> cacheAction = null)
        {
            services.Configure<RedisOptions>(configuration.GetSection("Redis"));
            services.AddSingleton<IRedisProvider, RedisProvider>();
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
                return new RedisManualConfig
                {
                    ManualConnectionTimeout = options.ManualConnectionTimeout,
                };
            });

            services.AddRedis(cacheAction);
            return services;
        }

        public static IServiceCollection AddRedisSentinelCache(this IServiceCollection services, IConfiguration configuration, Action<CacheExpireTimeConfig> cacheAction = null)
        {
            services.Configure<RedisSentinelOptions>(configuration.GetSection("RedisSentinel"));
            services.AddSingleton<IRedisProvider, RedisSentinelProvider>();
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<RedisSentinelOptions>>().Value;
                return new RedisManualConfig
                {
                    ManualConnectionTimeout = options.ManualConnectionTimeout,
                };
            });

            services.AddRedis(cacheAction);
            return services;
        }

        private static void AddRedis(this IServiceCollection services, Action<CacheExpireTimeConfig> cacheAction = null)
        {
            services.AddSingleton(_ =>
            {
                var config = new CacheExpireTimeConfig();
                cacheAction?.Invoke(config);
                return config;
            });
            // 使用单例模式
            // 对于 Redis 哨兵模式，在初次连接哨兵集群时出现网络故障，后续网络恢复后依旧可继续使用，不用担心单例导致一直处于故障中
            services.AddSingleton(sp =>
            {
                var provider = sp.GetRequiredService<IRedisProvider>();
                return provider.CreateRedisClientsManager();
            });
            services.AddTransient<ICache, RedisCache>();
        }
    }
}
