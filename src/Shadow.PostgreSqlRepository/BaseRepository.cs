using Shadow.PostgreSqlRepository.Context;
using System;
using ZZH.DapperExpression.Service;

namespace Shadow.PostgreSqlRepository
{
    public abstract class BaseRepository : CasualDapperRepository<ShadowActiveDbContext>
    {
        /// <summary>
        /// 初始化一个新的<see cref="BaseRepository"/>实例
        /// </summary>
        /// <param name="context">DB 上下文</param>
        /// <param name="useTransaction">是否启用事务处理, 默认为 false</param>
        protected BaseRepository(ShadowActiveDbContext context, bool useTransaction = false) : base(context, useTransaction)
        {

        }
    }
}
