using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using Nara.System.Pool;
using System;
using System.Threading;
using UnityEngine;

namespace Nara.System.UGUISystems
{
    public interface IUIContext
    {

    }
    public abstract class UIHandler : NaraBehaviour, IPoolable
    {
        public abstract UIType UIType { get; }
        public abstract bool CanHaveMultiple { get; }
        public abstract void OnShow();
        public abstract float OnHide();
        public bool IsOpen { get; protected set; }
        public bool IsInitialized { get; protected set; }

        protected IUIContext _context;

        public void Initialize()
        {
            OnInitialize();
            IsInitialized = true;
        }
        protected virtual void OnInitialize()
        {
            
        }
        public void Show(IUIContext context)
        {
            if (IsOpen && !CanHaveMultiple)
            {
                Debug.LogWarning($"UI of type {UIType} is already open and cannot be opened multiple times.");
                return;
            }
            _context = context;
            gameObject.SetActive(true);
            OnShow();
            IsOpen = true;
        }

        public async UniTask HideAsync()
        {
            float duration = OnHide();
            float elapsedTime = 0f;
            CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
            try
            {
                while ( elapsedTime < duration )
                {
                    elapsedTime += Time.deltaTime;
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during HideImmediately: {ex.Message}");
            }
            finally
            {
                IsOpen = false;
                gameObject.SetActive(false);
                GameApp.Interface.GetSystem<EventBus>().Raise(new HideUIEvent(this));
            }
        }
        protected virtual void ResetUIState()
        {

        }

        public void OnCreate()
        {
            gameObject.SetActive(false);
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {

        }

        public virtual void OnDestroyByPool()
        {
            DOTween.Kill(gameObject);
            Destroy(gameObject);
        }

    }
}