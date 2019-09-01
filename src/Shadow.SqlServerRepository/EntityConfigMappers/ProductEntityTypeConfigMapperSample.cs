using Shadow.Entity.Sample;
using ZZH.DapperExpression.Service;

namespace Shadow.SqlServerRepository.EntityConfigMappers
{
    /// <summary>
    /// Produce 实体配置，用于示例
    /// </summary>
    internal class ProductEntityTypeConfigMapperSample : EntityTypeConfigMapper<Product>
    {
        public ProductEntityTypeConfigMapperSample()
        {
            Table("Product");
        }
    }
}
