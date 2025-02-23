

using System;

namespace Nara.System.Network
{
    public enum PacketType
    {
        None,
        HeartBeat,
        Data,
        Acknowledge,
        Disconnect
    }

    public class HeartBeatPacket : IPacketData
    {
        public PacketType Type { get; } = PacketType.HeartBeat;
        public DateTime Timestamp { get; private set; } = DateTime.Now;
        public byte[] Encode(int data, int offset)
        {
            var timestampBytes = BitConverter.GetBytes(Timestamp.ToBinary());
            return timestampBytes;
        }

        public void Decode(byte[] data, int offset)
        {
            Timestamp = DateTime.FromBinary(BitConverter.ToInt64(data, offset));
        }
    }
}