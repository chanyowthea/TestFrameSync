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
        static void Main(string[] args)
        {
            Facade facade = new Facade();
            facade.Init();
            Console.ReadKey();
        }
    }
}
