using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.SDK.Objs;

public record StateObj
{
    public bool IsGameRun { get; set; }
    public bool IsToolInit { get; set; }
}
