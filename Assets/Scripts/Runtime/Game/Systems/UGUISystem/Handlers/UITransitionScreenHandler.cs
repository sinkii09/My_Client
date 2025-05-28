using DG.Tweening;
using Nara.Game.Enum;
using Nara.System.UGUISystems;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.Game.UI
{
    public class UITransitionScreenHandler : UIHandler
    {
        public override UIType UIType => UIType.TransitionScreen;
        public override bool CanHaveMultiple => false;
        public override bool IsOpen { get; protected set; }

        [SerializeField]
        private Image _transitionImage;

        [SerializeField]
        private CanvasGroup _canvasGroup;
        public override void Initialize()
        {
            _transitionImage.color = new Color(0, 0, 0, 0);
        }
        public override float OnHide()
        {
            return DoHideAnimation();
        }
        public override void OnShow()
        {
            ResetUIState();
            DoShowAnimation();
        }
        private void DoShowAnimation()
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_transitionImage.DOFade(0f, 0.5f).SetEase(Ease.InOutQuad).SetUpdate(true));
            sequence.OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
            });

            IsOpen = true;
        }
        private float DoHideAnimation()
        {
            _transitionImage.DOFade(1f, 0.5f).SetEase(Ease.InOutQuad).SetUpdate(true);
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            IsOpen = false;
            return 0.5f;
        }
        protected override void ResetUIState()
        {
            _transitionImage.color = Color.black;
        }
    }
}