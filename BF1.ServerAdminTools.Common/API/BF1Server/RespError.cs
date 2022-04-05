namespace BF1.ServerAdminTools.Common.API.BF1Server;

public record RespError
{
    public string jsonrpc { get; set; }
    public string id { get; set; }
    public Error error { get; set; }
}

public record Error
{
    public string message { get; set; }
    public int code { get; set; }
}
