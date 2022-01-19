﻿using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Core.Query
{
    class MySqlUpdate : IMySqlQuery
    {
        private readonly string tableName;
        private readonly DataRow row;
        private readonly DataRow newRow;

        public MySqlUpdate(string tableName, DataRow row, DataRow newRow)
        {
            this.tableName = tableName;
            this.row = row;
            this.newRow = newRow;
        }

        public MySqlCommand GetQuery()
        {
            MySqlCommand command = new MySqlCommand();
            DataColumnCollection cols = row.Table.Columns;
            string paramsString = this.CreateParamsSetUpdateString(cols);

            command.CommandText = "update " + tableName + " set " + paramsString;

            for (int i = 0; i < cols.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, newRow[cols[i].ColumnName]);
            }
            for (int i = cols.Count; i < cols.Count * 2; i++)
            {
                if (row[cols[i - cols.Count].ColumnName] != null)
                {
                    command.Parameters.AddWithValue("@param" + i, row[cols[i - cols.Count].ColumnName]);
                }
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
                if (row[cols[i].ColumnName] != null)
                {
                    if (i > 0)
                    {
                        paramsString.Append(" AND ");
                    }

                    paramsString.Append(cols[i].ColumnName)
                        .Append(" = ")
                        .Append("@param")
                        .Append(i + cols.Count);
                }
            }
            return paramsString.ToString();
        }
    }
}
