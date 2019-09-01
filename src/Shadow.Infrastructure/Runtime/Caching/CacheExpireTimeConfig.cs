using System;

namespace Shadow.Infrastructure.Runtime.Caching
{
    /// <summary>
    /// 缓存过期时间配置
    /// </summary>
    public class CacheExpireTimeConfig
    {
        /// <summary>
        /// 默认相对过期时间
        /// </summary>
        public TimeSpan? DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 默认绝对过期时间
        /// </summary>
        public TimeSpan? DefaultAbsoluteExpireTime { get; set; }
    }
}
