using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK.Objs;

public record IdObj
{
    public string Remid { get; set; }
    public string Sid { get; set; }
    public string SessionId { get; set; }
    public string GameId { get; set; }
    public string ServerId { get; set; }
    public string PersistedGameId { get; set; }
}
