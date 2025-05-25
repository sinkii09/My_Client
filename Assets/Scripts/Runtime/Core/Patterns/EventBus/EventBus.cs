using Nara.Core.Architecture;
using Nara.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nara.Patterns
{
    public interface IEventBus
    {
        void Clear();
    }
    public class EventBus<T> : IEventBus where T : IEvent
    {
        private static Dictionary<Type, List<IEventBinding<T>>> _eventDict = new();
    
        public void Raise(T @event)
        {
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
            _eventDict.Clear();
        }
        public void Register(Action<T> onEvent)
        {
            var binding = new EventBinding<T>(onEvent);

            if (!_eventDict.TryGetValue(typeof(T), out var bindings))
            {
                _eventDict[typeof(T)] = new List<IEventBinding<T>>();
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
                        bindings.Remove(binding);
                    }
                }
            }
        }
    }
}

