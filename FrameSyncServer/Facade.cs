using Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestFrameSync
{
    public struct GatePlayerInfo
    {
        static int _CurInternalIndex;
        public UserToken _UserToken;
        public int UserId { private set; get; }

        public GatePlayerInfo(UserToken token)
        {
            _UserToken = token;
            _CurInternalIndex++;
            UserId = _CurInternalIndex;
        }
    }

    public struct GamePlayerInfo
    {
        public int _UserId;
        public int _RoomId;
    }

    class Facade
    {
        public static Facade Instance { private set; get; }

        public GateServer _GateServer { private set; get; }
        public GameServer _GameServer { private set; get; }
        List<GameRoom> _GameRooms = new List<GameRoom>();
        Dictionary<int, GatePlayerInfo> _GatePlayerInfos = new Dictionary<int, GatePlayerInfo>();

        public void Init()
        {
            Instance = this;
            StartGateServer();
            StartGameServer();
            _GateServer._OnConnect += OnConnect;
            _GateServer._OnDisconnect += OnDisconnect;
            _GameServer._OnReceiveMsg += OnReceivedGameMsg;
        }

        public void Clear()
        {
            _GateServer._OnConnect -= OnConnect;
            _GateServer._OnDisconnect -= OnDisconnect;
            _GameServer._OnReceiveMsg -= OnReceivedGameMsg;
            StopGameServer();
            StopGateServer();
        }

        void OnConnect(UserToken token)
        {
            var info = new GatePlayerInfo(token);
            _GatePlayerInfos.Add(info.UserId, info);
        }

        void OnDisconnect(TcpClient client)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            int userId = 0;
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken._TcpClient == client)
                {
                    client.Close(); 
                    _GatePlayerInfos.Remove(e.Current.Value.UserId);
                    break; 
                }
            }
        }

        void OnReceivedGameMsg(byte[] msg, IPEndPoint ip)
        {
            // find client
            var e = _GatePlayerInfos.GetEnumerator();
            int userId = 0;
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken._GameIp == ip)
                {
                    userId = e.Current.Value.UserId;
                }
            }
            if (userId != 0)
            {
                for (int i = 0, length = _GameRooms.Count; i < length; i++)
                {
                    var room = _GameRooms[i];
                    for (int j = 0, max = room._UserIds.Length; j < max; j++)
                    {
                        if (room._UserIds[j] == userId)
                        {
                            room.Receive(msg);
                            return; 
                        }
                    }
                }
            }
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
            if (_GatePlayerInfos.Count >= 2)
            {
                Console.WriteLine("match successfully! ");

                List<PlayerInfo> list = new List<PlayerInfo>();
                List<int> userIds = new List<int>();
                var ep = _GatePlayerInfos.GetEnumerator();
                while (ep.MoveNext())
                {
                    var info = new PlayerInfo();
                    info.PlayerId = ep.Current.Value.UserId;
                    info.PlayerName = "PlayerName" + info.PlayerId;
                    info.RoleId = 1;
                    list.Add(info);

                    userIds.Add(ep.Current.Value.UserId);
                }

                // create room
                var room = new GameRoom();
                room.Init(userIds.ToArray());

                // send match info
                var matchRes = new MatchRes();
                matchRes.PlayerInfos.AddRange(list);
                var e = _GatePlayerInfos.GetEnumerator();
                while (e.MoveNext())
                {
                    _GateServer.Send(matchRes, e.Current.Value._UserToken._TcpClient);
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

        public UserToken GetClient(TcpClient client)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken._TcpClient == client)
                {
                    return e.Current.Value._UserToken;
                }
            }
            return default(UserToken);
        }

        public UserToken GetClient(IPEndPoint gateIp)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken.GateIP == gateIp)
                {
                    return e.Current.Value._UserToken;
                }
            }
            return default(UserToken);
        }

        public UserToken GetClient(int userId)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value.UserId == userId)
                {
                    return e.Current.Value._UserToken;
                }
            }
            return default(UserToken);
        }

        void AddClient(UserToken token)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken._TcpClient == token._TcpClient)
                {
                    Console.WriteLine("e.Current.Value._UserToken._TcpClient == token._TcpClient");
                    return;
                }
            }

            GatePlayerInfo info = new GatePlayerInfo(token);
            _GatePlayerInfos.Add(info.UserId, info);
        }

        void RemoveClient(TcpClient client)
        {
            var e = _GatePlayerInfos.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value._UserToken._TcpClient == client)
                {
                    client.Close();
                    _GatePlayerInfos.Remove(e.Current.Key);
                    Console.WriteLine("[INFO]client disconnect! ip address=" + client.Client.RemoteEndPoint);
                    break;
                }
            }
        }

    }
}
