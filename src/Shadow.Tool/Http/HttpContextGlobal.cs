using Microsoft.AspNetCore.Http;

namespace Shadow.Tool.Http
{
    /// <summary>
    /// <see cref="HttpContext"/> 全局对象
    /// </summary>
    public static class HttpContextGlobal
    {
        static IHttpContextAccessor _httpContextAccessor;
        static bool _hasConfig;
        static readonly object _lock = new object();

        /// <summary>
        /// 配置 HttpContext 访问者对象, 仅能配置一次
        /// </summary>
        /// <param name="accessor"><see cref="IHttpContextAccessor"/></param>
        public static void ConfigureOnce(IHttpContextAccessor accessor)
        {
            if (!_hasConfig)
            {
                lock (_lock)
                {
                    if (!_hasConfig)
                    {
                        _httpContextAccessor = accessor;
                        _hasConfig = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前的 <see cref="HttpContext"/> 对象
        /// </summary>
        public static HttpContext Current => _httpContextAccessor?.HttpContext;

        /// <summary>
        /// 获取当前请求的 TraceID 标识
        /// </summary>
        public static string CurrentTraceId
        {
            get
            {
                if (Current != null)
                {
                    if (Current.Request.Headers.TryGetValue(Constants.RESTfulTraceId, out Microsoft.Extensions.Primitives.StringValues value))
                    {
                        return value.ToString();
                    }
                }

                return null;
            }
        }
    }
}
