using Nara.Core.Architecture;
using Nara.System.UGUISystems;

namespace Nara.Game.Event
{
    public class HideUIAnimationEndEvent : IEvent
    {
        public UIHandler Handler { get; private set; }

        public HideUIAnimationEndEvent(UIHandler handler)
        {
            Handler = handler;
        }
    }
}