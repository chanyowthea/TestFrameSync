using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using Msg;
using Google.Protobuf;
using Proto;
using System.Reflection;

namespace TestFrameSync
{
    class UserToken
    {

    }

    class GateServer
    {
        //Dictionary<TcpClient, > _TcpClinets = new ;
        // the maximum packet is 2 m. the packet whose size is out of the maximum will not be processed. 
        byte[] _Buffer;
        TcpListener _Server;
        public void Start()
        {
            //LoginReq req = new LoginReq();
            //req.ToByteArray();
            //byte[] bs = new byte[1024];
            //LoginReq.Parser.ParseFrom(bs);

            _Buffer = new byte[1024 * 1024 * 2];
            _Server = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
            _Server.BeginAcceptTcpClient(AcceptCallback, _Server);
        }

        void AcceptCallback(IAsyncResult result)
        {
            var server = result.AsyncState as TcpListener;
            try
            {
                var client = server.EndAcceptTcpClient(result);
                client.GetStream().BeginRead(_Buffer, 0, _Buffer.Length, ReceiveCallback, client);
                server.BeginAcceptTcpClient(AcceptCallback, server);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex=" + ex.Message);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex=" + ex.Message);
            }
        }

        public void Stop()
        {
            if (_Server == null)
            {
                return;
            }
            try
            {
                _Server.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex=" + ex.Message);
            }
            finally
            {
                _Server = null;
            }
        }

        //public void SendMessage(IMessage obj)
        //{
        //    if (!ProtoDic.ContainProtoType(obj.GetType()))
        //    {
        //        Console.WriteLine("不存协议类型");
        //        return;
        //    }
        //    int protoId = ProtoDic.GetProtoIdByProtoType(obj.GetType());
        //    byte[] result = obj.ToByteArray();
        //    Console.WriteLine("lengh" + result.Length + ",protoId" + protoId);
        //}

        //public void DispatchProto(int protoId, byte[] buff)
        //{
        //    if (!ProtoDic.ContainProtoId(protoId))
        //    {
        //        Console.WriteLine("未知协议号");
        //        return;
        //    }
        //    Type protoType = ProtoDic.GetProtoTypeByProtoId(protoId);
        //    try
        //    {
        //        MessageParser messageParser = ProtoDic.GetMessageParser(protoType.TypeHandle);
        //        object toc = messageParser.ParseFrom(buff);
        //        //sEvents.Enqueue(new KeyValuePair<Type, object>(protoType, toc));
        //    }
        //    catch
        //    {
        //        Console.WriteLine("DispatchProto Error:" + protoType.ToString());
        //    }
        //}


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
            else return new Action<object>(oAction => tAction((T)oAction));
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
}
