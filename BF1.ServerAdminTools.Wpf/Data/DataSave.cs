using BF1.ServerAdminTools.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Wpf.Data;

public static class DataSave
{
    /// <summary>
    /// 加载的规则列表
    /// </summary>
    public static Dictionary<string, ServerRule> Rules { get; } = new();
    /// <summary>
    /// 目前应用的规则
    /// </summary>
    public static ServerRule NowRule { get; set; }

    /// <summary>
    /// 是否自动踢出违规玩家
    /// </summary>
    public static bool AutoKickBreakPlayer = false;

    /// <summary>
    /// 是否显示中文武器名称
    /// </summary>
    public static bool IsShowCHSWeaponName = true;

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
}
