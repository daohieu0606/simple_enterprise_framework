using System;
using System.Collections.Generic;

namespace IoC.DI
{
    public class ServiceLocator
    {
        private Cache _cache;

        private static ServiceLocator _intance;
        public static ServiceLocator Instance
        {
            get
            {
                if(_intance == null)
                {
                    _intance = new ServiceLocator();
                }
                return _intance;
            }
        }

        private ServiceLocator()
        {
            _cache = new Cache();
        }

        public void Register<IContract, CImplement>(params object[] inputs) where CImplement: new()
        {
            _cache.Put<IContract, CImplement>(inputs);
        }

        public T Get<T>() where T: class
        {
            return _cache.Get<T>();
        }

        public void Remove<T>() where T: class
        {
            _cache.Remove<T>();
        }
    }
}
