
using Google.Protobuf;
using Msg;
using System;
using System.Collections.Generic;

namespace Proto
{
   public class ProtoDic
   {
       private static List<int> _protoId = new List<int>
       {
            0,
            1,
            2,
            3,
            4,
        };

      private static List<Type>_protoType = new List<Type>
      {
            typeof(BaseMessage),
            typeof(HeartBeatReq),
            typeof(HeartBeatRes),
            typeof(LoginReq),
            typeof(LoginRes),
       };

       private static readonly Dictionary<RuntimeTypeHandle, MessageParser> Parsers = new Dictionary<RuntimeTypeHandle, MessageParser>()
       {
            {typeof(BaseMessage).TypeHandle,BaseMessage.Parser },
            {typeof(HeartBeatReq).TypeHandle,HeartBeatReq.Parser },
            {typeof(HeartBeatRes).TypeHandle,HeartBeatRes.Parser },
            {typeof(LoginReq).TypeHandle,LoginReq.Parser },
            {typeof(LoginRes).TypeHandle,LoginRes.Parser },
       };

        public static MessageParser GetMessageParser(RuntimeTypeHandle typeHandle)
        {
            MessageParser messageParser;
            Parsers.TryGetValue(typeHandle, out messageParser);
            return messageParser;
        }

        public static Type GetProtoTypeByProtoId(int protoId)
        {
            int index = _protoId.IndexOf(protoId);
            return _protoType[index];
        }

        public static int GetProtoIdByProtoType(Type type)
        {
            int index = _protoType.IndexOf(type);
            return _protoId[index];
        }

        public static bool ContainProtoId(int protoId)
        {
            if(_protoId.Contains(protoId))
            {
                return true;
            }
            return false;
        }

        public static bool ContainProtoType(Type type)
        {
            if(_protoType.Contains(type))
            {
                return true;
            }
            return false;
        }
    }
}