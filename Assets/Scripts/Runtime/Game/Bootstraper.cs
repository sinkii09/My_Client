using Nara.Game.Config;
using UnityEngine;

namespace Nara.Game
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField]
        private GlobalConfig _globalConfig;

        private void Awake()
        {
            GameApp.Instance.StartGame(_globalConfig);
        }
    }
}

