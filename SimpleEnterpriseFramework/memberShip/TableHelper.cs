using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MemberShip
{
    class TableHelper
    {
        public static DataTable addColumn(DataTable table, string name, string type)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType(type);
            column.ColumnName = name;
            table.Columns.Add(column);
            return table;
        }
    }
}
