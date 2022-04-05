using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Netty;

public record ConfigObj
{
    public int Port { get; set; }
    public long ServerKey { get; set; }
}
