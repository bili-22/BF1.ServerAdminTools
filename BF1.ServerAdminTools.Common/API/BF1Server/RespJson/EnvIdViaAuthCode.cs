namespace BF1.ServerAdminTools.Common.API.BF1Server.RespJson;

public record EnvIdViaAuthCode
{
    public string jsonrpc { get; set; }
    public string id { get; set; }
    public Result result { get; set; }
    public record Result
    {
        public string envId { get; set; }
        public Parameters parameters { get; set; }
        public string sessionId { get; set; }
        public string personaId { get; set; }
        public record Parameters
        {
            public string bbPrefix { get; set; }
            public bool supportsFilterState { get; set; }
            public bool supportsCampaignOperations { get; set; }
            public List<string> featureFlags { get; set; }
            public string currentUtcTimestamp { get; set; }
            public bool hasOnlineAccess { get; set; }
            [JsonIgnore]
            public string background { get; set; }
        }
    }
}
