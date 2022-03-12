namespace BF1.ServerAdminTools.Common.Data
{
    public class Globals
    {
        public static string SessionId = string.Empty;
        public static string GameId = string.Empty;
        public static string ServerId = string.Empty;
        public static string PersistedGameId = string.Empty;

        public static bool IsRuleSetRight = false;

        ///////////////////////////////////////////////////////

        // 保存限制武器名称
        public static List<string> Custom_WeaponList = new List<string>();
        // 自定义黑名单玩家
        public static List<string> Custom_BlackList = new List<string>();
        // 自定义白名单玩家
        public static List<string> Custom_WhiteList = new List<string>();

        // 服务器管理员、VIP
        public static List<string> Server_AdminList = new List<string>();
        public static List<string> Server_Admin2List = new List<string>();
        public static List<string> Server_VIPList = new List<string>();

        // 保存违规玩家列表信息
        public static List<BreakRuleInfo> BreakRuleInfo_PlayerList = new List<BreakRuleInfo>();

        // 观战玩家列表
        public static List<SpectatorInfo> Server_SpectatorList = new List<SpectatorInfo>();

        ///////////////////////////////////////////////////////

        // 自动踢出违规玩家
        public static bool AutoKickBreakPlayer = false;


        // 是否显示中文武器名称
        public static bool IsShowCHSWeaponName = false;
    }
}
