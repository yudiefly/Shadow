using Microsoft.Extensions.Logging;
using System;

namespace Shadow.Tool.Logger
{
    /// <summary>
    /// 日志提供者，用于创建 <see cref="ILogger"/> 对象
    /// </summary>
    internal class NLogLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;

        public Func<string, string, string> TextFormatter { get; set; }

        public NLogLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new NLogLogger(categoryName, _filter) { TextFormatter = TextFormatter };
        }

        public void Dispose()
        {
            
        }
    }
}
