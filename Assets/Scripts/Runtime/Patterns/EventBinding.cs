using System;

namespace Nara.Patterns
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        //public Action OnEventNoArgs { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        Action<T> onEvent = _ => {};
        public Action<T> OnEvent 
        { 
            get => onEvent;
            set => onEvent = value;
        }

        public EventBinding(Action<T> onEvent)
        {
            this.onEvent = onEvent;
        }

        //public Action OnEventNoArgs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(Action<T> onEvent)
        {
            this.onEvent += onEvent;
        }
        public void Remove(Action<T> onEvent)
        {
            this.onEvent -= onEvent;
        }
    }
}

