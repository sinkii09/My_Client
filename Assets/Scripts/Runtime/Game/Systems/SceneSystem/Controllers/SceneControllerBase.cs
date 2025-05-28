using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nara.Game.Enum;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.System.Scene
{
    public abstract class SceneControllerBase : NaraBehaviour
    {
        public abstract SceneType SceneType { get; }

        private Image _screenTransitionImage;
        [SerializeField] private CanvasGroup _screenTransitionCanvasGroup;
        protected override async void LateStart()
        {
            await FadeOutAsync(1);
            RegisterSystem();
            StartScene();
        }

        protected virtual void RegisterSystem()
        {
            // This method can be overridden to register systems if needed
        }
        protected virtual void StartScene()
        {

        }
        protected abstract void LoadScene();
        protected async UniTask ChangeSceneAsync(SceneType sceneType)
        {
            await FadeInAsync();
            LoadScene();
        }
        protected async UniTask FadeInAsync(float duration = 0.5f)
        {
            if (_screenTransitionCanvasGroup == null)
            {
                return;
            }
            _screenTransitionImage.color = new Color(0, 0, 0, 0);
            _screenTransitionImage.raycastTarget = true;
            await _screenTransitionImage.DOFade(1f, duration)
                                .SetEase(Ease.InOutQuad)
                                .SetUpdate(true).AsyncWaitForCompletion().AsUniTask();
        }

        protected async UniTask FadeOutAsync(float duration = .5f)
        {
            _screenTransitionImage = _screenTransitionCanvasGroup.GetComponentInChildren<Image>();

            if (_screenTransitionImage == null)
            {
                return;
            }

            _screenTransitionImage.color = Color.black;
            Debug.Log($"Fading out with duration: {duration}");
            await _screenTransitionImage.DOFade(0f, duration)
                                .SetEase(Ease.InOutQuad)
                                .SetUpdate(true)
                                .AsyncWaitForCompletion().AsUniTask();

            _screenTransitionImage.raycastTarget = false;
        }
    }
}