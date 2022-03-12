namespace BF1.ServerAdminTools.Features.API2.RespJson
{
    public class StatusArray
    {
        public List<int> diceSoldierAmount { get; set; }
        public List<int> communitySoldierAmount { get; set; }
        public List<int> soldierAmount { get; set; }
        public List<string> timeStamps { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
