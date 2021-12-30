using System;
using System.Data;
using Core.Database;
using Core.Utils;
using IoC.DI;
using MemberShip;
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
                password: "Hai06042000");

            var db = ServiceLocator.Instance.Get<IDatabase>();
            db.OpenConnection();

            //var list = db.GetAllTableNames();
            //Console.WriteLine(list?.Count);
            MemberShip.Role role = new MemberShip.Role();
        
            var result = db.GetTable("accounts");

            var oks = db.GetOneRow("accounts", "user_id", "10");
            User user = User.getInstance("username", "1233", "email", "phone", "address", "role");

           User test = MemberShip.MemberShip.AddNewUser(user);
            bool isRight = MemberShip.MemberShip.validate(user.Username, user.Password);
            Console.WriteLine(isRight);
            user.Username = "change username";
            isRight = MemberShip.MemberShip.UpdateUser(user);
            Console.WriteLine("is change success?: ",isRight);
            string rowStr = null;
            //foreach (DataColumn col in result.Columns)
            //{

            //    rowStr += string.Format(
            //        "{0}: {1}, ",
            //        col.ColumnName,
            //        oks[col.ColumnName]
            //    );
            //}
            //Console.WriteLine("sdd: {0}", rowStr);

            //Console.WriteLine(result.Columns[0].MaxLength);


            //if (result?.Rows?.Count > 0)
            //{
            //    foreach (DataRow row in result.Rows)
            //    {
            //        string rowStr1 = null;
            //        foreach (DataColumn col in row.Table.Columns)
            //        {

            //            rowStr1 += string.Format(
            //                "{0}: {1}, ",
            //                col.ColumnName,
            //                row[col.ColumnName]
            //            );
            //        }
            //        Console.WriteLine(rowStr1);
            //    }

            //}

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
