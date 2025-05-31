using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Nara.System.Pool
{

    public interface IPoolable
    {
        void OnCreate();
        void OnGet();
        void OnRelease();
        void OnDestroyByPool();
    }

    public interface IPoolable<T> : IPoolable where T : MonoBehaviour
    {
        T GetComponent();
    }

    public class DynamicPool
        <T> : ObjectPool<T> where T : class, IPoolable
    {
        private float _shrinkRatio = 0.5f; // Shrink to 50% of the current size
        private float _shrinkInterval = 5; 
        private float _lastShrinkTime;
        private int _countActive;
        private int _initialCapacity;
        private Func<T> _createFunc;
        public DynamicPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000) : base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
            _createFunc = createFunc;
            _initialCapacity = defaultCapacity;
            _lastShrinkTime = Time.time;
            _countActive = 0;
        }

        /// <summary>
        /// Extension method to get a value from the pool and automatically expand the pool if needed.
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            T value = Get();
            _countActive++;
            if (CountInactive == 0)
            {
                ExpandPool();
            }
            return value;
        }

        /// <summary>
        /// Extension method to release a value back to the pool and shrink the pool if necessary.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ReleaseValue(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Cannot release a null value to the pool.");
            }
            Release(value);
            _countActive--;
            if (Time.time - _lastShrinkTime >= _shrinkInterval)
            {
                ShrinkPool();
                _lastShrinkTime = Time.time;
            }
        }

        private void ExpandPool()
        {
            int totalCount = _countActive + CountInactive;
            for (int i = 0; i < totalCount; i++)
            {
                T item = _createFunc();
                if (item == null)
                {
                    throw new InvalidOperationException("Create function returned null.");
                }
                item.OnCreate();
                Release(item);
            }
        }
        
        private void ShrinkPool()
        {
            if (CountInactive > _initialCapacity)
            {
                int targetCount = Math.Max(_initialCapacity, (int)(CountInactive * _shrinkRatio));

                int itemsToRemove = CountInactive - targetCount;

                for (int i = 0; i < itemsToRemove; i++)
                {
                    if (CountInactive > 0)
                    {
                        T item = Get();
                        item.OnDestroyByPool();
                    }
                }
            }
        }
    }
}