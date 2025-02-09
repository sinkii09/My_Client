using Nara.Core.Architecture;
using Nara.Patterns;
using UnityEngine;

namespace Nara.Core
{
    public class GameApp : App<GameApp>
    {
        public void StartGame()
        {
            Debug.Log("Game Start");
        }
        protected override void Init()
        {
            Debug.Log("Init Game");
        }

        protected override void OnTerminate()
        {
            Debug.Log("OnTerminate");
        }
    }
}

