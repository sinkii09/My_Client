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

        protected override void LateStart()
        {
            _startBtn = Root.Q<Button>(name: "start-btn");


            _startBtn.RegisterCallback<ClickEvent>(OnStartBtnClick);
        }

        private void OnStartBtnClick(ClickEvent evt)
        {
            GameApp.Inteface.GetSystem<EventBus>().Raise(new StartGameEvent());
        }
    }
}