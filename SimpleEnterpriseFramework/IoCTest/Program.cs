using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using IoC;

namespace IoCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Instance.Init();
        }
    }
}
