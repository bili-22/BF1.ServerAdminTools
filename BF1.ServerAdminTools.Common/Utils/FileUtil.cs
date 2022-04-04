using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Wpf.Utils;

public static class FileUtil
{
    public static void WriteFile(string file, string data)
    {
        using var stream = File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        var temp = Encoding.UTF8.GetBytes(data);
        stream.Write(temp, 0, temp.Length);
    }
}
