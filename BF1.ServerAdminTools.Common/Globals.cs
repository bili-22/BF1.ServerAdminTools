using BF1.ServerAdminTools.Wpf.Data;

namespace BF1.ServerAdminTools.Wpf;

public static class Globals
{
    /// <summary>
    /// 目标进程
    /// </summary>
    public const string TargetAppName = "bf1";    // 战地1

    public static ConfigObj Config;
    public static bool IsRuleSetRight = false;

    ///////////////////////////////////////////////////////

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
