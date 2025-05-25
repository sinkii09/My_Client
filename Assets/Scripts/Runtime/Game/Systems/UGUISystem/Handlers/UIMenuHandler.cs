using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using Nara.System.UGUISystems;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.Game.UI
{
    public class UIMenuHandler : UIHandler
    {
        public override UIType UIType => UIType.MainMenu;

        public override bool CanHaveMultiple => false;

        public override bool IsOpen { get; protected set; }

        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _hideButton;
        [SerializeField]
        private Button _openPopUpButton;
        protected override void LateStart()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _hideButton.onClick.AddListener(OnHideButtonClicked);
            _openPopUpButton.onClick.AddListener(OnShowPopupButtonClicked);
        }
        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            _openPopUpButton.onClick.RemoveListener(OnShowPopupButtonClicked);
        }

        public override void OnHide()
        {

        }

        public override void OnShow()
        {

        }

        private void OnHideButtonClicked()
        {
            Hide();
        }

        private void OnPlayButtonClicked()
        {
            Debug.Log("Play button clicked!");
        }

        private void OnShowPopupButtonClicked()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.Popup, null);
            GameApp.Interface.GetSystem<UGUISystem>().ShowUI(showUIEvent);
        }

    }
}