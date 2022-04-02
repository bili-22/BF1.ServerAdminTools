namespace BF1.ServerAdminTools.Common.Data
{
    public record ChangeTeamInfo
    {
        /// <summary>
        /// 更换队伍的玩家等级
        /// </summary>
        public int Rank;
        /// <summary>
        /// 更换队伍的玩家ID
        /// </summary>
        public string Name;
        /// <summary>
        /// 更换队伍的玩家数字ID
        /// </summary>
        public long PersonaId;
        /// <summary>
        /// 更换队伍的状态
        /// </summary>
        public string Status;
        /// <summary>
        /// 更换队伍的时间
        /// </summary>
        public DateTime Time;
    }
}
