using Cysharp.Threading.Tasks;
using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using System;
using System.Threading;
using UnityEngine;

namespace Nara.System.UGUISystems
{
    public interface IUIContext
    {

    }
    public abstract class UIHandler : NaraBehaviour
    {
        public abstract UIType UIType { get; }
        public abstract bool CanHaveMultiple { get; }
        public abstract bool IsOpen { get; protected set; }
        public abstract void OnShow();
        public abstract float OnHide();

        protected IUIContext _context;

        public virtual void Initialize()
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
    }
}