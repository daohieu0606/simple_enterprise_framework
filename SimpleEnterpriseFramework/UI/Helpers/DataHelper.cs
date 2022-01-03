namespace UI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using UI.Views;

    /// <summary>
    /// Defines the <see cref="DataHelper" />.
    /// </summary>
    public static class DataHelper
    {
        /// <summary>
        /// The ToDataTable.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="data">The data<see cref="IList{T}"/>.</param>
        /// <returns>The <see cref="DataTable"/>.</returns>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// The GetALLFields.
        /// </summary>
        /// <param name="data">The data<see cref="DataTable"/>.</param>
        /// <param name="row">The row<see cref="DataRowView"/>.</param>
        /// <returns>The <see cref="List{Field}"/>.</returns>
        public static List<Field> GetALLFields(DataTable data, DataRow row)
        {
            string[] columnNames = data.Columns.Cast<DataColumn>()
                             .Select(x => x.ColumnName)
                             .ToArray();
            List<Field> items = new List<Field>();
            for (var i = 0; i < columnNames.Length; i++)
            {
                Field field = new Field();
                field.Title = columnNames[i];
                field.IsNullable = true;
                field.IsPrimaryKey = false;
                field.DataType = data.Columns[i].DataType.ToString();
                if (row == null)
                    field.Value = "";
                else field.Value = row[field.Title].ToString(); 
                items.Add(field);
            }
            return items;
        }
    }
}
