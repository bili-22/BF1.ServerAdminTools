namespace BF1.ServerAdminTools.Common.Data;

public record UpdateInfo
{
    public string Version { get; set; }
    public Latest Latest { get; set; }
    public Address Address { get; set; }
    public List<Download> Download { get; set; }
}

public record Latest
{
    public string Date { get; set; }
    public string Change { get; set; }
}

public record Address
{
    public string Notice { get; set; }
    public string Change { get; set; }
}

public record Download
{
    public string Name { get; set; }
    public string Url { get; set; }
}
