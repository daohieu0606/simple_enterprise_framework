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
            MemberShip.Role role = new MemberShip.Role();

            var result = db.GetTable("accounts");

            //var oks = db.GetOneRow("accounts", "user_id", "EDDRRUF");
            User user = User.getInstance("username", "111", "email", "phone", "address", "role");

            //User test = await MemberShip.MemberShip.AddNewUserAsync(user);
            //bool isRight = await MemberShip.MemberShip.validateAsync(user.Username, user.Password);
           // Console.WriteLine(isRight);
            //DataRow dr  = await db.GetOneRow(User.nameTable, "user_id", "EDDRRUF");
            //await MemberShip.MemberShip.createRoleAsync(Role.getInstance("testrole"));
            role = await MemberShip.MemberShip.findRoleByNameAsync("test role");

            //await MemberShip.MemberShip.removeRoleAsync("ONBSMYS");


            //--------------------  CHECK
            //Role[] roles = new Role[2];
            //roles[0] = role;
            //role = await MemberShip.MemberShip.findRoleFieldAsync("role_id", "QBQUXCD");
            //roles[1] = role;
            //if (role != null)
            //{
            //   await  MemberShip.MemberShip.AddRolesToUserAsync(roles, "SMQRSFV");
            //}
            //MemberShip.MemberShip.RemoveRoleFromUser("SMQRSFV", "PMVIXBX");//--   OK
            //Role[] _roles =await  MemberShip.MemberShip.getAllRoleAsync();//-- OK

            //if(_roles != null)
            //{
            //    foreach(Role role1 in _roles)
            //    {
            //        Console.WriteLine(role1.Id);
            //    }
            //}
            //if(role!= null)
            //{
            //    role.RoleName = "employee1";
            //    await MemberShip.MemberShip.updateRoleAsync(role);//---- OK
            //}
            User tes = await MemberShip.MemberShip.findUserByIdAsync("SMQRSFsV");//   OK
            if (tes != null)
            {
                Console.WriteLine(tes.Username);
            }

            //var list = db.GetAllTableNames();
            //Console.WriteLine(list?.Count);

            //var result = await db.ExecuteQueryAsync("select * from account");


            //DataTable result = await db.GetTable("accounts");
            //Console.WriteLine(result.Columns.Count);

            //DataRow oks = await db.GetOneRow("accounts", "user_id", "10");

            //string rowStr = null;
            //foreach (DataColumn col in result.Columns)
            //{

            //    rowStr += string.Format(
            //        "{0}: {1}, ",
            //        col.ColumnName,
            //        oks[col.ColumnName]
            //    );
            //}
            //Console.WriteLine("sdd: {0}", rowStr);


            //if (result?.Rows?.Count > 0)
            //{
            //    foreach (DataRow row in result.Rows)
            //    {
            //        string rowStr1 = null;
            //       foreach (DataColumn col in row.Table.Columns)
            //        {

            //           rowStr1 += string.Format(
            //                "{0}: {1}, ",
            //                col.ColumnName,
            //               row[col.ColumnName]
            //         );
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

            //db.Update("accounts", result.Rows[0], newRow);

            db.CloseConnection();
        }
    }
}
