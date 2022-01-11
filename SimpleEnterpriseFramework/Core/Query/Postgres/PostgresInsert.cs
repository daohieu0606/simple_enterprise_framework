using Npgsql;
using System;
using System.Data;
using System.Text;

namespace Core.Query
{
    class PostgresInsert : IPostgresQuery
    {
        private readonly string tableName;
        private readonly DataRow row;

        public PostgresInsert(string tableName, DataRow row)
        {
            this.tableName = tableName;
            this.row = row;
        }

        public NpgsqlCommand GetQuery()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            DataColumnCollection cols = row.Table.Columns;
            string paramsString = this.CreateParamsInsertString(cols);

            command.CommandText = "INSERT INTO " + tableName + " VALUES " + paramsString;
            Console.WriteLine(cols.Count);
            for (int i = 0; i < cols.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, row[cols[i].ColumnName]);
            }
            return command;
        }

        private string CreateParamsInsertString(DataColumnCollection cols)
        {
            StringBuilder paramsString = new StringBuilder();
            if (cols.Count < 1)
                return "";
            paramsString.Append("(");
            for (int i = 0; i < cols.Count; ++i)
            {
                paramsString.Append("@param").Append(i);
                if (i < cols.Count - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(")");
            return paramsString.ToString();
        }
    }
}