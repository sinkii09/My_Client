using System;
using UnityEngine;

namespace Nara.Core.Architecture
{
    public interface ISystem
    {
        bool Initialized { get; set; }

        bool Init();
        void Terminate();
    }
    public abstract class BaseSystem : ISystem
    {
        public bool Initialized { get; set; }

        public bool Init() => OnInit();
        public void Terminate() => OnTerminate();
        protected abstract bool OnInit();
        protected virtual void OnTerminate()
        {

        }
    }
    public interface IEvent
    {

    }
    
}

