using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace IoC
{
    public class Container
    {
        private static readonly string _rootName = "objects";
        private static readonly string _elementName = "object";
        private static Random _random = new Random();

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

        private IDictionary<string, object> _objects;
        private IDictionary<string, XmlNode> _nodes;

        private Container()
        {
            _objects = new Dictionary<string, object>();
            _nodes = new Dictionary<string, XmlNode>();
        }

        public void Init()
        {
            var assembly = Assembly.GetCallingAssembly();
            var projectName = assembly.GetName().Name;
            var containerName = string.Format("{0}.container.xml", projectName);
            var sourceNames = assembly.GetManifestResourceNames();

            if (sourceNames == null || sourceNames.Count() == 0 || !sourceNames.Contains(containerName))
            {
                return;
            }

            using (Stream stream = assembly.GetManifestResourceStream(containerName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(stream);

                if(doc.DocumentElement?.Name != _rootName)
                    return;

                if (doc.DocumentElement?.ChildNodes?.Count > 0)
                {
                    GetNodes(doc);
                    RegisterObjects();
                }
            }
        }

        private void RegisterObjects()
        {
            if (_nodes?.Count() == 0)
                return;

            foreach (var nodeKey in _nodes)
            {
                if(!string.IsNullOrEmpty(nodeKey.Key) && !_objects.ContainsKey(nodeKey.Key))
                {
                    var obj = GetObjectFromNode(nodeKey);
                    RegisterObject(nodeKey.Key, obj);
                }
            }
        }

        private string RegisterObject(string key, object obj)
        {
            if(!string.IsNullOrWhiteSpace(key) && !_objects.ContainsKey(key) && obj != null)
            {
                _objects.Add(key, obj);
                return key;
            }
            return string.Empty;
        }

        private object GetObjectFromNode(KeyValuePair<string, XmlNode> nodeKey)
        {
            var node = nodeKey.Value;
            var attrs = node.Attributes;
            var classname = attrs["class"];

            Type t = Type.GetType(classname.Value);

            var obj = Activator.CreateInstance(t);
            if (obj == null)
                return null;

            if (node.ChildNodes?.Count > 0)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "property" && childNode.Attributes?.Count > 1)
                    {
                        SetPropertyValueFromXmlNode(obj, childNode);
                    }
                }
            }

            return obj;
        }

        private void GetNodes(XmlDocument doc)
        {
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var attrs = node.Attributes;
                var id = attrs["id"];
                var classname = attrs["class"];

                if (attrs?.Count != 2)    //must have id and object type
                    continue;
                if (id == null || string.IsNullOrWhiteSpace(id.Value))
                    continue;
                if (classname == null || string.IsNullOrWhiteSpace(classname.Value))
                    continue;
                if (node.Name != _elementName)
                    continue;
                if (_nodes.ContainsKey(id.Value))
                    continue;

                Type t = Type.GetType(classname.Value);
                if (t == null)
                    continue;

                _nodes.Add(id.Value, node);
            }
        }

        private void SetPropertyValueFromXmlNode(object obj, XmlNode childNode)
        {
            string propertyName = childNode.Attributes["name"]?.Value;
            string val = childNode.Attributes["value"]?.Value;

            string refId = childNode.Attributes["ref"]?.Value;
            var refObject = GetReferenceObject(refId);

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                if (property != null)
                {
                    try
                    {
                        if (refObject != null && refObject.GetType() == property.PropertyType)
                        {
                            property.SetValue(obj, Convert.ChangeType(refObject, property.PropertyType));
                        }
                        else if (!string.IsNullOrWhiteSpace(val))
                        {
                            property.SetValue(obj, Convert.ChangeType(val, property.PropertyType));
                        }
                    }
                    catch
                    {
                        //do nothing
                    }
                }
            }
        }

        private object GetReferenceObject(string refId)
        {
            if (string.IsNullOrWhiteSpace(refId))
                return null;
            if (!_nodes.ContainsKey(refId))
                return null;

            if (_objects.ContainsKey(refId))
                return _objects[refId];

            var nodeKey = _nodes.FirstOrDefault(i => i.Key == refId);
            var obj = GetObjectFromNode(nodeKey);
            RegisterObject(nodeKey.Key, obj);

            return obj;
        }

        public T GetObjectById<T>(string id)
        {
            if (!_objects.ContainsKey(id))
                return default(T);
            return (T)_objects[id];
        }

        public string RegisterObject(object obj)
        {
            string key = string.Empty;
            int times = 0;
            do
            {
                key = RandomKey();
                if(times == 1000)
                {
                    return string.Empty;
                }
                times++;
            } while (_objects.ContainsKey(key));

            return RegisterObject(key, obj);
        }

        public static string RandomKey()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = _random.Next(4, chars.Length);

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
