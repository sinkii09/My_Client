using Nara.Core.Architecture;
using Nara.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.Pool
{
    public class PoolSystem : BaseSystem
    {
        private Dictionary<object, object> _pools;

        protected override bool OnInit()
        {
            _pools = new Dictionary<object, object>();
            return true;
        }

        // For C# classes
        #region C# Pool
        public DynamicPool<T> GetPool<T>() where T : class, IPoolable, new()
        {
            object key = typeof(T);
            if (!_pools.TryGetValue(key, out object pool))
            {
                pool = new DynamicPool<T>(
                    () => new T(),
                    actionOnGet: item => item.OnGet(),
                    actionOnRelease: item => item.OnRelease(),
                    actionOnDestroy: item => item.OnDestroyByPool()
                );
                _pools[key] = pool;
            }
            return (DynamicPool<T>)pool;
        }
        // Get instance (C# class)
        public T Get<T>() where T : class, IPoolable, new()
        {
            return GetPool<T>().GetValue();
        }

        // Release instance (C# class)
        public void Release<T>(T value) where T : class, IPoolable, new()
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            GetPool<T>().ReleaseValue(value);
        }
        #endregion

        // For MonoBehaviours (prefab-based)

        #region Mono Pool
        public DynamicPool<T> GetPool<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            object key = prefab;
            if (!_pools.TryGetValue(key, out object pool))
            {
                pool = new DynamicPool<T>(
                    () => UnityEngine.Object.Instantiate(prefab,GameApp.Instance.transform),
                    actionOnGet: item => item.OnGet(),
                    actionOnRelease: item => item.OnRelease(),
                    actionOnDestroy: item => item.OnDestroyByPool()
                );
                _pools[key] = pool;
            }
            return (DynamicPool<T>)pool;
        }


        // Get instance (MonoBehaviour)
        public T Get<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            return GetPool(prefab).GetValue();
        }

        // Release instance (MonoBehaviour)
        public void Release<T>(T value, T prefab) where T : MonoBehaviour, IPoolable
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            GetPool(prefab).ReleaseValue(value);
        }

        protected override void OnTerminate()
        {
            foreach (var pool in _pools.Values)
            {
                if (pool is IDisposable disposablePool)
                {
                    disposablePool.Dispose();
                }
            }
            _pools.Clear();
        }
        #endregion

    }
}