using Microsoft.Extensions.DependencyInjection;
using Shadow.IRepository.Sample;
using Shadow.MongoDbRepository.Sample;

namespace Shadow.MongoDbRepository.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// MongoDB 仓储服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDbRepository(this IServiceCollection services)
        {
            // 示例 demo
            services.AddTransient<IMongoProductRepositorySample, MongoProductRepositorySample>();

            // add repositories

            return services;
        }
    }
}
