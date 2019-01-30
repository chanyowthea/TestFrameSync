using Google.Protobuf;
using Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFrameSync
{
    class GameRoom
    {
        static int _InternalRoomIndex;

        public int RoomId { private set; get; }
        public int[] _UserIds { private set; get; }
        Timer _Timer;
        Queue<byte[]> _MessageQueue = new Queue<byte[]>();
        int _FrameNumber;
        Dictionary<int, IPEndPoint> _ReadyPlayers = new Dictionary<int, IPEndPoint>();

        public GameRoom()
        {
            _InternalRoomIndex++;
            RoomId = _InternalRoomIndex;
        }

        public void Init(int[] userIds)
        {
            _UserIds = userIds;
            // 一秒10帧，服务器上的帧率要比客户端低才对，然后客户端做插值
            _FrameNumber = 1;
        }

        public void PlayerReady(int userId, IPEndPoint ip)
        {
            if (!_ReadyPlayers.ContainsKey(userId))
            {
                _ReadyPlayers.Add(userId, ip);
            }
            if (_ReadyPlayers.Count == _UserIds.Length)
            {
                Start(); 
            }
        }

        public void Start()
        {
            _Timer = new Timer(TimerFunc, null, 0, 100);
        }

        public void Clear()
        {
            _FrameNumber = 0;
            _Timer.Dispose();
            _Timer = null;
            _UserIds = null;
        }

        public void Receive(byte[] msg)
        {
            lock (_MessageQueue)
            {
                _MessageQueue.Enqueue(msg);
            }
        }

        void TimerFunc(object obj)
        {
            lock (_MessageQueue)
            {
                UDPFrameData data = new UDPFrameData();
                data.FrameNumber = _FrameNumber;
                Console.WriteLine("====================_MessageQueue.Count=" + _MessageQueue.Count);
                while (_MessageQueue.Count > 0)
                {
                    var msg = _MessageQueue.Dequeue();
                    data.Msgs.Add(ByteString.CopyFrom(msg));
                }
                Facade.Instance._GameServer.Send(data, _UserIds);
                _FrameNumber++;
            }
        }
    }
}
