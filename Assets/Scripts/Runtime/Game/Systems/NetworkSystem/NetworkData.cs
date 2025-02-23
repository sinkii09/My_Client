using System;
using System.Timers;
using UnityEngine;

namespace Nara.System.Network
{
    [Serializable]
    public class NetworkData : IPacketProcessor
    { 
        public int Latency { get; private set; }
        public DateTime LastHeartBeat { get; private set; }
        public double UploadSpeed { get; private set; }
        public double DownloadSpeed { get; private set; }

        public NetworkData()
        {
            Latency = 0;
            LastHeartBeat = DateTime.Now;
            UploadSpeed = 0;
            DownloadSpeed = 0;

        }
        public void Process(PacketMessage message)
        {
            if (message.Type == PacketType.HeartBeat)
            {
                LastHeartBeat = DateTime.Now;
                if (message.Data is HeartBeatPacket data) 
                    Latency = DateTime.Now.Millisecond - data.Timestamp.Millisecond - 3;
            }
        }

    }
}