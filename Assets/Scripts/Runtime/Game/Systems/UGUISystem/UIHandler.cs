using Nara.Game.Enum;
using UnityEngine;

namespace Nara.System.UGUISystem
{
    public abstract class UIHandler : MonoBehaviour
    {
        public abstract UIType UIType { get; }
        public abstract bool CanHaveMultiple { get; }
        public abstract bool IsOpen { get; }
        public abstract void OnShow();
        public abstract void OnHide();
    }
}