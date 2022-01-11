using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Query
{
    class DeleteFactory : QueryAbstractFactory
    {
        public override IMySqlQuery CreateMySql(string tableName, DataRow row, DataRow newRow)
        {
            return new MySqlDelete(tableName, row);
        }

        public override IPostgresQuery CreatePostgres(string tableName, DataRow row, DataRow newRow)
        {
            return new PostgresDelete(tableName, row);
        }
    }
}
