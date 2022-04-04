namespace BF1.ServerAdminTools.Common.API;

public record RespContent<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public double ExecTime { get; set; }
    public T Obj { get; set; }
}
