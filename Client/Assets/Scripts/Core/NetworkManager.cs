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

public class NetworkManager
{
    TcpClient _TcpClient;
    byte[] _Buffer;
    public void Init()
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

    public void Clear()
    {
        if (_TcpClient == null)
        {
            return;
        }

        _TcpClient.Close();
    }

    public void Send<T>(T message)
        where T : IMessage
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
    public void ProcessReceivedMessage(byte[] bytes)
    {
        // parse to base message. 
        var message = BaseMessage.Parser.ParseFrom(bytes);

        // get target message info. 
        var type = ProtoDic.GetProtoTypeByProtoId(message.Id);
        MessageParser messageParser = ProtoDic.GetMessageParser(type.TypeHandle);

        // convert to target message object. 
        object target = messageParser.ParseFrom(message.Data);

        List<Action<object>> list = null;
        if (_MessageCallbacks.TryGetValue(type, out list))
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("list == null || list.Count == 0");
                return;
            }

            for (int i = 0, length = list.Count; i < length; i++)
            {
                list[i](target);
            }
        }
    }

    public Action<object> ConvertToObjAction<T>(Action<T> tAction)
        where T : IMessage
    {
        if (tAction == null) return null;
        else return new Action<object>(obj => tAction((T)obj));
    }

    Dictionary<Type, List<Action<object>>> _MessageCallbacks = new Dictionary<Type, List<Action<object>>>();

    public void AddCallback<T>(Action<T> callback)
        where T : IMessage
    {
        var type = typeof(T);
        if (!_MessageCallbacks.ContainsKey(type))
        {
            var list = new List<Action<object>>();
            _MessageCallbacks.Add(type, list);
        }
        _MessageCallbacks[type].Add(ConvertToObjAction(callback));
    }

    public void RemoveCallback<T>(Action<T> callback)
        where T : IMessage
    {
        var type = typeof(T);
        List<Action<object>> list = null;
        if (_MessageCallbacks.TryGetValue(type, out list))
        {
            var call = ConvertToObjAction(callback);
            if (list.Contains(call))
            {
                list.Remove(call);
            }
        }
    }
}
