using Microsoft.Extensions.DependencyInjection;
using Shadow.IService.Sample;
using Shadow.Service.Sample;

namespace Shadow.Service.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册应用层的应用服务
        /// </summary>
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // sample
            services.AddTransient<IProductInboundServiceSample, ProductInboundServiceSample>();
            services.AddTransient<IProduct2InboundServiceSample, Product2InboundServiceSample>();
            services.AddTransient<IMongoProductServiceSample, MongoProductServiceSample>();

            return services;
        }
    }
}
