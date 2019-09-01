using Microsoft.Extensions.DependencyInjection;
using Shadow.IRepository.Sample;
using Shadow.PostgreSqlRepository.Sample;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadow.PostgreSqlRepository.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// PostgreSql 仓储服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPostgreSqlRepository(this IServiceCollection services)
        {
            // 示例 demo
            services.AddTransient<IProductRepositorySample, ProductRepositorySample>();

            // add repositories

            return services;
        }
    }
}
