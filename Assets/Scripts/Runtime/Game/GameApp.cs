using Nara.Core.Architecture;
using Nara.Game.Model;
using Nara.Game.System;
using Nara.Patterns;
using Nara.System;
using System;
using UnityEngine;

namespace Nara.Game
{
    public class GameApp : App<GameApp>
    {
        EventBus _eventBus;
        protected override void LateStart()
        {
            _eventBus = Interface.GetSystem<EventBus>();

            //_eventBus.Register(new EventBinding<QuitGameEvent>(OnQuitGame));
        }
        protected override void OnTerminate()
        {
            Debug.Log("OnTerminate");
        }

        private void OnQuitGame(QuitGameEvent @event)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}

