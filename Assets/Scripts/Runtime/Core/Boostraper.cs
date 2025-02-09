using UnityEngine;

namespace Nara.Core
{
    public class Boostraper : MonoBehaviour
    {
        private void Start()
        {
            GameApp.Instance.StartGame();
        }

    }
}

