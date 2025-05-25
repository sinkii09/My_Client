using Nara.Core.Architecture;
using Nara.Game.Enum;

namespace Nara.System.UGUISystem
{
    public class ShowUIEvent : IEvent
    {
        public UIType UIType { get; private set; }
        public ShowUIEvent(UIType uiType)
        {
            UIType = uiType;
        }
    }
}