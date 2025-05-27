using Cysharp.Threading.Tasks;
using Nara.Game;
using Nara.Game.Enum;
using Nara.Game.Event;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nara.System.Scene
{
    public class SceneMainMenuController : SceneControllerBase
    {
        public override SceneType SceneType => SceneType.MainMenu;

        private EventBus _eventBusSystem;

        protected override void LateStart()
        {

            RegisterSystem();

            _eventBusSystem.Register<StartGameEvent>(OnGameStart);

            ShowUI();
        }

        private void RegisterSystem()
        {
            _eventBusSystem = GameApp.Interface.GetSystem<EventBus>();
        }

        private void OnGameStart(StartGameEvent evt)
        {
            evt.OnAnimationComplete?.Invoke().ContinueWith(() =>
            {
                Debug.Log("Game started from Main Menu");
            });
        }

        [Button("Test")]
        private void ShowUI()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.MainMenu, null);
            _eventBusSystem.Raise(showUIEvent);
        }

        [Button("Show Popup")]
        private void ShowPopup()
        {
            ShowUIEvent showUIEvent = new ShowUIEvent(UIType.Popup, null);
            _eventBusSystem.Raise(showUIEvent);
        }
    }
}