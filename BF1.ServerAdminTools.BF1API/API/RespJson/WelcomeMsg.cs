namespace BF1.ServerAdminTools.BF1API.API.RespJson;

public record WelcomeMsg
{
    public string jsonrpc { get; set; }
    public string id { get; set; }
    public Result result { get; set; }
    public record Result
    {
        public string firstMessage { get; set; }
        public string secondMessage { get; set; }
    }
}
