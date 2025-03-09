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
        protected override void OnTerminate()
        {
            Debug.Log("OnTerminate");
        }
    }
}

