using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace TestFrameSync
{
    class AMessage
    {
        public string _Name;
    }
    class BMessage
    {
        public int _ID;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Run<AMessage>(ACallback);
            Run<BMessage>(BCallback);
            Console.ReadKey();
        }

        static Action<object> _Call;
        static void Run<T>(Action<T> a)
        {
            Console.WriteLine("Run type=" + typeof(T));
            _Call = ConvertToObjAction(a);
            DoSth(typeof(T));
        }

        static void DoSth(Type type)
        {
            if (type == typeof(AMessage))
            {
                _Call(new AMessage { _Name = "Test Name" });
            }
            else if (type == typeof(BMessage))
            {
                _Call(new BMessage { _ID = 10086 });
            }
        }

        static void ACallback(AMessage t)
        {
            Console.WriteLine("ACallback " + t._Name);
        }
        static void BCallback(BMessage t)
        {
            Console.WriteLine("BCallback " + t._ID);
        }

        public static Action<object> ConvertToObjAction<T>(Action<T> tAction)
        {
            if (tAction == null) return null;
            else return new Action<object>(oAction => tAction((T)oAction));
        }
    }
}
