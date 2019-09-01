using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;

namespace Shadow.Tool.Diagnostics
{
    /// <summary>
    /// 记录 ExceptionHandlerMiddleware 中间件中的诊断信息
    /// </summary>
    public class ExceptionHandlerDiagnostic
    {
        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.HandledException")]
        public virtual void Write(HttpContext httpContext, Exception exception)
        {
            var loggerFactory = httpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ExceptionHandlerDiagnostic>();
            logger.LogError(exception, exception.Message);
        }
    }
}
