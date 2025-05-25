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
        public override SceneType SceneType => SceneType.MainMenu;

        private EventBus _eventBusSystem;
        private UISystem _uiSystem;

        private EventBinding<StartGameEvent> _startGameEventBinding;
        protected override void LateStart()
        {

            RegisterSystem();

            //_startGameEventBinding = new EventBinding<StartGameEvent>(OnGameStart);
            //_eventBusSystem.Register(_startGameEventBinding);

            //_uiSystem.ShowUI(UIType.MainMenu);
        }
        void OnDestroy()
        {
            _eventBusSystem.Unregister(_startGameEventBinding);
        }
        private void RegisterSystem()
        {
            _eventBusSystem = GameApp.Interface.GetSystem<EventBus>();
            //_uiSystem = GameApp.Interface.GetSystem<UISystem>();
        }

        private void OnGameStart(StartGameEvent @event)
        {
            //SceneManager.LoadSceneAsync(SceneTypeExtensions.GetSceneName(SceneType.MapSelection));
        }

    }
}