using Nara.Core.Architecture;

namespace Nara.System.UGUISystem
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