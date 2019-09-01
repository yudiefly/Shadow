using Shadow.Entity.Sample;
using System;
using System.Collections.Generic;
using System.Text;
using ZZH.DapperExpression.Service;

namespace Shadow.PostgreSqlRepository.EntityConfigMappers
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
