using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Core.Database;
using IoC.DI;

namespace IoCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceLocator.Instance.Register<IDatabase, MySqlDatabase>("locoalhost", "student_db", "abc", "abc");

            //var db = ServiceLocator.Instance.Get<IDatabase>();

            //var result = db.OpenConnection();
            //var tables = db.GetAllTableNames();
            //var res = db.CloseConnection();

            ServiceLocator.Instance.Register<IDatabase, PostgreDatabase>("localhost", "student_db", "abc", "abc");

            var db = ServiceLocator.Instance.Get<IDatabase>();

            var result = db.OpenConnection();
            var tables = db.GetAllTableNames();
            var res = db.CloseConnection();
        }
    }
}
