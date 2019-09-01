using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shadow.Entity.Sample;
using Shadow.IRepository.Sample;
using Shadow.IService.Sample;

namespace Shadow.Service.Sample
{
    /// <summary>
    ///  Mongo DB 服务示例
    /// </summary>
    public class MongoProductServiceSample : IMongoProductServiceSample
    {
        private readonly IMongoProductRepositorySample _mongoProductRepository;

        public MongoProductServiceSample(IMongoProductRepositorySample mongoProductRepository)
        {
            _mongoProductRepository = mongoProductRepository;
        }

        public Task<List<MongoProduct>> GetAllProductsAsync()
        {
            return _mongoProductRepository.GetAllListAsync();
        }

        public Task<long> GetAllCountAsync()
        {
            return _mongoProductRepository.LongCountAsync();
        }

        public Task<MongoProduct> GetProductsAsync(Guid id)
        {
            return _mongoProductRepository.GetAsync(id);
        }

        public Task<MongoProduct> AddProductAsync(string no, string name, double weight)
        {
            return _mongoProductRepository.InsertAsync(new MongoProduct
            {
                NO = no,
                Name = name,
                Weight = weight,
                InBound = DateTime.Now,
            });
        }

        public Task AddProductsAsync(IEnumerable<MongoProduct> products)
        {
            return _mongoProductRepository.InsertManyAsync(products);
        }

        public Task RemoveProductAsync(Guid id)
        {
            return _mongoProductRepository.DeleteAsync(id);
        }
    }
}
