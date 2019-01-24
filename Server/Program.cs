using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Msg;

namespace TestFrameSync
{
    class Program
    {
        static GateServer _GateServer;
        static void Main(string[] args)
        {
            _GateServer = new GateServer();
            _GateServer.Start();
            _GateServer.AddCallback<LoginReq>(LoginReqCallback);
            Console.ReadKey();
            _GateServer.Stop();
        }

        static void LoginReqCallback(LoginReq message, TcpClient client)
        {
            Console.WriteLine("login req name=" + message.AccountName);
            _GateServer.Send(new LoginRes { Rs = true }, client);

            if (_GateServer._Clients.Count >= 2)
            {
                Console.WriteLine("match successfully! ");
                var e = _GateServer._Clients.GetEnumerator();
                while (e.MoveNext())
                {
                    _GateServer.Send(new MatchRes(), e.Current.Key);
                }
            }
        }
    }
}
