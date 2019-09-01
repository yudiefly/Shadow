using System;
using Microsoft.AspNetCore.Builder;

namespace Shadow.Tool.Http
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// 默认的异常处理选项
        /// </summary>
        public static ExceptionHandlerOptions Default => new ExceptionHandlerOptions
        {
            ExceptionHandler = context =>
            {
                return System.Threading.Tasks.Task.CompletedTask;
            },
        };
    }
}
