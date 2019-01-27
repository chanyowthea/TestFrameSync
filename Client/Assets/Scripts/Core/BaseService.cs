using Google.Protobuf;
using Msg;
using Proto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseService
{
    public virtual void Init()
    {

    }

    public virtual void Clear()
    {

    }

    public virtual void Send<T>(T message)
        where T : IMessage
    {

    }

    public virtual void ProcessReceivedMessage(byte[] bytes)
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
                Loom.QueueOnMainThread(() =>
                {
                    list[index](target);
                });
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
