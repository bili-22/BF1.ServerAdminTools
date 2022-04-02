using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Common.Data;

public record ServerRule
{
    public int MaxKill { get; set; }

    public int KDFlag { get; set; }
    public float MaxKD { get; set; }

    public int KPMFlag { get; set; }
    public float MaxKPM { get; set; }

    public int MaxRank { get; set; }
    public int MinRank { get; set; }

    public float LifeMaxKD { get; set; }
    public float LifeMaxKPM { get; set; }
    public int LifeMaxWeaponStar { get; set; }
    public int LifeMaxVehicleStar { get; set; }

    /// <summary>
    /// 保存限制武器名称列表
    /// </summary>
    public List<string> Custom_WeaponList { get; set; }
    /// <summary>
    /// 自定义黑名单玩家列表
    /// </summary>
    public List<string> Custom_BlackList { get; set; }
    /// <summary>
    /// 自定义白名单玩家列表
    /// </summary>
    public List<string> Custom_WhiteList { get; set; }
}


