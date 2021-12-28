using System;
using System.Collections;
using System.Collections.Generic;

namespace IoC.DI
{
    class Cache
    {
        private IDictionary<object, object> _container = new Dictionary<object, object>();

        public Cache()
        {
        }

        public void Put<IContract, CImplement>(object[] inputs) where CImplement : new()
        {
            _container[typeof(IContract)] = Activator.CreateInstance(typeof(CImplement), inputs);
        }

        public T Get<T>() where T : class
        {
            try
            {
                if (_container.ContainsKey(typeof(T)))
                {
                    return (T)_container[typeof(T)];
                }
                return default(T);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public void Remove<T>() where T : class
        {
            if(_container.ContainsKey(typeof(T)))
                _container.Remove(typeof(T));
        }
    }
}
