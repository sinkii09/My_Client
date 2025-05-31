using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nara.Game.Enum;
using Nara.Game.Extensions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        protected async UniTask LoadSceneAsync(SceneType sceneType, LoadSceneMode loadMode = LoadSceneMode.Single)
        {
            await FadeInAsync();
            await SceneManager.LoadSceneAsync(SceneTypeExtensions.GetSceneName(sceneType), loadMode);
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

            await _screenTransitionImage.DOFade(0f, duration)
                                .SetEase(Ease.InOutQuad)
                                .SetUpdate(true)
                                .AsyncWaitForCompletion().AsUniTask();

            _screenTransitionImage.raycastTarget = false;
        }
    }
}