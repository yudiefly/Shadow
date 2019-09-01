using System;
using System.Threading.Tasks;

namespace Shadow.Infrastructure.Runtime.Caching
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 默认的缓存滑动过期时间, 默认为 1h
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 默认的缓存绝对过期时间
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// 从缓存中获取实例。
        /// 若缓存中没有此实例，会用<paramref name="factory"/>方法来提供实例。会使用默认设置的的过期时间。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">若不存在实例，会用此方法创建</param>
        /// <returns>Cached item</returns>
        T Get<T>(string key, Func<string, T> factory);

        /// <summary>
        /// 从缓存中获取实例。
        /// 若缓存中没有此实例，会用<paramref name="factory"/>方法来提供实例。会使用默认设置的的过期时间。
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">若不存在实例，会用此方法创建</param>
        /// <returns>Cached item</returns>
        Task<T> GetAsync<T>(string key, Func<string, Task<T>> factory);

        /// <summary>
        /// 获取缓存实例，若不存在则返回 null。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        T GetOrDefault<T>(string key);

        /// <summary>
        /// 获取缓存实例，若不存在则返回 null。
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found</returns>
        Task<T> GetOrDefaultAsync<T>(string key);

        /// <summary>
        /// 通过 key 来保存或更新一个缓存实例。
        /// 使用 (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>) 中的一个过期时间。
        /// 若两者都没指定，那么
        /// <see cref="DefaultAbsoluteExpireTime"/> 不为 null 时将使用该值. 否则使用 <see cref="DefaultSlidingExpireTime"/>
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        /// <param name="absoluteExpireTime">Absolute expire time</param>
        void Set<T>(string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 通过 key 来保存或更新一个缓存实例。
        /// 使用 (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>) 中的一个过期时间。
        /// 若两者都没指定，那么
        /// <see cref="DefaultAbsoluteExpireTime"/> 不为 null 时将使用该值. 否则使用 <see cref="DefaultSlidingExpireTime"/>
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        /// <param name="absoluteExpireTime">Absolute expire time</param>
        Task SetAsync<T>(string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// 通过 key 移除缓存实例，若指定的 key 不在缓存中，则不做任何处理.
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(string key);

        /// <summary>
        /// 通过 key 移除缓存实例，若指定的 key 不在缓存中，则不做任何处理.
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 清除该缓存中所有的实例。
        /// </summary>
        void Clear();

        /// <summary>
        /// 清除该缓存中所有的实例。
        /// </summary>
        Task ClearAsync();
    }
}
