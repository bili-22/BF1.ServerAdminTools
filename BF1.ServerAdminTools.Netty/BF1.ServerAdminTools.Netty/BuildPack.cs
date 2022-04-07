using BF1.ServerAdminTools.Common;
using BF1.ServerAdminTools.Common.Data;
using DotNetty.Buffers;
using System.Text;

namespace BF1.ServerAdminTools.Netty;

internal static class BuildPack
{
    public static void WriteString(this IByteBuffer buff, string data)
    {
        byte[] temp = Encoding.UTF8.GetBytes(data);
        buff.WriteInt(temp.Length);
        buff.WriteBytes(temp);
    }
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
        buff.WriteString(Globals.Config.Remid);
        buff.WriteString(Globals.Config.Sid);
        buff.WriteString(Globals.Config.SessionId);
        buff.WriteString(Globals.Config.GameId);
        buff.WriteString(Globals.Config.ServerId);
        buff.WriteString(Globals.Config.PersistedGameId);
    }

    public static void ServerInfo(IByteBuffer buff)
    {
        buff.WriteString(Globals.ServerHook.ServerName);
        buff.WriteLong(Globals.ServerHook.ServerID);
        buff.WriteFloat(Globals.ServerHook.ServerTime);
        buff.WriteString(Globals.ServerHook.ServerTimeS);
        buff.WriteInt(Globals.ServerHook.Team1Score);
        buff.WriteInt(Globals.ServerHook.Team2Score);
        buff.WriteInt(Globals.ServerHook.Team1FromeKill);
        buff.WriteInt(Globals.ServerHook.Team2FromeKill);
        buff.WriteInt(Globals.ServerHook.Team1FromeFlag);
        buff.WriteInt(Globals.ServerHook.Team2FromeFlag);
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
        if (Globals.ServerDetailed != null)
        {
            buff.WriteBoolean(true);
            buff.WriteString(Globals.ServerDetailed.currentMap);
            buff.WriteString(Globals.ServerDetailed.currentMapImage);
            buff.WriteString(Globals.ServerDetailed.teams.teamOne.key);
            buff.WriteString(Globals.ServerDetailed.teams.teamOne.image);
            buff.WriteString(Globals.ServerDetailed.teams.teamTwo.key);
            buff.WriteString(Globals.ServerDetailed.teams.teamTwo.image);
        }
        else
        {
            buff.WriteByte(0);
        }
    }

    public static void Player(IByteBuffer buff, PlayerData player)
    {
        buff.WriteBoolean(player.Admin);
        buff.WriteBoolean(player.VIP);
        buff.WriteByte(player.Mark);
        buff.WriteInt(player.TeamID);
        buff.WriteByte(player.Spectator);
        buff.WriteString(player.Clan);
        buff.WriteString(player.Name);
        buff.WriteLong(player.PersonaId);
        buff.WriteString(player.SquadId);
        buff.WriteInt(player.Rank);
        buff.WriteInt(player.Dead);
        buff.WriteInt(player.Score);
        buff.WriteFloat(player.KD);
        buff.WriteFloat(player.KPM);
        buff.WriteString(player.WeaponS0CH);
    }

    public static void PlayerList(IByteBuffer buff, List<PlayerData> list)
    {
        buff.WriteInt(list.Count);
        foreach (var item in list)
        {
            Player(buff, item);
        }
    }

    public static void ServerScore(IByteBuffer buff)
    {
        ServerInfo(buff);
        if (Globals.PlayerList_Team1.Count != 0)
        {
            buff.WriteBoolean(true);
            PlayerList(buff, Globals.PlayerList_Team1);
        }
        else
        {
            buff.WriteBoolean(false);
        }
        if (Globals.PlayerList_Team2.Count != 0)
        {
            buff.WriteBoolean(true);
            PlayerList(buff, Globals.PlayerList_Team2);
        }
        else
        {
            buff.WriteBoolean(value: false);
        }
        if (Globals.PlayerList_Team0.Count != 0)
        {
            buff.WriteBoolean(true);
            PlayerList(buff, Globals.PlayerList_Team0);
        }
        else
        {
            buff.WriteBoolean(value: false);
        }
    }
}
