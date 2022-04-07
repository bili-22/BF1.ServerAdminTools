using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Data;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Netty;

internal class BuildPack
{
    public static void State(IByteBuffer buff) 
    {
        buff.WriteByte(Globals.IsGameRun ? 0xff : 0x00);
        buff.WriteByte(Globals.IsToolInit ? 0xff : 0x00);
    }

    public static void Check(IByteBuffer buff) 
    {
        buff.WriteByte(Core.IsGameRun() ? 0xff : 0x00);
        buff.WriteByte(Core.HookInit() ? 0xff : 0x00);
    }

    public static void Id(IByteBuffer buff) 
    {
        buff.WriteInt(Globals.Config.Remid.Length);
        buff.WriteString(Globals.Config.Remid, Encoding.UTF8);
        buff.WriteInt(Globals.Config.Sid.Length);
        buff.WriteString(Globals.Config.Sid, Encoding.UTF8);
        buff.WriteInt(Globals.Config.SessionId.Length);
        buff.WriteString(Globals.Config.SessionId, Encoding.UTF8);
        buff.WriteInt(Globals.Config.GameId.Length);
        buff.WriteString(Globals.Config.GameId, Encoding.UTF8);
        buff.WriteInt(Globals.Config.ServerId.Length);
        buff.WriteString(Globals.Config.ServerId, Encoding.UTF8);
        buff.WriteInt(Globals.Config.PersistedGameId.Length);
        buff.WriteString(Globals.Config.PersistedGameId, Encoding.UTF8);
    }

    public static void ServerInfo(IByteBuffer buff) 
    {
        buff.WriteInt(Globals.ServerInfo.ServerName.Length);
        buff.WriteString(Globals.ServerInfo.ServerName, Encoding.UTF8);
        buff.WriteLong(Globals.ServerInfo.ServerID);
        buff.WriteFloat(Globals.ServerInfo.ServerTime);
        buff.WriteInt(Globals.ServerInfo.ServerTimeS.Length);
        buff.WriteString(Globals.ServerInfo.ServerTimeS, Encoding.UTF8);
        buff.WriteInt(Globals.ServerInfo.Team1Score);
        buff.WriteInt(Globals.ServerInfo.Team2Score);
        buff.WriteInt(Globals.ServerInfo.Team1FromeKill);
        buff.WriteInt(Globals.ServerInfo.Team2FromeKill);
        buff.WriteInt(Globals.ServerInfo.Team1FromeFlag);
        buff.WriteInt(Globals.ServerInfo.Team2FromeFlag);
        buff.WriteInt(Globals.StatisticData_Team1.MaxPlayerCount);
        buff.WriteInt(Globals.StatisticData_Team1.PlayerCount);
        buff.WriteInt(Globals.StatisticData_Team1.Rank150PlayerCount);
        buff.WriteInt(Globals.StatisticData_Team1.AllKillCount);
        buff.WriteInt(Globals.StatisticData_Team1.AllDeadCount);
        buff.WriteInt(Globals.StatisticData_Team2.MaxPlayerCount);
        buff.WriteInt(Globals.StatisticData_Team2.PlayerCount);
        buff.WriteInt(Globals.StatisticData_Team2.Rank150PlayerCount);
        buff.WriteInt(Globals.StatisticData_Team2.AllKillCount);
        buff.WriteInt(Globals.StatisticData_Team2.AllDeadCount);
    }

    public static void Player(IByteBuffer buff, PlayerData player) 
    {
        buff.WriteBoolean(player.Admin);
        buff.WriteBoolean(player.VIP);
        buff.WriteByte(player.Mark);
        buff.WriteInt(player.TeamID);
        buff.WriteByte(player.Spectator);
        buff.WriteInt(player.Clan.Length);
        buff.WriteString(player.Clan, Encoding.UTF8);
        buff.WriteInt(player.Name.Length);
        buff.WriteString(player.Name, Encoding.UTF8);
        buff.WriteLong(player.PersonaId);
        buff.WriteInt(player.SquadId.Length);
        buff.WriteString(player.SquadId, Encoding.UTF8);
        buff.WriteInt(player.Rank);

    }
}
