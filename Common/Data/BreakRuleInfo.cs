namespace BF1.ServerAdminTools.Common.Data
{
    public class BreakRuleInfo
    {
        /// <summary>
        /// 被踢出的玩家ID
        /// </summary>
        public string Name;
        /// <summary>
        /// 被踢出的玩家数字ID
        /// </summary>
        public long PersonaId;
        /// <summary>
        /// 被踢出的原因
        /// </summary>
        public string Reason;
        /// <summary>
        /// 踢人标志，-1代表默认，0代表正在踢人中，1代表踢出成功，2代表踢出失败
        /// </summary>
        public int Flag;
        /// <summary>
        /// 执行踢人操作的状态
        /// </summary>
        public string Status;
        /// <summary>
        /// 记录踢人请求响应时间
        /// </summary>
        public DateTime Time;
    }
}
