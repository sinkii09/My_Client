using System.Collections.Concurrent;

namespace Nara.System.Network
{
    public interface IChannel
    {
        ConcurrentQueue<PacketMessage> SendQueue { get; set; }
        ConcurrentQueue<PacketMessage> ReceiveQueue { get; set; }
        bool Connect(string ipAddress, int port);
        void Disconnect();
        void Send(IPacketData data);
        void Receive();
    }
}