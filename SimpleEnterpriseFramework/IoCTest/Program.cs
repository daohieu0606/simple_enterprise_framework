using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
//using Core.Database;
using IoC;
using IoC.DI;

namespace IoCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceLocator.Instance.Register<IDatabase, MySqlDatabase>("student.db", 1);
            //var mysql = ServiceLocator.Instance.Get<IDatabase>();

            //mysql.OnpenConnection("student.db");
            //mysql.CloseConnection("student.db");
        }
    }
}
