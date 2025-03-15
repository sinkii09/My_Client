using Nara.Core.Architecture;
using Nara.Game.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.UI
{
    public class UISystem : BaseSystem
    {
        private Dictionary<UIType, IUIHandler> UIDict = new Dictionary<UIType, IUIHandler>();
        private List<IUIHandler> _openUI = new List<IUIHandler>();

        public void ShowUI(UIType uiType, object data = null)
        {
            if (UIDict.ContainsKey(uiType))
            {
                var uiHandler = UIDict[uiType];
                uiHandler.ShowUI();
                _openUI.Add(uiHandler);
            }
        }

        public void HideUI(UIType uiType)
        {
            if (UIDict.ContainsKey(uiType))
            {
                var uiHandler = UIDict[uiType];
                uiHandler.HideUI();
                _openUI.Remove(uiHandler);
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
                var uiGameObject = Object.Instantiate(prefab);
                var uiHandler = uiGameObject.GetComponent<IUIHandler>();
                RegisterHandler(uiHandler);

                uiHandler.HideUI();
            }
        }
    }
}