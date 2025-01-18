using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Test_Client : MonoBehaviour
{
    private TcpClient tcpClient;
    private IPAddress _ipAddress;
    private int _port = 3000;
    private void Start()
    {
        tcpClient = new TcpClient();

        //var hostname = Dns.GetHostName();
        //IPHostEntry entry = Dns.GetHostEntry(hostname);
        //Debug.Log($"{hostname}");

        //IPAddress iPAddress = entry.AddressList[0];
        //foreach(var adr in entry.AddressList)
        //{
        //    if(adr.AddressFamily == AddressFamily.InterNetwork)
        //    {
        //        iPAddress = adr; break;
        //    }
        //}
        _ipAddress = IPAddress.Parse("192.168.1.9");
        IPEndPoint endPoint = new IPEndPoint(_ipAddress, _port);
        Debug.Log("Client try connecting");
        try
        {
            if (tcpClient.Connected) return;
            tcpClient.Connect(endPoint);
            Debug.Log(tcpClient.Connected);
        }
        catch (SocketException e)
        {
            Debug.LogException(e);
        } 
    }
    private void OnDestroy()
    {
        tcpClient.Close();
    }
}
