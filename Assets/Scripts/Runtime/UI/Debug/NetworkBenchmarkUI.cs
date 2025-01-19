using Nara.System.Network;
using TMPro;
using UnityEngine;

namespace Nara.UI.Debug
{
    public class NetworkBenchmarkUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pingText;
        [SerializeField] private TextMeshProUGUI _lastHeartBeat;

        [SerializeField] private NetworkManager _networkManager;
        void Update()
        {
            _pingText.text = $"Ping: {_networkManager.NetworkData.Latency}ms";
            _lastHeartBeat.text = $"Last HeartBeat: {_networkManager.NetworkData.LastHeartBeat}";
        }
    }
}

