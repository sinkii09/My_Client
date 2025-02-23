using Nara.Core.Architecture;
using Nara.Game.Model;
using Nara.Game.System;
using Nara.Patterns;
using UnityEngine;

namespace Nara.Game
{
    public class GameApp : App<GameApp>
    {
        #region Unity Callbacks

        #endregion
        public void StartGame()
        {
            Debug.Log("Game Start");
        }
        protected override void Init()
        {
            RegisterModel(new UserGlobalData());

            RegisterSystem(new ResourceSystem());
            RegisterSystem(new GachaSystem());
            RegisterSystem(new HttpService());
            RegisterSystem(new AgentManagementSystem());
        }

        protected override void OnTerminate()
        {
            Debug.Log("OnTerminate");
        }
    }
}

