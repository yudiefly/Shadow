using System.Data.Common;
using ZZH.DapperExpression.Service.Data;

namespace Shadow.MySqlRepository.Context
{
    /// <summary>
    /// MySql DB 上下文对象
    /// </summary>
    public class ShadowActiveDbContext : ActiveDbContext
    {
        public override DbConnection CreateDbConnection(string connectionString)
        {
            return new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        }
    }
}
