using DG.Tweening;
using Nara.Core.Architecture;
using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using Nara.Patterns;
using Nara.System;
using Nara.System.UGUISystems;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.Game.UI
{
    public class UIMenuHandler : UIHandler
    {
        public override UIType UIType => UIType.MainMenu;

        public override bool CanHaveMultiple => false;

        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _hideButton;
        [SerializeField]
        private Button _openPopUpButton;

        [SerializeField]
        private RectTransform _menuPanel;

        [SerializeField]
        private TextMeshProUGUI _titleText;

        private EventBus _eventBus;

        #region origin controls state
        private Vector2 _originMenuPanelPosition;
        private Vector2 _originMenuPanelOffset;
        private Vector3 _originMenuPanelScale;


        private Vector2[] _originButtonPositions;
        private Vector3[] _originButtonScales;

        private Vector2 _originTitleTextPosition;
        private Vector2 _originTitleTextOffset;
        private Vector3 _originTitleTextScale;
        private float _originTitleTextAlpha;

        #endregion

        protected override void OnInitialize()
        {
            // Store original states
            _originMenuPanelPosition = _menuPanel.anchoredPosition;
            _originMenuPanelOffset = new Vector2(0, -60f);
            _originMenuPanelScale = _menuPanel.localScale;

            _originTitleTextPosition = _titleText.rectTransform.anchoredPosition;
            _originTitleTextOffset = new Vector2(0, -30f);
            _originTitleTextAlpha = _titleText.alpha;
            _originTitleTextScale = _titleText.rectTransform.localScale;

            var buttons = _menuPanel.GetComponentsInChildren<Button>();
            _originButtonPositions = new Vector2[buttons.Length];

            _originButtonScales = new Vector3[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                var rect = buttons[i].transform as RectTransform;
                _originButtonPositions[i] = rect.anchoredPosition;
                _originButtonScales[i] = rect.transform.localScale;
            }
        }
        protected override void LateStart()
        {
            _eventBus = GameApp.Interface.GetSystem<EventBus>();
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

        public override float OnHide()
        {
            return DoHideAnimation();
        }

        public override void OnShow()
        {
            ResetUIState();
            AssignData();
            DoShowAnimation();
        }

        private void OnHideButtonClicked()
        {
            _hideButton.interactable = false;
            _ = HideAsync();
        }

        private void OnPlayButtonClicked()
        {
            _playButton.interactable = false;
            StartGameEvent startGameEvent = new StartGameEvent()
            {
                OnAnimationComplete = HideAsync
            };
            _eventBus.Raise(startGameEvent);
        }

        private void OnShowPopupButtonClicked()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.Popup, null);
            _eventBus.Raise(showUIEvent);
        }

        private void AssignData()
        {
            _titleText.text = "Welcome to the Main Menu!";
        }

        private void DoShowAnimation()
        {

            // Reset title state
            _titleText.rectTransform.localScale = Vector3.zero;
            _titleText.rectTransform.anchoredPosition += _originTitleTextOffset;
            _titleText.alpha = 0f;

            // Animate title: scale, move up, and fade in
            var titleSeq = DOTween.Sequence();
            titleSeq.Append(_titleText.rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
            titleSeq.Join(_titleText.DOFade(1f, 0.4f));
            titleSeq.Join(_titleText.rectTransform.DOAnchorPosY(_titleText.rectTransform.anchoredPosition.y - _originTitleTextOffset.y, 0.5f).SetEase(Ease.OutCubic));
            titleSeq.Play();

            // Animate menu panel scaling in
            _menuPanel.localScale = Vector3.zero;
            _menuPanel.anchoredPosition = _originMenuPanelPosition + _originMenuPanelOffset;
            _menuPanel.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.1f)
                .SetUpdate(true);

            var buttons = _menuPanel.GetComponentsInChildren<Button>();
            float delayStep = 0.08f;
            float baseDelay = 0.15f;

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                var rect = button.transform as RectTransform;

                // Reset state
                rect.localScale = Vector3.zero;
                var originalPos = rect.anchoredPosition;
                rect.anchoredPosition = originalPos + _originMenuPanelOffset;

                // Sequence for each button
                var seq = DOTween.Sequence();
                seq.AppendInterval(baseDelay + i * delayStep);
                seq.Append(rect.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack));
                seq.Join(rect.DOAnchorPos(originalPos, 0.35f).SetEase(Ease.OutCubic));
                seq.Play();
            }
        }

        private float DoHideAnimation()
        {
            var titleSeq = DOTween.Sequence();
            titleSeq.Append(_titleText.rectTransform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));
            titleSeq.Join(_titleText.DOFade(0f, 0.25f));
            titleSeq.Join(_titleText.rectTransform.DOAnchorPosY(_titleText.rectTransform.anchoredPosition.y + _originMenuPanelScale.y, 0.3f).SetEase(Ease.InCubic));
            titleSeq.Play();

            // Animate buttons with a staggered, cascading effect
            var buttons = _menuPanel.GetComponentsInChildren<Button>();
            float delayStep = 0.06f;
            float baseDelay = 0.05f;

            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                var rect = button.transform as RectTransform;
                var originalPos = rect.anchoredPosition;

                // Sequence for each button
                var seq = DOTween.Sequence();
                seq.AppendInterval(baseDelay + i * delayStep);
                seq.Append(rect.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
                seq.Join(rect.DOAnchorPos(originalPos + _originMenuPanelOffset, 0.2f).SetEase(Ease.InCubic));
                seq.Play();
            }

            // Animate the menu panel scaling out after buttons and title
            float panelDelay = baseDelay + buttons.Length * delayStep + 0.1f;
            _menuPanel.DOScale(Vector3.zero, 0.25f)
                .SetEase(Ease.InBack)
                .SetDelay(panelDelay)
                .SetUpdate(true);

            return panelDelay + 0.25f; // Return total duration for the hide animation
        }

        protected override void ResetUIState()
        {
            _menuPanel.anchoredPosition = _originMenuPanelPosition;
            _menuPanel.localScale = _originMenuPanelScale;

            _titleText.rectTransform.anchoredPosition = _originTitleTextPosition;
            _titleText.rectTransform.localScale = _originTitleTextScale;
            _titleText.alpha = _originTitleTextAlpha;
            
            var buttons = _menuPanel.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                var rect = buttons[i].transform as RectTransform;
                rect.anchoredPosition = _originButtonPositions[i];
                rect.localScale = _originButtonScales[i];
            }

            _hideButton.interactable = true;
            _playButton.interactable = true;
        }
    }
}