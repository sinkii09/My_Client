using Nara.Game.Enum;
using Nara.System.UGUISystems;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.Game.UI
{
    public class UIPopupHandler : UIHandler
    {
        public override UIType UIType => UIType.Popup;
        public override bool CanHaveMultiple => true;
        public override bool IsOpen { get; protected set; }

        [SerializeField]
        private Button _hideButton;
        protected override void LateStart()
        {
            // Initialization logic for the popup can go here
            _hideButton.onClick.AddListener(OnHideButtonClicked);
        }

        private void OnDestroy()
        {
            if (_hideButton != null)
            {
                _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            }
        }
        public override void OnShow()
        {
            Debug.Log("Popup shown!");
        }
        public override void OnHide()
        {
            Debug.Log("Popup hidden!");
        }

        private void OnHideButtonClicked()
        {
            Hide();
        }

    }
}