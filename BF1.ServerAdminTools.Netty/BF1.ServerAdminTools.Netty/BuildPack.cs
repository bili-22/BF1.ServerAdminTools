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
        buff.WriteInt(Globals.ServerHook.ServerName.Length);
        buff.WriteString(Globals.ServerHook.ServerName, Encoding.UTF8);
        buff.WriteLong(Globals.ServerHook.ServerID);
        buff.WriteFloat(Globals.ServerHook.ServerTime);
        buff.WriteInt(Globals.ServerHook.ServerTimeS.Length);
        buff.WriteString(Globals.ServerHook.ServerTimeS, Encoding.UTF8);
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
            buff.WriteByte(0xff);
            buff.WriteInt(Globals.ServerDetailed.currentMap.Length);
            buff.WriteString(Globals.ServerDetailed.currentMap, Encoding.UTF8);
            buff.WriteInt(Globals.ServerDetailed.currentMapImage.Length);
            buff.WriteString(Globals.ServerDetailed.currentMapImage, Encoding.UTF8);
            buff.WriteInt(Globals.ServerDetailed.teams.teamOne.key.Length);
            buff.WriteString(Globals.ServerDetailed.teams.teamOne.key, Encoding.UTF8);
            buff.WriteInt(Globals.ServerDetailed.teams.teamOne.image.Length);
            buff.WriteString(Globals.ServerDetailed.teams.teamOne.image, Encoding.UTF8);
            buff.WriteInt(Globals.ServerDetailed.teams.teamTwo.key.Length);
            buff.WriteString(Globals.ServerDetailed.teams.teamTwo.key, Encoding.UTF8);
            buff.WriteInt(Globals.ServerDetailed.teams.teamTwo.image.Length);
            buff.WriteString(Globals.ServerDetailed.teams.teamTwo.image, Encoding.UTF8);
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
        buff.WriteInt(player.Clan.Length);
        buff.WriteString(player.Clan, Encoding.UTF8);
        buff.WriteInt(player.Name.Length);
        buff.WriteString(player.Name, Encoding.UTF8);
        buff.WriteLong(player.PersonaId);
        buff.WriteInt(player.SquadId.Length);
        buff.WriteString(player.SquadId, Encoding.UTF8);
        buff.WriteInt(player.Rank);
        buff.WriteInt(player.Dead);
        buff.WriteInt(player.Score);
        buff.WriteFloat(player.KD);
        buff.WriteFloat(player.KPM);
        buff.WriteInt(player.WeaponS0CH.Length);
        buff.WriteString(player.WeaponS0CH, Encoding.UTF8);
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
            buff.WriteByte(value: 0xff);
            PlayerList(buff, Globals.PlayerList_Team1);
        }
        else
        {
            buff.WriteByte(0);
        }
        if (Globals.PlayerList_Team2.Count != 0)
        {
            buff.WriteByte(0xff);
            PlayerList(buff, Globals.PlayerList_Team2);
        }
        else
        {
            buff.WriteByte(0);
        }
        if (Globals.PlayerList_Team0.Count != 0)
        {
            buff.WriteByte(0xff);
            PlayerList(buff, Globals.PlayerList_Team0);
        }
        else
        {
            buff.WriteByte(0);
        }
    }
}
