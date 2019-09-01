using System;
using System.Threading.Tasks;

namespace Shadow.Infrastructure.Runtime.Caching
{
    public static class CacheExtensions
    {
        /// <summary>
        /// 从缓存中获取实例。
        /// 若缓存中没有此实例，会用<paramref name="factory"/>方法来提供实例。会使用默认设置的的过期时间。
        /// 注：ok 表示是否成功获取到缓存对象(出现异常时ok值为false)；
        ///     value 表示获取到的缓存对象，若 ok 为 false，则 value 为 default(T)
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item</returns>
        public static (bool ok, T value) TryGetOrDefault<T>(this ICache cache, string key)
        {
            try
            {
                var value = cache.GetOrDefault<T>(key);
                return (true, value);
            }
            catch
            {
                return (false, default(T));
            }
        }

        /// <summary>
        /// 从缓存中获取实例。使用方法参考 <see cref="TryGetOrDefault{T}"/>
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item</returns>
        public static async Task<(bool ok, T value)> TryGetOrDefaultAsync<T>(this ICache cache, string key)
        {
            try
            {
                var value = await cache.GetOrDefaultAsync<T>(key);
                return (true, value);
            }
            catch
            {
                return (false, default(T));
            }
        }

        /// <summary>
        /// 通过 key 来保存或更新一个缓存实例。
        /// 使用方法参考 <see cref="ICache.Set{T}"/>
        /// </summary>
        /// <returns>true 表示成功写入；false 表示有出现异常</returns>
        public static bool TrySet<T>(this ICache cache, string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            try
            {
                cache.Set(key, value, slidingExpireTime, absoluteExpireTime);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通过 key 来保存或更新一个缓存实例。使用方法参考 <see cref="ICache.SetAsync{T}"/>
        /// </summary>
        /// <returns>true 表示成功写入；false 表示有出现异常</returns>
        public static Task<bool> TrySetAsync<T>(this ICache cache, string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            try
            {
                cache.SetAsync(key, value, slidingExpireTime, absoluteExpireTime);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
