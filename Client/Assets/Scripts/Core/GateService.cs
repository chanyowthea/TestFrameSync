using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.Net;
using System;
using Google.Protobuf;
using Proto;
using Msg;
using System.Linq;

public class GateService : BaseService
{
    TcpClient _TcpClient;
    byte[] _Buffer;
    public override void Init()
    {
        _Buffer = new byte[1024 * 1024 * 2];

        _TcpClient = new TcpClient(new IPEndPoint(IPAddress.Any, 8000));
        try
        {
            _TcpClient.Connect("127.0.0.1", 8000);
            _TcpClient.GetStream().BeginRead(_Buffer, 0, _Buffer.Length, ReceiveCallback, _TcpClient);
        }
        catch (Exception ex)
        {
            Debug.Log("ex=" + ex.Message);
        }
    }

    public override void Clear()
    {
        if (_TcpClient == null)
        {
            return;
        }

        _TcpClient.Close();
    }

    public override void Send<T>(T message)
    {
        if (_TcpClient == null)
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
            _TcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }
        catch (Exception ex)
        {
            Debug.Log("ex=" + ex.Message);
        }
    }

    void ReceiveCallback(IAsyncResult result)
    {
        var client = result.AsyncState as TcpClient;
        try
        {
            int receivedSize = client.GetStream().EndRead(result);
            if (receivedSize <= _Buffer.Length)
            {
                ProcessReceivedMessage(_Buffer.Take(receivedSize).ToArray());
                client.GetStream().BeginRead(_Buffer, 0, _Buffer.Length, ReceiveCallback, client);
            }
            else
            {
                Console.WriteLine("received size is out of range! ");
            }

            client.GetStream().BeginRead(_Buffer, 0, _Buffer.Length, ReceiveCallback, client);
        }
        catch (Exception ex)
        {
            Debug.Log("ex=" + ex.Message);
        }
    }
}
