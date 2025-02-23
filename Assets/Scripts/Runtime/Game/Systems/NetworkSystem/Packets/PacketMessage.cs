using System;
using UnityEngine;

namespace Nara.System.Network
{
    public class PacketMessage
    {
        private readonly int _headerBufferSize = 4;
        private readonly int _dataBufferSize = 1024;
        public byte[] Buffer { get; }
        public PacketType Type { get; set; }
        public IPacketData Data { get; set; }
        public PacketMessage(IPacketData data = null)
        {
            Type = data?.Type ?? PacketType.None;
            Data = data;
            Buffer = new byte[_headerBufferSize + _dataBufferSize];
        }
        public byte[] Encode()
        {

            var header = new byte[_headerBufferSize];
            header[0] = 0xAA;
            header[1] = (byte)Type;
            header[2] = (byte)(_dataBufferSize & 0xFF);
            header[3] = (byte)((_dataBufferSize >> 8) & 0xFF);

            Array.Copy(header,Buffer,_headerBufferSize);
            byte[] data = EncodeHelper();
            Array.Copy(data,0,Buffer,_headerBufferSize,_dataBufferSize);
            return Buffer;
        }
        public void Decode(byte[] buffer)
        {
            var header = new byte[_headerBufferSize];

            Array.Copy(buffer,header,_headerBufferSize);
            Type = (PacketType)header[1];
            Debug.Log(Type.ToString());
            int dataSize = header[2] | (header[3] << 8);
            byte[] data = new byte[dataSize];

            Array.Copy(buffer,_headerBufferSize,data,0,dataSize);
            Data = DecodeHelper(data);
        }

        private byte[] EncodeHelper()
        {
            return new byte[1024];
        }
        private IPacketData DecodeHelper(byte[] data)
        {
            switch (Type)
            {
                case PacketType.HeartBeat:
                    var packet = new HeartBeatPacket();
                    packet.Decode(data, 0);
                    return packet;
            }
            return null;
        }
    }
}