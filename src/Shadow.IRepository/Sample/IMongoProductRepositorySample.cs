using Shadow.Entity.Sample;
using ZZH.MongoDB.StandardService.Repositories;

namespace Shadow.IRepository.Sample
{
    /// <summary>
    /// Mongo 仓储接口示例
    /// </summary>
    public interface IMongoProductRepositorySample : IMongoDbRepository<MongoProduct>
    {

    }
}
