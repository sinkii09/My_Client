using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nara.Patterns
{
    public class ServiceLocator
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
        public IEnumerable<object> Services => services.Values;
        
        public IEnumerable<T> GetServicesByType<T>()
        {
            var type = typeof(T);
            return services.Values.Where(x => type.IsInstanceOfType(x)).Cast<T>();
        }
        public bool TryGet<T>(out object service) where T : class
        {
            var type = typeof(T);
            if(services.TryGetValue(type, out service)) return true;
            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);
            if(services.TryGetValue(key, out var value))
            {
                return value as T;
            }
            throw new ArgumentException($"[Service_Locator] Get: service of type {key.FullName} not registered");
        }
        public ServiceLocator Register<T>(T service)
        {
            var type = typeof(T);
            if(services.TryAdd(type, service))
            {
                Debug.Log($"[Service_Locator] Register: Service of type {type.FullName} already registered");
            }
            return this;
        }
        public void Clear() => services.Clear();
    }
}

