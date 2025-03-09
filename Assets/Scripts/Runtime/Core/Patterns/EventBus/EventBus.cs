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
    
        public void Register(EventBinding<T> binding) => bindings.Add(binding);
        public void Unregister(EventBinding<T> binding) => bindings.Remove(binding); 
    
        public void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(bindings);

            foreach (var item in snapshot)
            {
                if (bindings.Contains(item))
                {
                    item.OnEvent.Invoke(@event);
                }
            }
        }
        public  void Clear()
        {
            bindings.Clear();
        }
        public void BindingAndRegister(Action<T> onEvent)
        {
            var binding = new EventBinding<T>(onEvent);
            bindings.Add(binding);
        }
    }
}

