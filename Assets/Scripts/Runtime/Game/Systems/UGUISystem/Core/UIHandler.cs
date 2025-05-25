using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
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
        public abstract void OnHide();

        protected IUIContext _context;
        public void Show(IUIContext context)
        {
            if (IsOpen && !CanHaveMultiple)
            {
                Debug.LogWarning($"UI of type {UIType} is already open and cannot be opened multiple times.");
                return;
            }
            IsOpen = true;
            _context = context;
            gameObject.SetActive(true);
            OnShow();
        }
        protected void Hide()
        {
            GameApp.Interface.GetSystem<EventBus>().Raise(new HideUIEvent(this));
        }
        public void HideImmediately()
        {
            IsOpen = false;
            gameObject.SetActive(false);
            OnHide();
        }
    }
}