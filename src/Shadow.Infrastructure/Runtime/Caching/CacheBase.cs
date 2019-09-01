using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nito.AsyncEx;
using System;
using System.Threading.Tasks;

namespace Shadow.Infrastructure.Runtime.Caching
{
    /// <summary>
    /// 缓存基类，简单地事项了 <see cref="ICache"/> 接口.
    /// </summary>
    public abstract class CacheBase : ICache
    {
        public ILogger Logger { get; set; }

        public TimeSpan DefaultSlidingExpireTime { get; set; }

        public TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        protected readonly object SyncObj = new object();

        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor.
        /// 默认设置相对过期时间为 1h.
        /// </summary>
        protected CacheBase()
        {
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);

            Logger = NullLogger.Instance;
        }

        public virtual T Get<T>(string key, Func<string, T> factory)
        {
            T item = default(T);  // how to do struct ?
            
            try
            {
                item = GetOrDefault<T>(key);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.ToString());
            }

            if (item == null)
            {
                lock (SyncObj)
                {
                    try
                    {
                        item = GetOrDefault<T>(key);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, ex.ToString());
                    }

                    if (item == null)
                    {
                        item = factory(key);

                        if (item == null)
                        {
                            return item;
                        }

                        try
                        {
                            Set(key, item);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex, ex.ToString());
                        }
                    }
                }
            }

            return item;
        }

        public virtual async Task<T> GetAsync<T>(string key, Func<string, Task<T>> factory)
        {
            T item = default(T);

            try
            {
                item = await GetOrDefaultAsync<T>(key);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.ToString());
            }

            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    try
                    {
                        item = await GetOrDefaultAsync<T>(key);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, ex.ToString());
                    }

                    if (item == null)
                    {
                        item = await factory(key);

                        if (item == null)
                        {
                            return item;
                        }

                        try
                        {
                            await SetAsync(key, item);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError(ex, ex.ToString());
                        }
                    }
                }
            }

            return item;
        }

        public abstract T GetOrDefault<T>(string key);

        public virtual Task<T> GetOrDefaultAsync<T>(string key)
        {
            return Task.FromResult(GetOrDefault<T>(key));
        }

        public abstract void Set<T>(string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        public virtual Task SetAsync<T>(string key, T value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            Set(key, value, slidingExpireTime, absoluteExpireTime);
            return Task.FromResult(0);
        }

        public abstract void Remove(string key);

        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        public abstract void Clear();

        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        public virtual void Dispose()
        {

        }
    }
}
