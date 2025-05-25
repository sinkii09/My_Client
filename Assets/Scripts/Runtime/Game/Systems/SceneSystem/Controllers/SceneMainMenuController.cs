using Nara.Core.Architecture;
using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using Nara.Patterns;
using Nara.System.UGUISystems;
using Sirenix.OdinInspector;

namespace Nara.System.Scene
{
    public class SceneMainMenuController : SceneControllerBase
    {
        public override SceneType SceneType => SceneType.MainMenu;

        private EventBus _eventBusSystem;
        private UGUISystem _uiSystem;

        private EventBinding<StartGameEvent> _startGameEventBinding;
        protected override void LateStart()
        {

            RegisterSystem();

            //_startGameEventBinding = new EventBinding<StartGameEvent>(OnGameStart);
            //_eventBusSystem.Register(_startGameEventBinding);
        }

        private void RegisterSystem()
        {
            _eventBusSystem = GameApp.Interface.GetSystem<EventBus>();
            _uiSystem = GameApp.Interface.GetSystem<UGUISystem>();
        }

        private void OnGameStart(StartGameEvent @event)
        {
            //SceneManager.LoadSceneAsync(SceneTypeExtensions.GetSceneName(SceneType.MapSelection));
        }

        [Button("Test")]
        private void ShowUI()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.MainMenu, null);
            _uiSystem.ShowUI(showUIEvent);
        }

        [Button("Show Popup")]
        private void ShowPopup()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.Popup, null);
            _uiSystem.ShowUI(showUIEvent);
        }
    }
}