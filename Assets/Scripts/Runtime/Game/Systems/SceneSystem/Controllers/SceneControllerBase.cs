using Nara.Game.Enum;
using UnityEngine;

namespace Nara.System.Scene
{
    public abstract class SceneControllerBase : NaraBehaviour
    {
        public abstract SceneType SceneType { get; }
    }
}