using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using ZZH.DapperExpression.Service.Data;

namespace Shadow.PostgreSqlRepository.Context
{
    /// <summary>
    /// PostgreSql Db上下文对象
    /// </summary>
    public class ShadowActiveDbContext : ActiveDbContext
    {
        public override DbConnection CreateDbConnection(string connectionString)
        {
            return new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
