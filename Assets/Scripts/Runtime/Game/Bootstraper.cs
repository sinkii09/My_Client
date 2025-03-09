using Nara.Patterns;
using UnityEngine;

namespace Nara.Game
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField]
        private ServiceConfigSO serviceConfig;

        private void Awake()
        {
            GameApp.Instance.StartGame(serviceConfig);
        }
    }
}

