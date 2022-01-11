using System;
using System.Data;
using System.Text;
using Npgsql;

namespace Core.Query
{
    class PostgresUpdate : IPostgresQuery
    {
        private readonly string tableName;
        private readonly DataRow row;
        private readonly DataRow newRow;

        public PostgresUpdate(string tableName, DataRow row, DataRow newRow)
        {
            this.tableName = tableName;
            this.row = row;
            this.newRow = newRow;
        }

        public NpgsqlCommand GetQuery()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            DataColumnCollection cols = row.Table.Columns;
            string paramsString = this.CreateParamsSetUpdateString(cols);

            command.CommandText = "update " + tableName + " set " + paramsString;

            for (int i = 0; i < cols.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, newRow[cols[i].ColumnName]);
            }
            for (int i = cols.Count; i < cols.Count * 2; i++)
            {
                command.Parameters.AddWithValue("@param" + i, row[cols[i - cols.Count].ColumnName]);
            }
            return command;
        }

        private string CreateParamsSetUpdateString(DataColumnCollection cols)
        {
            StringBuilder paramsString = new StringBuilder();
            if (cols.Count < 1)
                return "";
            for (int i = 0; i < cols.Count; ++i)
            {
                paramsString.Append(cols[i].ColumnName)
                    .Append(" = ")
                    .Append("@param")
                    .Append(i);
                if (i < cols.Count - 1)
                {
                    paramsString.Append(",");
                }
            }
            paramsString.Append(" where ");
            for (int i = 0; i < cols.Count; ++i)
            {
                paramsString.Append(cols[i].ColumnName)
                    .Append(" = ")
                    .Append("@param")
                    .Append(i + cols.Count);

                if (i < cols.Count - 1)
                {
                    paramsString.Append(" AND ");
                }
            }
            return paramsString.ToString();
        }
    }
}
