using System.Data.Common;
using ZZH.DapperExpression.Service.Data;

namespace Shadow.SqlServerRepository.Context
{
    /// <summary>
    /// SqlServer DB 上下文对象
    /// </summary>
    public class ShadowActiveDbContext : ActiveDbContext
    {
        public override DbConnection CreateDbConnection(string connectionString)
        {
            return new System.Data.SqlClient.SqlConnection(connectionString);            
        }
    }
}
