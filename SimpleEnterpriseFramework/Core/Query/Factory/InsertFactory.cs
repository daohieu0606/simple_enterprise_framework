using System.Data;

namespace Core.Query
{
    class InsertFactory : QueryAbstractFactory
    {

        public override IMySqlQuery CreateMySql(string tableName, DataRow row, DataRow newRow)
        {
            return new MySqlInsert(tableName, row);
        }

        public override IPostgresQuery CreatePostgres(string tableName, DataRow row, DataRow newRow)
        {
            return new PostgresInsert(tableName, row);
        }
    }

}
