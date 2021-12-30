using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Core.Database;
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
                DatabaseType.MySql,
                host: "",
                dbName: "",
                username: "",
                password: "");

            var currentDb = CurrentFrameworkState.Instance.DatabaseType;

            var db = ServiceLocator.Instance.Get<IDatabase>();

            var isConnectSuccess = db.OpenConnection();

            var tableNames = db.GetAllTableNames();

            var result = await db.ExecuteSqlAsync("select * from student");

            if (result?.Rows?.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    string rowStr = string.Format(
                        "id: {0}, gpa: {1}, age: {2}, full_name: {3}",
                        row["id"],
                        row["gpa"],
                        row["age"],
                        row["full_name"]
                        );

                    Console.WriteLine(rowStr);
                }
            }

            string updateStatement = string.Format("update {0} set gpa = {1} where id = {2}", "student", 8.5, 2);

            int effectedRowCount = await db.ExecuteNoQueryAsync(updateStatement);

            db.CloseConnection();
        }
    }
}
