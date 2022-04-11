namespace BF1.ServerAdminTools.Netty;

public record ConfigNettyObj
{
    public int Port { get; set; }
    public long ServerKey { get; set; }
}
