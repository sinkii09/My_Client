using Nara.Game.Enum;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nara.System.UI
{
    public interface IUIHandler
    {
        UIType UIType { get; }
        void ShowUI();
        void HideUI();
    }
    public abstract class UIHandlerBase : NaraBehaviour, IUIHandler
    {
        public abstract UIType UIType { get; }

        [SerializeField]
        protected UIDocument UIDocument;

        protected VisualElement Root => UIDocument.rootVisualElement;
        public virtual void ShowUI()
        {
            Root.style.display = DisplayStyle.Flex;
        }
        public virtual void HideUI()
        {
            Root.style.display = DisplayStyle.None;
        }
    }
}