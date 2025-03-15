using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Extensions;
using Nara.Patterns;
using Nara.System.UI;
using System;
using UnityEngine.SceneManagement;

namespace Nara.System.Scene
{
    public class SceneMainMenuController : SceneControllerBase
    {
        public override SceneEnum SceneType => SceneEnum.MainMenu;

        private EventBus _eventBusSystem;
        private UISystem _uiSystem;

        private EventBinding<StartGameEvent> _startGameEventBinding;
        protected override void LateStart()
        {
            _startGameEventBinding = new EventBinding<StartGameEvent>(OnGameStart);


            _eventBusSystem = GameApp.Inteface.GetSystem<EventBus>();
            _eventBusSystem.Register(_startGameEventBinding);


            _uiSystem = GameApp.Inteface.GetSystem<UISystem>();
            _uiSystem.ShowUI(UIType.MainMenu);
        }
        void OnDestroy()
        {
            _eventBusSystem.Unregister(_startGameEventBinding);
        }

        private void OnGameStart(StartGameEvent @event)
        {
            SceneManager.LoadSceneAsync(SceneTypeExtensions.GetSceneName(SceneEnum.MapSelection));
        }
    }
}