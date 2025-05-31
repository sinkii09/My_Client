using DG.Tweening;
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

        [SerializeField]
        private Button _hideButton;

        [SerializeField]
        private RectTransform _popupPanel;

        #region origin controls state

        #endregion
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

        protected override void OnInitialize()
        {

        }

        protected override void ResetUIState()
        {
            _popupPanel.localScale = Vector3.one;

            _hideButton.interactable = true;
        }

        public override void OnShow()
        {
            DoShowAnimation();
        }
        private void DoShowAnimation()
        {
            _popupPanel.localScale = Vector3.zero;
            _popupPanel.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.05f)
                .SetUpdate(true);
        }

        public override float OnHide()
        {
            return DoHideAnimation();
        }

        private float DoHideAnimation()
        {
            _popupPanel.DOScale(Vector3.zero, 0.25f)
                    .SetEase(Ease.InBack)
                    .SetDelay(.05f)
                    .SetUpdate(true);

            return 0.3f; // Return the duration of the hide animation
        }

        private void OnHideButtonClicked()
        {
            _hideButton.interactable = false; // Disable the button to prevent multiple clicks
            _ = HideAsync();
        }

    }
}