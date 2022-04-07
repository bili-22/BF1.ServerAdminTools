using BF1.ServerAdminTools.Common.API.GT.RespJson;
using BF1.ServerAdminTools.Common.Data;
using static BF1.ServerAdminTools.Common.API.BF1Server.RespJson.FullServerDetails.Result;

namespace BF1.ServerAdminTools.Common;

public static class Globals
{
    /// <summary>
    /// 目标进程
    /// </summary>
    public const string TargetAppName = "bf1";    // 战地1

    public static ConfigObj Config;
    public static bool IsRuleSetRight = false;

    ///////////////////////////////////////////////////////

    public static List<PlayerData> PlayerList_All { get; } = new();
    public static List<PlayerData> PlayerList_Team0 { get; } = new();
    public static List<PlayerData> PlayerList_Team1 { get; } = new();
    public static List<PlayerData> PlayerList_Team2 { get; } = new();

    public static Dictionary<long, PlayerData> PlayerDatas_Team1 { get; } = new();
    public static Dictionary<long, PlayerData> PlayerDatas_Team2 { get; } = new();
    public static Dictionary<long, PlayerData> PlayerDatas_Team3 { get; } = new();

    public static ClientPlayer LocalPlayer;
    public static ServerHook ServerHook;
    public static ServerInfo ServerInfo;
    public static ServerInfos.ServersItem ServerDetailed;

    public static StatisticData StatisticData_Team1;
    public static StatisticData StatisticData_Team2;

    /// <summary>
    /// 观战玩家列表
    /// </summary>
    public static List<SpectatorInfo> Server_SpectatorList { get; } = new();

    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static List<long> Server_AdminList { get; } = new();
    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static List<string> Server_Admin2List { get; } = new();
    /// <summary>
    /// 服务器VIP
    /// </summary>
    public static List<long> Server_VIPList { get; } = new();

    /// <summary>
    /// 游戏是否在运行
    /// </summary>
    public static bool IsGameRun = false;

    /// <summary>
    /// 内存模块是否运行
    /// </summary>
    public static bool IsToolInit = false;
}
