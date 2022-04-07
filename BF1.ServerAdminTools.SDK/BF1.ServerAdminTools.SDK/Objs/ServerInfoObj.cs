using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK.Objs;

public record ServerInfoObj
{
    public string ServerName { get; set; }
    public long ServerID { get; set; }
    public float ServerTime { get; set; }
    public string ServerTimeS { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    public int Team1FromeKill { get; set; }
    public int Team2FromeKill { get; set; }
    public int Team1FromeFlag { get; set; }
    public int Team2FromeFlag { get; set; }
    public int Team1MaxPlayerCount { get; set; }
    public int Team2MaxPlayerCount { get; set; }
    public int Team1PlayerCount { get; set; }
    public int Team2PlayerCount { get; set; }
    public int Team1Rank150PlayerCount { get; set; }
    public int Team2Rank150PlayerCount { get; set; }
    public int Team1AllKillCount { get; set; }
    public int Team2AllKillCount { get; set; }
    public int Team1AllDeadCount { get; set; }
    public int Team2AllDeadCount { get; set; }
    public string MapName { get; set; }
    public string MapUrl { get; set; }
    public string TeamOne { get; set; }
    public string TeamOneUrl { get; set; }
    public string TeamTwo { get; set; }
    public string TeamTwoUrl { get; set; }
}
