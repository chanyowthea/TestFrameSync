using Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestFrameSync
{
    class Facade
    {
        public static Facade Instance { private set; get; }

        public GateServer _GateServer { private set; get; }
        public GameServer _GameServer { private set; get; }
        Dictionary<TcpClient, GameRoom> _GameRooms = new Dictionary<TcpClient, GameRoom>();

        public void Init()
        {
            Instance = this;
            StartGateServer();
        }

        public void Clear()
        {
            StopGateServer();
        }

        void StartGateServer()
        {
            _GateServer = new GateServer();
            _GateServer.Start();
            _GateServer.AddCallback<LoginReq>(LoginReqCallback);
        }

        void StopGateServer()
        {
            _GateServer.RemoveCallback<LoginReq>(LoginReqCallback);
            _GateServer.Stop();
        }

        void LoginReqCallback(LoginReq message, TcpClient client)
        {
            Console.WriteLine("login req name=" + message.AccountName);
            _GateServer.Send(new LoginRes { Rs = true }, client);

            // TODO 目前只能是两个人进行测试
            if (_GateServer._Clients.Count >= 2)
            {
                Console.WriteLine("match successfully! ");

                List<PlayerInfo> list = new List<PlayerInfo>();
                var ep = _GateServer._Clients.GetEnumerator();
                while (ep.MoveNext())
                {
                    var info = new PlayerInfo();
                    info.PlayerId = ep.Current.Value.UserId;
                    info.PlayerName = "PlayerName" + info.PlayerId;
                    info.RoleId = 1;
                    list.Add(info);
                }

                var e = _GateServer._Clients.GetEnumerator();
                while (e.MoveNext())
                {
                    bool joinRoomRs = !_GameRooms.ContainsKey(e.Current.Key);
                    var matchRes = new MatchRes();
                    if (joinRoomRs)
                    {
                        matchRes.PlayerInfos.AddRange(list);
                        _GateServer.Send(matchRes, e.Current.Key);
                        var room = new GameRoom();
                        room.Init();
                        _GateServer.GetClient(client);
                        _GameRooms.Add(client, room);
                    }
                    else
                    {
                        _GateServer.Send(matchRes, e.Current.Key);
                    }
                }
            }
        }

        void StartGameServer()
        {
            _GameServer = new GameServer();
            _GameServer.Start();
        }

        void StopGameServer()
        {
            _GameServer.Stop();
        }
    }
}
