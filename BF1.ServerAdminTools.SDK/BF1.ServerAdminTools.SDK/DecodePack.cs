using BF1.ServerAdminTools.SDK.Objs;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK;

internal class DecodePack
{
    public static StateObj State(IByteBuffer buff)
    {
        return new StateObj()
        {
            IsGameRun = buff.ReadByte() == 0xff,
            IsToolInit = buff.ReadByte() == 0xff
        };
    }

    public static IdObj Id(IByteBuffer buff) 
    {
        return new IdObj()
        {
            Remid = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            Sid = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            SessionId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            GameId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            ServerId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            PersistedGameId = buff.ReadString(buff.ReadInt(), Encoding.UTF8)
        };
    }

    public static ServerInfoObj ServerInfo(IByteBuffer buff) 
    {
        return new ServerInfoObj()
        {
            ServerName = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            ServerID = buff.ReadLong(),
            SessionId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            GameId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            ServerId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            PersistedGameId = buff.ReadString(buff.ReadInt(), Encoding.UTF8)
        };
    }
}
