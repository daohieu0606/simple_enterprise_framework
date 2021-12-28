using System.Data;

namespace Core.Query
{
    class UpdateFactory : QueryAbstractFactory
    {
        public override IMySqlQuery CreateMySql(string tableName, DataRow row, DataRow newRow)
        {
            return new MySqlUpdate(tableName, row, newRow);
        }

        public override IPostgresQuery CreatePostgres(string tableName, DataRow row, DataRow newRow)
        {
            return new PostgresUpdate(tableName, row, newRow);
        }
    }
}
