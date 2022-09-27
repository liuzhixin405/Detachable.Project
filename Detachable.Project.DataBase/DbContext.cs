using Detachable.Project.Core.Extensions;
using SqlSugar;

namespace Detachable.Project.DataBase
{
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigExtensions.Configuration["DbConnection:MySqlConnectionString"],
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string s = sql;
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };//调式代码 打印SQL 
        }

        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
    }
}