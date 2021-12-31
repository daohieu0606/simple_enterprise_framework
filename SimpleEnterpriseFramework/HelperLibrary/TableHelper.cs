using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelperLibrary
{
    public class TableHelper
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
        public static void printDataRow(DataRow dr)
        {
            foreach (var item in dr.ItemArray)
            {
                Console.WriteLine(item);
            }
        }
    }
}