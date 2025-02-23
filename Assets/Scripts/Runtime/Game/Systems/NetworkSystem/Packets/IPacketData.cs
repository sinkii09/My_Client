namespace Nara.System.Network
{
    public interface IPacketData
    {
        PacketType Type { get; }
        byte[] Encode(int data, int offset);
        void Decode(byte[] data, int offset);
    }
}