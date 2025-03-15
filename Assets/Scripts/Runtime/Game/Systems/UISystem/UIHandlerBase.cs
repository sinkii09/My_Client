using Nara.Game.Enum;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nara.System.UI
{
    public interface IUIHandler
    {
        UIType UIType { get; }
        void ShowUI(object data = null);
        void HideUI();
    }
    public abstract class UIHandlerBase : NaraBehaviour, IUIHandler
    {
        public abstract UIType UIType { get; }

        [SerializeField]
        protected UIDocument UIDocument;

        protected VisualElement Root => UIDocument.rootVisualElement;
        public void ShowUI(object data = null)
        {
            Root.style.display = DisplayStyle.Flex;
            OnShowUI(data);
        }
        public void HideUI()
        {
            Root.style.display = DisplayStyle.None;
            OnHideUI();
        }
        private void OnShowUI(object data = null) { }
        private void OnHideUI() { }
    }
}