using Shadow.IRepository.Sample;
using Shadow.SqlServerRepository.Context;

namespace Shadow.SqlServerRepository.Sample
{
    /// <summary>
    /// SqlServer 仓储示例 demo
    /// </summary>
    public class ProductRepositorySample : BaseRepository, IProductRepositorySample
    {
        /// <summary>
        /// 初始化一个<see cref="ProductRepositorySample"/>对象，同时启动事务支持
        /// </summary>
        /// <param name="context"></param>
        public ProductRepositorySample(ShadowActiveDbContext context) : base(context)
        {
        }
    }
}
