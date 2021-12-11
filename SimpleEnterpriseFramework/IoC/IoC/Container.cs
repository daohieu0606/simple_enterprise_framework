using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace IoC
{
    public class Container
    {
        private static Container _instance;
        public static Container Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Container();
                }
                return _instance;
            }
        }

        private Container()
        {

        }

        public void Init()
        {
            var assembly = Assembly.GetCallingAssembly();
            var projectName = assembly.GetName().Name;
            var beanName = string.Format("{0}.beans.xml", projectName);
            var sourceNames = assembly.GetManifestResourceNames();

            if (sourceNames == null || sourceNames.Count() == 0 || !sourceNames.Contains(beanName))
            {
                return;
            }

            using (Stream stream = assembly.GetManifestResourceStream(beanName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);

                if (doc.DocumentElement?.ChildNodes?.Count > 0)
                {
                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        var attrs = node.Attributes;

                        if (attrs?.Count < 2)    //must have id and object type
                        {
                            return;
                        }
                        var id = attrs["id"];
                        var classname = attrs["class"];

                        Type t = Type.GetType(classname.Value);
                        if (t != null)
                        {
                            var bean = Activator.CreateInstance(t);

                            if (bean != null)
                            {
                                foreach (XmlAttribute attr in attrs)
                                {
                                    PropertyInfo property = bean.GetType().GetProperty(attr.Name);
                                    if (property != null)
                                    {
                                        property.SetValue(bean, Convert.ChangeType(attr.Value, property.PropertyType));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
