using Microsoft.Extensions.DependencyInjection;
using Shadow.IRepository.Sample;
using Shadow.SqlServerRepository.Sample;

namespace Shadow.SqlServerRepository.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// SqlServer 仓储服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerRepository(this IServiceCollection services)
        {
            // 示例 demo
            services.AddTransient<IProductRepositorySample, ProductRepositorySample>();

            // add repositories

            return services;
        }
    }
}
