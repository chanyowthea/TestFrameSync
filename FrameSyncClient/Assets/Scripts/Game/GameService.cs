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
    IPEndPoint _LocalIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);
    public override void Init()
    {
        base.Init();
        Debug.LogError("GameService.Init");
        _Client = new UdpClient();
        _Client.Connect(_LocalIP);
        //Loom.RunAsync(() =>
        //{
        _ReceiveThread = new Thread(ReceiveThread);
        _ReceiveThread.Start();
        //});
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
            //Debug.LogError("send message typeof(T)=" + typeof(T));
            _Client.Send(bytes, bytes.Length);
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
                var ip = new IPEndPoint(IPAddress.Any, 0);
                var bs = _Client.Receive(ref ip);
                ProcessReceivedMessage(bs);
            }
            catch (Exception ex)
            {
                Debug.Log("ex=" + ex.Message);
            }
        }
    }

    public new void ProcessReceivedMessage(byte[] bytes)
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
                int index = i;
                //Loom.QueueOnMainThread(() =>
                //{
                list[index](target);
                //});
            }
        }
    }
}
