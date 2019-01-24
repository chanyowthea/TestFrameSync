using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class GameService
{
    UdpClient _Client;
    Thread _ReceiveThread;
    IPEndPoint _TempServerIP = new IPEndPoint(IPAddress.Any, 8000);
    public void Init()
    {
        _Client = new UdpClient(new IPEndPoint(IPAddress.Any, 8000));
        _ReceiveThread = new Thread(ReceiveThread);
    }

    public void Clear()
    {
        if (_Client == null)
        {
            return;
        }

        _Client.Close();
    }

    void ReceiveThread()
    {
        while (true)
        {
            var bs = _Client.Receive(ref _TempServerIP);

        }
    }
}
