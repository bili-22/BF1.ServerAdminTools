namespace BF1.ServerAdminTools.BF1API.API;

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
