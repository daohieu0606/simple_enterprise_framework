using System;
using System.Data;

namespace Core.Query
{
    abstract class QueryAbstractFactory
    {
        public abstract IMySqlQuery CreateMySql(String tableName, DataRow row, DataRow newRow);
        public abstract IPostgresQuery CreatePostgres( String tableName, DataRow row, DataRow newRow);
    }
}
