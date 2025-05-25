using Nara.Core.Architecture;
using Nara.Game;
using Nara.Game.Config;
using Nara.Game.Enum;
using Nara.Game.Event;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.UGUISystems
{
    public class UGUISystem : BaseSystem
    {
        private Dictionary<UIType, Stack<UIHandler>> _uiPools;
        private Dictionary<UIType, Stack<UIHandler>> _openUI;

        private UGUIConfig _uiConfig;

        private EventBus _eventBus;
        protected override bool OnInit()
        {
            _uiPools = new Dictionary<UIType, Stack<UIHandler>>();
            _openUI = new Dictionary<UIType, Stack<UIHandler>>();

            _uiConfig = GameApp.Interface.GetConfig<UGUIConfig>();

            if (_uiConfig == null)
            {
                Debug.LogError("[UGUI System] UGUIConfigSO not found in Resources/Configs/UGUIConfig");
                return false;
            }

            _eventBus = GameApp.Interface.GetSystem<EventBus>();

            _eventBus.Register<HideUIEvent>(HideUI);
            _eventBus.Register<ShowUIEvent>(ShowUI);

            return true;
        }

        public void ShowUI(ShowUIEvent @event)
        {
            UIType uiType = @event.UIType;
            var prefab = _uiConfig.UIHandlers[uiType];
            if (prefab == null)
            {
                Debug.LogError($"UIHandler not found for {uiType}");
                return;
            }

            if (_openUI.TryGetValue(uiType, out var uiStack) && uiStack.Count > 0)
            {
                if (uiStack.Peek().CanHaveMultiple == false) return;
            }

            UIHandler handler = null;

            if (!_uiPools.TryGetValue(uiType, out var pool))
            {
                _uiPools[uiType] = new Stack<UIHandler>();
                handler = Object.Instantiate(prefab);
            }
            else if (!pool.TryPop(out handler) || handler == null)
            {
                handler = Object.Instantiate(prefab);
            }

            handler.Show(@event.Context);

            if (!_openUI.TryGetValue(uiType, out uiStack))
            {
                _openUI[uiType] = new Stack<UIHandler>();
            }

            _openUI[uiType].Push(handler);
        }

        public void HideUI(HideUIEvent hideEvent)
        {
            UIHandler handler = hideEvent.Handler;

            if (_openUI.TryGetValue(handler.UIType, out var uiStack) == false || uiStack.Count == 0)
            {
                Debug.LogWarning($"No open UI found for {handler.UIType}");
                return;
            }

            if (uiStack.Peek() != handler)
            {
                Debug.LogWarning($"Trying to hide UI {handler.UIType} that is not on top of the stack.");
                return;
            }

            _openUI[handler.UIType].Pop();
            handler.HideImmediately();

            if (_uiPools.TryGetValue(handler.UIType, out var pool))
            {
                pool.Push(handler);
            }
        }
    }
}