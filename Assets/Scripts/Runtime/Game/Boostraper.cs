using UnityEngine;

namespace Nara.Game
{
    public class Boostraper : MonoBehaviour
    {
        private void Awake()
        {
            GameApp.Instance.StartGame();
        }

    }
}

