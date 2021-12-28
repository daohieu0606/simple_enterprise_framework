using System;
using System.Data;
using Core.Database;
using Core.Query;
using Core.Utils;
using IoC.DI;
using Npgsql;

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
                //host: "ec2-54-159-244-207.compute-1.amazonaws.com",
                //dbName: "d6mjh0tj6jfstd",
                //username: "tijrpiymnlgqgk",
                //password: "8dc32830b108cd26e89bc6f0596fedb2dc5577526b5e7740ce998270201b44ce");
               
                host: "localhost",
                dbName: "postgres",
                username: "postgres",
                password: "postgres");

            var currentDb = CurrentFrameworkState.Instance.DatabaseType;
            Console.WriteLine(currentDb);

            var db = ServiceLocator.Instance.Get<IDatabase>();

                var isConnectSuccess = db.OpenConnection();

                var list = db.GetAllTableNames();
                Console.WriteLine(db.GetAllTableNames()?.Count);


                var result = await db.ExecuteQueryAsync("select * from deal");
            

            //Console.WriteLine(result.Columns[0].MaxLength);


           

            if (result?.Rows?.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string rowStr = null;
                    foreach (DataColumn col in row.Table.Columns)
                    {

                        rowStr += string.Format(
                            "{0}: {1}, ",
                            col.ColumnName,
                            row[col.ColumnName]
                        );
                    }
                    Console.WriteLine(rowStr);

                    //DataRow newRow = result.NewRow();
                    //newRow["name"] = "Okei";
                    //foreach (DataColumn col in row.Table.Columns)
                    //{
                    //    Console.WriteLine("- {0} ", newRow[col]);
                    //}
                       
                }

            }

            //string updateStatement = string.Format("update {0} set gpa = {1} where id = {2}", "student", 8.5, 2);

            //int effectedRowCount = await db.ExecuteNoQueryAsync(updateStatement);

            db.CloseConnection();
        }
    }
}
