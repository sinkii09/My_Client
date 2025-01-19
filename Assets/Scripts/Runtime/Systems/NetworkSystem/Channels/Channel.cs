using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace Nara.System.Network
{
    public class Channel : IChannel
    {
        private const int HeartBeatInterval = 3000;
        private Timer _heartBeatTimer;

        public IPAddress IpV4;
        public int Port;
        public TcpClient Client { get; private set; }
        public NetworkStream Stream { get; private set; }
        public ConcurrentQueue<PacketMessage> SendQueue { get; set; }
        public ConcurrentQueue<PacketMessage> ReceiveQueue { get; set; }

        private Thread _sendThread;
        private Thread _receiveThread;

        private readonly PacketProcessHandler _packetProcessHandler;
        public Channel(PacketProcessHandler packetProcessHandler)
        {
            _packetProcessHandler = packetProcessHandler;
        }
        public bool Connect(string ipAddress, int port)
        {
            IpV4 = IPAddress.Parse(ipAddress);
            Port = port;
            Client = new TcpClient();
            try
            {
                    
                Client.Connect(ipAddress, Port);
                Stream = Client.GetStream();

                SendQueue = new ConcurrentQueue<PacketMessage>();
                ReceiveQueue = new ConcurrentQueue<PacketMessage>();

                _sendThread = new Thread(SendAsync);
                _receiveThread = new Thread(ReceiveAsync);

                _sendThread.Start();
                _receiveThread.Start();

                _heartBeatTimer = new Timer(SendHeartbeat,null,0,HeartBeatInterval);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }

        }

        private void SendHeartbeat(object state)
        {
            IPacketData data = new HeartBeatPacket();
            Send(data);
            Debug.Log($"[Network system] : Send heart beat at {DateTime.Now}");
        }

        public void Disconnect()
        {
            Client.Close();
            Client.Dispose();

            _sendThread.Abort();
            _receiveThread.Abort();

            _heartBeatTimer?.Dispose();
        }

        public void Update()
        {
            Process();
        }
        public void Send(IPacketData data)
        {
            var message = new PacketMessage(data);
            SendQueue.Enqueue(message);
        }
        public void Receive()
        {
        }

        public void SendAsync()
        {
            while (Client.Connected)
            {
                try
                {
                    if (!SendQueue.TryDequeue(out var packetMessage)) continue;
                    var buffer = packetMessage.Encode();
                    Stream.Write(buffer, 0, buffer.Length);
                }
                catch (IOException ex)
                {
                    Debug.LogError($"IOException: {ex.Message}");
                    Disconnect();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception: {ex.Message}");
                    Disconnect();
                }
            }
        }
        public void ReceiveAsync()
        {
            while (Client.Connected)
            {
                if (!Stream.CanRead || !Stream.DataAvailable) continue;
                var buffer = new byte[1028];
                var readBytes = Stream.Read(buffer, 0, buffer.Length);

                if (readBytes <= 0) continue;
                var packetMessage = new PacketMessage();
                packetMessage.Decode(buffer);
                ReceiveQueue.Enqueue(packetMessage);
            }
        }
        public void Process()
        {
            if (ReceiveQueue.TryDequeue(out var message))
            {
                Debug.Log($"Process a message {message.Type}");
                _packetProcessHandler.Process(message);
            }
        }
    }
}