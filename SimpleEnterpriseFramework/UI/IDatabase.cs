using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UI.Views;

namespace UI
{
    public class IDatabase
    {
        public IDatabase(string dbType, string host, string name, string pwd)
        {

        }
        public bool Connect()
        {
            return true;
        }

        public DataTable GetData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(string));
            table.Columns.Add("CreatedAt", typeof(DateTime));

            table.Rows.Add(25, "Nguyen Van A", 30, DateTime.Now);
            table.Rows.Add(50, "Tran Van B", 40, DateTime.Now);
            table.Rows.Add(10, "Le Van C", 50, DateTime.Now);
            table.Rows.Add(21, "Nguyen Hoang D", 60, DateTime.Now);
            return table;
        }

        public List<string> GetAllTableNames(){
            return new List<string> { "Bảng 1", "Bảng 2", "Bảng 3" };
        }

        public List<string> GetAllDBNames()
        {
            return new List<string> { "Database 1", "Database 2", "Database 3" };
        }

        public List<Field> GetAllFields()
        {
            List<Field> items = new List<Field>();
            items.Add(new Field() { Title = "Id", IsNullable = true, IsPrimaryKey = true });
            items.Add(new Field() { Title = "Name", IsNullable = false, IsPrimaryKey = false });
            items.Add(new Field() { Title = "Age", IsNullable = true, IsPrimaryKey = false });
            items.Add(new Field() { Title = "CreatedAt", IsNullable = true, IsPrimaryKey = false });
            return items;
        }
    }
}
