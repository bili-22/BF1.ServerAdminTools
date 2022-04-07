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
        var obj = new ServerInfoObj()
        {
            ServerName = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            ServerID = buff.ReadLong(),
            ServerTime = buff.ReadFloat(),
            ServerTimeS = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            Team1Score = buff.ReadInt(),
            Team2Score = buff.ReadInt(),
            Team1FromeKill = buff.ReadInt(),
            Team2FromeKill = buff.ReadInt(),
            Team1FromeFlag = buff.ReadInt(),
            Team2FromeFlag = buff.ReadInt(),
            Team1MaxPlayerCount = buff.ReadInt(),
            Team1PlayerCount = buff.ReadInt(),
            Team1Rank150PlayerCount = buff.ReadInt(),
            Team1AllKillCount = buff.ReadInt(),
            Team1AllDeadCount = buff.ReadInt(),
            Team2MaxPlayerCount = buff.ReadInt(),
            Team2PlayerCount = buff.ReadInt(),
            Team2Rank150PlayerCount = buff.ReadInt(),
            Team2AllKillCount = buff.ReadInt(),
            Team2AllDeadCount = buff.ReadInt()
        };
        if (buff.ReadByte() == 0xff)
        {
            obj.MapName = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
            obj.MapUrl = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
            obj.TeamOne = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
            obj.TeamOneUrl = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
            obj.TeamTwo = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
            obj.TeamTwoUrl = buff.ReadString(buff.ReadInt(), Encoding.UTF8);
        }

        return obj;
    }

    public static PlayerDataObj Player(IByteBuffer buff) 
    {
        return new PlayerDataObj()
        {
            Admin = buff.ReadBoolean(),
            VIP = buff.ReadBoolean(),
            Mark = buff.ReadByte(),
            TeamID = buff.ReadInt(),
            Spectator = buff.ReadByte(),
            Clan = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            Name = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            PersonaId = buff.ReadLong(),
            SquadId = buff.ReadString(buff.ReadInt(), Encoding.UTF8),
            Rank = buff.ReadInt(),
            Dead = buff.ReadInt(),
            Score = buff.ReadInt(),
            KD = buff.ReadFloat(),
            KPM = buff.ReadFloat(),
            WeaponS0CH = buff.ReadString(buff.ReadInt(), Encoding.UTF8)
        };
    }

    public static List<PlayerDataObj> PlayerList(IByteBuffer buff)
    {
        var list = new List<PlayerDataObj>();
        for (int a = 0; a < buff.ReadInt(); a++)
        {
            list.Add(Player(buff));
        }
        return list;
    }

    public static ScoreInfoObj ServerScore(IByteBuffer buff) 
    {
        return new ScoreInfoObj()
        {
            Info = ServerInfo(buff),
            Team1 = buff.ReadByte() == 0xff ? PlayerList(buff) : new(),
            Team2 = buff.ReadByte() == 0xff ? PlayerList(buff) : new(),
            Team3 = buff.ReadByte() == 0xff ? PlayerList(buff) : new()
        };
    }
}
