using Cysharp.Threading.Tasks;
using Nara.Core.Architecture;
using System;

namespace Nara.Game.Event
{
    public class StartGameEvent : IEvent
    {
        public Func<UniTask> OnAnimationComplete;
    }
}