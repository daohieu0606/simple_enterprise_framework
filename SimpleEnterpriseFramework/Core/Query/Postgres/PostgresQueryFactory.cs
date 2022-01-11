using System;
using System.Data;

namespace Core.Query
{
    class PostgresQueryFactory
    {
        public static IPostgresQuery MakeQuery(string type, String tableName, DataRow row, DataRow newRow)
        {
            switch (type)
            {
                case "insert":
                    return new PostgresInsert(tableName, row);

                case "delete":
                    return new PostgresDelete(tableName, row);

                case "update":
                    return new PostgresUpdate(tableName, row, newRow);
                default:
                    return null;
            }
        }
    }
}
