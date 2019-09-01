using System;

namespace Shadow.Infrastructure.Utils
{
    /// <summary>
    /// <see cref="DateTime"/>的工具类
    /// </summary>
    public static class TimeUtil
    {
        /// <summary>
        /// The unix UTC Ticks.
        /// 1970-01-01 00:00:00 的时间刻度
        /// </summary>
        public const long UnixUTCTicks = 621355968000000000;

        /// <summary>
        /// 获取本地当前时间的 Unix 时间戳
        /// 注：会将本地时间转换为 UTC 时间
        /// </summary>
        /// <returns>The UTCT imestamp.</returns>
        public static long UnixTimestamp()
        {
            return UnixTimestamp(DateTime.UtcNow);
        }

        /// <summary>
        /// 获取指定时间的 Unix 时间戳（使用的是 UTC 时间） 
        /// </summary>
        /// <param name="utcTime">UTC 时间, 注：此时间必须为 UTC 时间</param>
        /// <returns></returns>
        public static long UnixTimestamp(DateTime utcTime)
        {
            return (utcTime.Ticks - UnixUTCTicks) / 10000000;
        }

        /// <summary>
        /// 获取本地当前时间的 Unix 时间戳，精确到毫秒。
        /// 注：会将本地时间转换为 UTC 时间
        /// </summary>
        /// <returns></returns>
        public static long UnixTimestampMillisecond()
        {
            return UnixTimestampMillisecond(DateTime.UtcNow);
        }

        /// <summary>
        /// 获取指定时间的 Unix 时间戳，精确到毫秒
        /// </summary>
        /// <param name="utcTime">UTC 时间，注：此处是 UTC 时间</param>
        /// <returns></returns>
        public static long UnixTimestampMillisecond(DateTime utcTime)
        {
            return (utcTime.Ticks - UnixUTCTicks) / 10000;
        }
    }
}
