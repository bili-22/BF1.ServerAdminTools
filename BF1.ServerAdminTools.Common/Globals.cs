using BF1.ServerAdminTools.Common.Data;

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

    public static Dictionary<string, ServerRule> Rules { get; } = new();

    public static ServerRule NowRule { get; set; }

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
    /// 保存违规玩家列表信息
    /// </summary>
    public static Dictionary<long, BreakRuleInfo> BreakRuleInfo_PlayerList { get; } = new();
    /// <summary>
    /// 已经发送踢出的ID
    /// </summary>
    public static Dictionary<long, BreakRuleInfo> NowKick { get; } = new();

    /// <summary>
    /// 观战玩家列表
    /// </summary>
    public static List<SpectatorInfo> Server_SpectatorList { get; } = new();

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 是否自动踢出违规玩家
    /// </summary>
    public static bool AutoKickBreakPlayer = false;

    /// <summary>
    /// 是否显示中文武器名称
    /// </summary>
    public static bool IsShowCHSWeaponName = false;

    /// <summary>
    /// 游戏是否在运行
    /// </summary>
    public static bool IsGameRun = false;

    /// <summary>
    /// 内存模块是否运行
    /// </summary>
    public static bool IsToolInit = false;
}
