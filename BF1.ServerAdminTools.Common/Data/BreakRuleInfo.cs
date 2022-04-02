namespace BF1.ServerAdminTools.Common.Data;

public record BreakRuleInfo
{
    /// <summary>
    /// 被踢出的玩家ID
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 被踢出的玩家数字ID
    /// </summary>
    public long PersonaId { get; set; }
    /// <summary>
    /// 被踢出的原因
    /// </summary>
    public string Reason { get; set; }
    /// <summary>
    /// 踢人标志，-1代表默认，0代表正在踢人中，1代表踢出成功，2代表踢出失败
    /// </summary>
    public int Flag { get; set; }
    /// <summary>
    /// 执行踢人操作的状态
    /// </summary>
    public string Status { get; set; }
    /// <summary>
    /// 记录踢人请求响应时间
    /// </summary>
    public DateTime Time { get; set; }
}
