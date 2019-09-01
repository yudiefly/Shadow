
using Shadow.Entity.Sample;
using Shadow.IRepository.Sample;
using ZZH.MongoDB.StandardService.MongoDb;
using ZZH.MongoDB.StandardService.Repositories;

namespace Shadow.MongoDbRepository.Sample
{
    /// <summary>
    /// Mongo DB 仓储示例
    /// </summary>
    public class MongoProductRepositorySample : MongoDbRepositoryBase<MongoProduct>, IMongoProductRepositorySample
    {
        public MongoProductRepositorySample(IMongoDatabaseProvider provider) : base(provider)
        {

        }
    }
}
