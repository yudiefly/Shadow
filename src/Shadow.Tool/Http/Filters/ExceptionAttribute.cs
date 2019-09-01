using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Shadow.Tool.Http.Filters
{
    /// <summary>
    /// 异常筛选器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ExceptionAttribute>();
            logger.LogError(context.Exception, context.Exception.Message);

            ExceptionHandle(context);
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        public virtual void ExceptionHandle(ExceptionContext context)
        {
            context.Result = new JsonResult(new { action = context.ActionDescriptor.DisplayName, error = context.Exception.Message })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
