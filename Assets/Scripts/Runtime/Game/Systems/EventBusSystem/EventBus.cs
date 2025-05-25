using Nara.Core.Architecture;
using Nara.Patterns;
using System;
using System.Collections.Generic;

namespace Nara.System
{
    public class EventBus : BaseSystem
    {
        private Dictionary<Type, IEventBus> Buses = new Dictionary<Type, IEventBus>();

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
            return (EventBus<T>)Buses[typeof(T)];
        }
        public void Raise<T>(T @event) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Raise(@event);
        }
        public void Register<T>(Action<T> action) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Register(action);
        }
        public void Unregister<T>(Action<T> action) where T : IEvent
        {
            var bus = GetBus<T>();
            bus?.Unregister(action);
        }
        void Clear()
        {
            foreach (var bus in Buses.Values)
            {
                bus.Clear();
            }
            EventBusUtil.ClearAllBuses();
        }
    }
}
