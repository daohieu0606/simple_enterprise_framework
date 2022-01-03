using System;
using System.Data;

namespace Core.Query
{
    class MySqlQueryFactory
    {
        public static IMySqlQuery MakeQuery(string type, String tableName, DataRow row, DataRow newRow)
        {
            switch (type)
            {
                case "insert":
                    return new MySqlInsert(tableName, row);

                case "delete":
                    return new MySqlDelete(tableName, row);

                case "update":
                    return new MySqlUpdate(tableName, row, newRow);
                default:
                    return null;
            }
        }
    }
}
