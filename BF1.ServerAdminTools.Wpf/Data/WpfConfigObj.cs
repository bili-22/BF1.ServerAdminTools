using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Wpf.Data;

public record WpfConfigObj
{
    public string Bg { get; set; }
    public int Bg_O { get; set; }
    public bool Window_O { get; set; }
    public bool AutoRun { get; set; }
}
