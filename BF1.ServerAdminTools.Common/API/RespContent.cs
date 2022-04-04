namespace BF1.ServerAdminTools.Common.API;

public record RespContent
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public double ExecTime { get; set; }
    public object Obj { get; set; }
}
