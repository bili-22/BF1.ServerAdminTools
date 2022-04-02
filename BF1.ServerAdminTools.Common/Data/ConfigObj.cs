using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Common.Data;

public record ConfigObj
{
    public string Remid { get; set; }
    public string Sid { get; set; }

    public string SessionId { get; set; }
    public string GameId { get; set; }
    public string ServerId { get; set; }
    public string PersistedGameId { get; set; }

    public string Msg0 { get; set; }
    public string Msg1 { get; set; }
    public string Msg2 { get; set; }
    public string Msg3 { get; set; }
    public string Msg4 { get; set; }
    public string Msg5 { get; set; }
    public string Msg6 { get; set; }
    public string Msg7 { get; set; }
    public string Msg8 { get; set; }
    public string Msg9 { get; set; }

    public int AudioIndex { get; set; }
}
