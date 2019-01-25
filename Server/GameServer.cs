using Google.Protobuf;
using Msg;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFrameSync
{
    class GameServer : BaseServer
    {
        List<UserToken> _PlayerTokens = new List<UserToken>();
        UdpClient _Server;
        Thread _ReceiveThread;
        IPEndPoint _TempServerIP = new IPEndPoint(IPAddress.Any, 8000);
        public event Action<byte[]> _OnReceiveMsg;

        public void Start(UserToken[] tokens)
        {
            base.Start();
            _Server = new UdpClient(new IPEndPoint(IPAddress.Any, 8000));
            _ReceiveThread = new Thread(ReceiveThread);
            _PlayerTokens.AddRange(tokens);
        }

        public new void Stop()
        {
            base.Stop();
            if (_Server == null)
            {
                return;
            }
            _PlayerTokens.Clear();
            _ReceiveThread.Abort();
            _Server.Close();
        }

        public void Send<T>(T message)
            where T : IMessage
        {
            if (_Server == null)
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
                _Server.Send(bytes, bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex=" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes">这个bytes必须是客户端发来的原样数据</param>
        /// <param name="receiveUserIds"></param>
        public void Send(byte[] bytes, params int[] receiveUserIds)
        {
            if (_Server == null)
            {
                return;
            }
            try
            {
                for (int i = 0, length = receiveUserIds.Length; i < length; i++)
                {
                    var token = Facade.Instance._GateServer.GetClient(receiveUserIds[i]);
                    if (token != null)
                    {
                        Console.WriteLine("send message to token._IP=" + token._IP);
                        _Server.Send(bytes, bytes.Length, token._IP as IPEndPoint);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex=" + ex.Message);
            }
        }

        void ReceiveThread()
        {
            while (true)
            {
                try
                {
                    var bs = _Server.Receive(ref _TempServerIP);
                    if (_OnReceiveMsg != null)
                    {
                        _OnReceiveMsg(bs);
                    }
                    ProcessReceivedMessage(bs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ex=" + ex.Message);
                }
            }
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
}
