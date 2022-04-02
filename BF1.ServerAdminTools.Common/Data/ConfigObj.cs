using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Common.Data;

public record ConfigObj
{
    public string SessionId { get; set; }
    public string Remid { get; set; }
    public string Sid { get; set; }
}
