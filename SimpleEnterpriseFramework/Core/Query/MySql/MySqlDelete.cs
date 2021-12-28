using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Core.Query
{
    class MySqlDelete: IMySqlQuery
    {
        private readonly string tableName;
        private readonly DataRow row;

        public MySqlDelete(string tableName, DataRow row)
        {
            this.tableName = tableName;
            this.row = row;
        }

        public MySqlCommand GetQuery()
        {
            MySqlCommand command = new MySqlCommand();
            DataColumnCollection cols = row.Table.Columns;
            command.CommandText = "delete from " + tableName + " where " + this.CreateParamsDeleteString(cols);
            for (int i = 0; i < cols.Count; i++)
            {
                command.Parameters.AddWithValue("@param" + i, row[cols[i].ColumnName]);
            }
            return command;
        }

        private string CreateParamsDeleteString(DataColumnCollection cols)
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
                    paramsString.Append(" AND ");
                }
            }
            return paramsString.ToString();
        }
    }
}

