using Microsoft.Extensions.Logging;
using System;

namespace Shadow.Tool.Logger
{
    public class NLogLoggerOptions
    {
        /// <summary>
        /// 日志筛选器
        /// </summary>
        public Func<string, LogLevel, bool> Filter { get; set; }

        /// <summary>
        /// 输出消息格式化器，第一个参数 CategoryName, 第二个参数为 message
        /// </summary>
        public Func<string, string, string> TextFormatter { get; set; }
    }
}
