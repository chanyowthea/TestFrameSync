using Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestFrameSync
{
    class GameRoom
    {
        static
        public int RoomId
        { private set; get; }
        int[] _UserIds;

        public void Init(int[] userIds)
        {
            _UserIds = userIds;
        }

        public void Clear()
        {
            _UserIds = null;
        }

        public void Receive(byte[] msg)
        {
            Facade.Instance._GameServer.Send(msg, _UserIds);
        }
    }
}
