using System.Net.Sockets;
using UnityEngine;

namespace Nara.System.Network
{
    public interface IPacketProcessor
    {
        void Process(PacketMessage message);
    }
    public class NetworkManager : MonoBehaviour
    {
        public NetworkData NetworkData { get; private set; } = new NetworkData();
        public Channel MainSocket { get; private set; }

        private readonly PacketProcessHandler _packetProcessHandler = new PacketProcessHandler();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MainSocket = new Channel(_packetProcessHandler);
            MainSocket.Connect("192.168.1.9", 3000);

            RegisterProcessor(NetworkData);
            //PacketMessage message = new PacketMessage();
            //MainSocket.Send(message);
        }
        void Update()
        {
            MainSocket?.Update();
        }
        void OnDestroy()
        {
            MainSocket?.Disconnect();
        }
        private bool StartConnect()
        {
            return true;
        }

        public void RegisterProcessor(IPacketProcessor processor)
        {
            _packetProcessHandler.RegisterProcessor(processor);
        }
        public void UnregisterProcessor(IPacketProcessor processor)
        {
            _packetProcessHandler.UnregisterProcessor(processor);
        }
    }

}

