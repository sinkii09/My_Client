using Nara.Game;
using Nara.Game.Enum;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nara.System.UI
{
    public class MainMenuUI : UIHandlerBase
    {
        public override UIType UIType => UIType.MainMenu;

        private VisualElement _startBtn;
        private VisualElement _quitBtn;

        protected override void LateStart()
        {
            _startBtn = Root.Q<Button>(name: "start-btn");
            _quitBtn = Root.Q<Button>(name: "quit-btn");

            _startBtn?.RegisterCallback<ClickEvent>(OnStartBtnClick);
            _quitBtn?.RegisterCallback<ClickEvent>(OnQuitBtnClick);
        }

        private void OnStartBtnClick(ClickEvent evt)
        {
            GameApp.Interface.GetSystem<EventBus>().Raise(new StartGameEvent());
        }
        private void OnQuitBtnClick(ClickEvent evt)
        {
            GameApp.Interface.GetSystem<EventBus>().Raise(new QuitGameEvent());
        }
    }
}