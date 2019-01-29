using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Msg;
using Google.Protobuf;
using Proto;

public class GameService : BaseService
{
    UdpClient _Client;
    Thread _ReceiveThread;
    IPEndPoint _TempServerIP = new IPEndPoint(IPAddress.Any, 0);
    public override void Init()
    {
        base.Init();
        Debug.LogError("GameService.Init");
        _Client = new UdpClient();
        _ReceiveThread = new Thread(ReceiveThread);
    }

    public override void Clear()
    {
        base.Clear();
        if (_Client == null)
        {
            return;
        }
        _ReceiveThread.Abort();
        _Client.Close();
    }

    public override void Send<T>(T message)
    {
        if (_Client == null)
        {
            return;
        }
        BaseMessage m = new BaseMessage();
        m.Id = ProtoDic.GetProtoIdByProtoType(typeof(T));
        byte[] bytes = message.ToByteArray();
        m.Data = ByteString.CopyFrom(bytes);
        try
        {
            bytes = m.ToByteArray();
            Debug.LogError("send message typeof(T)=" + typeof(T));
            _Client.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001));
        }
        catch (Exception ex)
        {
            Debug.Log("ex=" + ex.Message);
        }
    }

    void ReceiveThread()
    {
        while (true)
        {
            try
            {
                var bs = _Client.Receive(ref _TempServerIP);
                ProcessReceivedMessage(bs);
            }
            catch (Exception ex)
            {
                Debug.Log("ex=" + ex.Message);
            }
        }
    }
}
