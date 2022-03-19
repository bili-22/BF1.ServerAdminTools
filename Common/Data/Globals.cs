namespace BF1.ServerAdminTools.Common.Data
{
    public class Globals
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
        public static List<string> Custom_WeaponList = new List<string>();
        /// <summary>
        /// 自定义黑名单玩家列表
        /// </summary>
        public static List<string> Custom_BlackList = new List<string>();
        /// <summary>
        /// 自定义白名单玩家列表
        /// </summary>
        public static List<string> Custom_WhiteList = new List<string>();

        /// <summary>
        /// 服务器管理员
        /// </summary>
        public static List<string> Server_AdminList = new List<string>();
        /// <summary>
        /// 服务器管理员
        /// </summary>
        public static List<string> Server_Admin2List = new List<string>();
        /// <summary>
        /// 服务器VIP
        /// </summary>
        public static List<string> Server_VIPList = new List<string>();

        /// <summary>
        /// 保存违规玩家列表信息
        /// </summary>
        public static List<BreakRuleInfo> BreakRuleInfo_PlayerList = new List<BreakRuleInfo>();

        /// <summary>
        /// 观战玩家列表
        /// </summary>
        public static List<SpectatorInfo> Server_SpectatorList = new List<SpectatorInfo>();

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
}
