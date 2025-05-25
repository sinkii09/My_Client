using Nara.Core.Architecture;
using Nara.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nara.Patterns
{
    public class EventBus<T> where T : IEvent
    {
        readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        private static Dictionary<Type, List<IEventBinding<T>>> _eventDict = new();
    
        public void Register(EventBinding<T> binding) => bindings.Add(binding);
        public void Unregister(EventBinding<T> binding) => bindings.Remove(binding); 
    
        public void Raise(T @event)
        {
            //var snapshot = new HashSet<IEventBinding<T>>(bindings);

            //foreach (var item in snapshot)
            //{
            //    if (bindings.Contains(item))
            //    {
            //        item.OnEvent.Invoke(@event);
            //    }
            //}

            if (_eventDict.TryGetValue(typeof(T), out var bindings))
            {
                foreach (var item in bindings)
                {
                    item.OnEvent.Invoke(@event);
                }
            }
        }
        public void Clear()
        {
            bindings.Clear();
            _eventDict.Clear();
        }
        public void Register(Action<T> onEvent)
        {
            var binding = new EventBinding<T>(onEvent);

            if (!_eventDict.TryGetValue(typeof(T), out var bindings))
            {
                _eventDict[typeof(T)] = new List<IEventBinding<T>>();
                return;
            }

            _eventDict[typeof(T)].Add(binding);
        }
        public void Unregister(Action<T> onEvent)
        {
            if ( _eventDict.TryGetValue(typeof(T),out var bindings))
            {
                foreach (var binding in bindings)
                {
                    if (binding.OnEvent == onEvent)
                    {
                        binding.Remove(onEvent);
                    }
                    bindings.Remove(binding);
                }
            }
        }
    }
}

