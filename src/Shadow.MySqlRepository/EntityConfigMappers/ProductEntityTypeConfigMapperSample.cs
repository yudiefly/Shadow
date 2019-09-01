using Shadow.Entity.Sample;
using ZZH.DapperExpression.Service;

namespace Shadow.MySqlRepository.EntityConfigMappers
{
    /// <summary>
    /// Produce 实体配置，用于示例
    /// </summary>
    internal class ProductEntityTypeConfigMapperSample : EntityTypeConfigMapper<Product2>
    {
        public ProductEntityTypeConfigMapperSample()
        {
            Table("product");
        }
    }
}
