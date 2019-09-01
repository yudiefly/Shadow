using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shadow.Tool.Http;
using System;

namespace Shadow.Tool.ApplicationBuilder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用全局的 <see cref="HttpContextGlobal"/> 对象
        /// </summary>
        /// <exception cref="NotImplementedException">IHttpContextAccessor 未注入导致的异常</exception>
        public static IApplicationBuilder UseHttpContextGlobal(this IApplicationBuilder app)
        {
            HttpContextGlobal.ConfigureOnce(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            return app;
        }

        /// <summary>
        /// 在请求 Header 中写入 TraceId，在 UseMvc 方法之前调用。
        /// </summary>
        public static IApplicationBuilder UseToolTrace(this IApplicationBuilder app)
        {
            app.Use(next =>
            {
                return async (context) =>
                {
                    if (!context.Request.Headers.ContainsKey(Constants.RESTfulTraceId))
                    {
                        var traceId = Guid.NewGuid().ToString();
                        context.Request.Headers.Add(Constants.RESTfulTraceId, traceId);
                    }

                    if (!context.Items.ContainsKey(Constants.RESTfulTraceTime))
                    {
                        context.Items[Constants.RESTfulTraceTime] = DateTime.UtcNow; // for other call
                    }

                    await next(context);

                    // the last response ...
                };
            });

            return app;
        }
    }
}
