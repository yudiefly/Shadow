using Microsoft.Extensions.Logging;
using System;

namespace Shadow.Tool.Logger
{
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// 添加默认的 NLog 日志， 其中会排除 "Microsoft" 类别的日志，输出采用 <see cref="TextLoggerFormatter.DefaultLogMessageFormatter"/> 格式化
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddDefaultNLog(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog(options => 
            {
                options.Filter = (name, logLevel) => logLevel >= LogLevel.Trace && !name.StartsWith("Microsoft");
                options.TextFormatter = TextLoggerFormatter.DefaultLogMessageFormatter;
            });

            return loggerFactory;
        }

        public static ILoggerFactory AddDefaultNLog(this ILoggerFactory loggerFactory, Action<TextLoggerPropertyOptions> action)
        {
            loggerFactory.AddNLog(options =>
            {
                options.Filter = (name, logLevel) => logLevel >= LogLevel.Trace && !name.StartsWith("Microsoft");

                var formatter = new TextLoggerFormatter
                {
                    TextPropertyOptions = new TextLoggerPropertyOptions()
                };
                action?.Invoke(formatter.TextPropertyOptions);
                options.TextFormatter = formatter.LogMessageFormatter;
            });

            return loggerFactory;
        }

        /// <summary>
        /// 添加默认的 NLog 日志
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="options">options</param>
        /// <returns></returns>
        public static ILoggerFactory AddNLog(this ILoggerFactory loggerFactory, Action<NLogLoggerOptions> action)
        {
            var options = new NLogLoggerOptions();
            action?.Invoke(options);

            loggerFactory.AddProvider(new NLogLoggerProvider(options.Filter) { TextFormatter = options.TextFormatter });
            return loggerFactory;
        }
    }
}
