namespace BF1.ServerAdminTools.Common.Data;

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

    public static readonly Dictionary<string, ServerRule> Rules = new();

    public static ServerRule? NowRule;

    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static readonly List<string> Server_AdminList = new();
    /// <summary>
    /// 服务器管理员
    /// </summary>
    public static readonly List<string> Server_Admin2List = new();
    /// <summary>
    /// 服务器VIP
    /// </summary>
    public static readonly List<string> Server_VIPList = new();

    /// <summary>
    /// 保存违规玩家列表信息
    /// </summary>
    public static readonly List<BreakRuleInfo> BreakRuleInfo_PlayerList = new();

    /// <summary>
    /// 观战玩家列表
    /// </summary>
    public static readonly List<SpectatorInfo> Server_SpectatorList = new();

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
