using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shadow.Infrastructure.Extensions;
using System;
using System.Reflection;

namespace Shadow.Tool.Http.Filters
{
    /// <summary>
    /// 日志记录，用于记录 Action 执行前输入和执行后输出的数据
    /// </summary>
    [AttributeUsage((AttributeTargets.Method | AttributeTargets.Class), AllowMultiple = true)]
    public class LogAttribute : ActionFilterAttribute
    {
        private readonly DateTime _startTime = DateTime.Now;

        /// <summary>
        /// 是否记录输出的日志, 默认为 true.
        /// </summary>
        public bool IsLogOutPut { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller.GetType().GetCustomAttribute<NoLogAttribute>() != null)
            {
                return;
            }

            if (context.ActionArguments != null)
            {
                var logger = GetLogger(context.HttpContext);

                // think: how to exclude the "IFormFile" argument.
                logger.LogInformation(context.ActionArguments.ToJson());
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller.GetType().GetCustomAttribute<NoLogAttribute>() != null)
            {
                return;
            }

            if (IsLogOutPut)
            {
                var logger = GetLogger(context.HttpContext);

                var timeSpan = (long)(DateTime.Now - _startTime).TotalMilliseconds / 1000.0;
                var requestRoute = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString.ToString();

                if (context.Result is JsonResult jsonResult)
                {
                    logger.LogInformation(new { ElapsedTime = timeSpan, jsonResult.Value }.ToJson());
                }
                else if (context.Result is ObjectResult objectResult)
                {
                    logger.LogInformation(new { ElapsedTime = timeSpan, objectResult.Value }.ToJson());
                }
                else if (context.Result is StatusCodeResult okResult)
                {
                    logger.LogInformation(new { ElapsedTime = timeSpan, okResult.StatusCode }.ToJson());
                }
                else if (context.Result is EmptyResult)
                {
                    logger.LogInformation(new { ElapsedTime = timeSpan }.ToJson());
                }
            }

            base.OnActionExecuted(context);
        }

        private ILogger GetLogger(HttpContext context)
        {
            var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger<LogAttribute>();
        }
    }
}
