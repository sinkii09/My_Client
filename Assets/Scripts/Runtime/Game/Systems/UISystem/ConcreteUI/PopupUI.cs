using Nara.Game;
using Nara.Game.Enum;
using UnityEngine.UIElements;

namespace Nara.System.UI
{
    public class PopupUI : UIHandlerBase
    {
        public override UIType UIType => UIType.Popup;
        private VisualElement _closeBtn;
        protected override void LateStart()
        {
            _closeBtn = Root.Q<Button>(name: "close-btn");
            _closeBtn.RegisterCallback<ClickEvent>(OnCloseBtnClick);
        }
        private void OnCloseBtnClick(ClickEvent evt)
        {
            GameApp.Inteface.GetSystem<EventBus>().Raise(new ClosePopupEvent(this));
        }
    }
}