using Nara.Core.Architecture;
using Nara.Game.Enum;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nara.System.UI
{
    public abstract class UIController : NaraBehaviour
    {

        [SerializeField]
        
        public void ShowUI()
        {

        }
        public void HideUI()
        {

        }
    }
    public class UISystem : BaseSystem
    {
        private Dictionary<UIType, IUIHandler> UIDict = new Dictionary<UIType, IUIHandler>();

        public void ShowUI(UIType uiType)
        {
            if (UIDict.ContainsKey(uiType))
            {
                UIDict[uiType].ShowUI();
            }
        }
        public void HideUI(UIType uiType)
        {
            if (UIDict.ContainsKey(uiType))
            {
                UIDict[uiType].HideUI();
            }
        }
        protected override bool OnInit()
        {
            LoadUIData();
            return true;
        }

        protected override void OnTerminate()
        {
            UIDict.Clear();
        }
        private void RegisterHandler(IUIHandler handler)
        {
            if (!UIDict.ContainsKey(handler.UIType))
            {
                UIDict[handler.UIType] = handler;
            }
        }
        private void LoadUIData()
        {
            var UIConfig = UIConfigSO.Load();
            foreach (var prefab in UIConfig.UIHandlers)
            {
                var uiObj = Object.Instantiate(prefab);

                var uiHandler = uiObj.GetComponent<IUIHandler>();
                RegisterHandler(uiHandler);

                uiHandler.HideUI();
            }
        }
    }
}