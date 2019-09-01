using Microsoft.Extensions.DependencyInjection;

namespace Shadow.Tool.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册工具服务
        /// </summary>
        public static IServiceCollection AddTool(this IServiceCollection services)
        {
            return services;
        }
    }
}
