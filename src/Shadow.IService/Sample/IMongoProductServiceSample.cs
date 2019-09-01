using Shadow.Entity.Sample;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shadow.IService.Sample
{
    /// <summary>
    /// Mongo DB 服务接口示例
    /// </summary>
    public interface IMongoProductServiceSample
    {
        Task<List<MongoProduct>> GetAllProductsAsync();

        Task<long> GetAllCountAsync();

        Task<MongoProduct> GetProductsAsync(Guid id);

        Task<MongoProduct> AddProductAsync(string no, string name, double weight);

        Task AddProductsAsync(IEnumerable<MongoProduct> products);

        Task RemoveProductAsync(Guid id);
    }
}
