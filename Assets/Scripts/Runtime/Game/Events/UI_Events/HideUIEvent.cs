using Nara.Core.Architecture;
using Nara.System.UGUISystems;

namespace Nara.Game.Event
{
    public class HideUIEvent : IEvent
    {
        public UIHandler Handler { get; private set; }

        public HideUIEvent(UIHandler handler)
        {
            Handler = handler;
        }
    }
}