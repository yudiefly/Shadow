using Microsoft.Extensions.DependencyInjection;
using Shadow.IRepository.Sample;
using Shadow.MySqlRepository.Sample;

namespace Shadow.MySqlRepository.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// MySql 仓储服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMySqlRepository(this IServiceCollection services)
        {
            // 示例 demo
            services.AddTransient<IProduct2RepositorySample, Product2RepositorySample>();

            // add repositories

            return services;
        }
    }
}
