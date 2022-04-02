namespace BF1.ServerAdminTools.BF1API.API2;

public record RespContent
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public double ExecTime { get; set; }
}
