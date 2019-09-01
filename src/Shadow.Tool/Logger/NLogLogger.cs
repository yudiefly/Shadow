using Microsoft.Extensions.Logging;
using System;

namespace Shadow.Tool.Logger
{
    /// <summary>
    /// Kafka 记录日志者
    /// </summary>
    internal class NLogLogger : ILogger
    {
        private readonly Func<string, LogLevel, bool> _filter;

        public string CategoryName { get; }

        /// <summary>
        /// 格式化输出的文本
        /// 第一个参数 CategoryName, 第二个参数为 message
        /// </summary>
        public Func<string, string, string> TextFormatter { get; set; }

        public NLogLogger(string categoryName, Func<string, LogLevel, bool> filter)
        {
            CategoryName = categoryName;
            _filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return Microsoft.Extensions.Logging.Abstractions.Internal.NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && ((_filter == null) || _filter(CategoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                string message = formatter(state, exception);
                if (!string.IsNullOrEmpty(message) || exception != null)
                {
                    // use NLog, 此处只负责调用 NLog， 是否抛出异常由 NLog 自身处理
                    if (NLog.LogManager.IsLoggingEnabled())
                    {
                        if (TextFormatter != null)
                        {
                            message = TextFormatter(CategoryName, message);
                        }

                        NLog.LogManager.GetLogger(CategoryName).Log(ConvertToNLogLevel(logLevel), exception, message);
                    }
                }
            }
        }

        private NLog.LogLevel ConvertToNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace: return NLog.LogLevel.Trace;
                case LogLevel.Debug: return NLog.LogLevel.Debug;
                case LogLevel.Information: return NLog.LogLevel.Info;
                case LogLevel.Warning: return NLog.LogLevel.Warn;
                case LogLevel.Error: return NLog.LogLevel.Error;
                case LogLevel.Critical: return NLog.LogLevel.Fatal;
                case LogLevel.None: return NLog.LogLevel.Off;
                default: return NLog.LogLevel.Off;
            }
        }
    }
}
