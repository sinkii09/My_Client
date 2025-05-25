using Nara.Core.Architecture;
using Nara.Game.Enum;
using Nara.System.UGUISystems;

namespace Nara.Game.Event
{
    public class ShowUIEvent : IEvent
    {
        public UIType UIType { get; private set; }
        public IUIContext Context { get; private set; }
        public ShowUIEvent(UIType uiType, IUIContext context)
        {
            UIType = uiType;
            Context = context;
        }
    }
}