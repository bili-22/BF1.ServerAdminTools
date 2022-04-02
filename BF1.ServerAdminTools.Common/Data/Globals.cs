namespace BF1.ServerAdminTools.Common.Data;

public static class ServerRule
{
    public static int MaxKill = 0;

    public static int KDFlag = 0;
    public static float MaxKD = 0.00f;

    public static int KPMFlag = 0;
    public static float MaxKPM = 0.00f;

    public static int MaxRank = 0;
    public static int MinRank = 0;

    public static float LifeMaxKD = 0.00f;
    public static float LifeMaxKPM = 0.00f;
    public static int LifeMaxWeaponStar = 0;
    public static int LifeMaxVehicleStar = 0;
}
public static class Globals
{
    public static string Remid = string.Empty;
    public static string Sid = string.Empty;

    public static string SessionId = string.Empty;
    public static string GameId = string.Empty;
    public static string ServerId = string.Empty;
    public static string PersistedGameId = string.Empty;

    public static bool IsRuleSetRight = false;

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 保存限制武器名称列表
    /// </summary>
    public static List<string> Custom_WeaponList = new();
    /// <summary>
    /// 自定义黑名单玩家列表
    /// </summary>
    public static List<string> Custom_BlackList = new();
    /// <summary>
    /// 自定义白名单玩家列表
    /// </summary>
    public static List<string> Custom_WhiteList = new();

    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static List<string> Server_AdminList = new();
    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static List<string> Server_Admin2List = new();
    /// <summary>
    /// 服务器VIP
    /// </summary>
    public static List<string> Server_VIPList = new();

    /// <summary>
    /// 保存违规玩家列表信息
    /// </summary>
    public static List<BreakRuleInfo> BreakRuleInfo_PlayerList = new();

    /// <summary>
    /// 观战玩家列表
    /// </summary>
    public static List<SpectatorInfo> Server_SpectatorList = new();

    ///////////////////////////////////////////////////////

    /// <summary>
    /// 是否自动踢出违规玩家
    /// </summary>
    public static bool AutoKickBreakPlayer = false;

    /// <summary>
    /// 是否显示中文武器名称
    /// </summary>
    public static bool IsShowCHSWeaponName = false;
}
