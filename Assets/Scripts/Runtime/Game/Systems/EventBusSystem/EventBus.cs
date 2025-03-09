using Nara.Core.Architecture;
using Nara.Patterns;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nara.System
{
    public class EventBus : BaseSystem
    {
        private Dictionary<Type, object> Buses = new Dictionary<Type, object>();

        protected override bool OnInit()
        {
            return true;
        }
        protected override void OnTerminate()
        {
            Clear();
        }
        public EventBus<T> GetBus<T>() where T : IEvent
        {
            if (!Buses.ContainsKey(typeof(T)))
            {
                Buses.Add(typeof(T), new EventBus<T>());
            }
            return Buses[typeof(T)] as EventBus<T>;
        }
        public void Raise<T>(T @event) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Raise(@event);
        }
        public void Register<T>(EventBinding<T> binding) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Register(binding);
        }
        public void Unregister<T>(EventBinding<T> binding) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Unregister(binding);
        }
        void Clear()
        {
            foreach (var bus in Buses.Values)
            {
                var clearMethod = bus.GetType().GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }
            EventBusUtil.ClearAllBuses();
        }
    }
}
