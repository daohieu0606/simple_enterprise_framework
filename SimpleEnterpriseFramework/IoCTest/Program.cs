using System;
using System.Data;
using Core.Database;
using Core.Utils;
using IoC.DI;

namespace IoCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoDatabase();
        }

        private async static void DemoDatabase()
        {
            CurrentFrameworkState.Instance.ChangeDataBase(
                DatabaseType.Postgres,
                host: "localhost",
                dbName: "crm",
                username: "postgres",
                password: "postgres");

            var db = ServiceLocator.Instance.Get<IDatabase>();

            db.OpenConnection();

            var list = await db.GetAllTableNames();
            Console.WriteLine(list?.Count);

            //var result = await db.ExecuteQueryAsync("select * from account");


            DataTable result = await db.GetTable("accounts");
            Console.WriteLine(result.Columns.Count);

            DataRow oks = await db.GetOneRow("accounts", "user_id", "10");

            string rowStr = null;
            foreach (DataColumn col in result.Columns)
            {

                rowStr += string.Format(
                    "{0}: {1}, ",
                    col.ColumnName,
                    oks[col.ColumnName]
                );
            }
            Console.WriteLine("sdd: {0}", rowStr);




            //Console.WriteLine(result.Columns[0].MaxLength);


            if (result?.Rows?.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string rowStr1 = null;
                   foreach (DataColumn col in row.Table.Columns)
                    {

                       rowStr1 += string.Format(
                            "{0}: {1}, ",
                            col.ColumnName,
                           row[col.ColumnName]
                     );
                    }
                    Console.WriteLine(rowStr1);
                }

            }

            //bool ke = db.Delete("accounts", result.Rows[0]);
            //Console.WriteLine(ke);



            //DataRow newRow = result.Rows[0];
            //newRow["user_id"] = 10;
            //newRow["username"] = "lji";
            //newRow["email"] = "hkoi@gmail.com";
            //bool okey = db.Insert("accounts", newRow);
            //Console.WriteLine(okey);

            db.CloseConnection();
        }
    }
}
