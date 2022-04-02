using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF1.ServerAdminTools.Wpf.Utils;

public static class FileSelectUtil
{
    public static string? FileSelect()
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog()
        {
            Filter = "名单 (*.txt)|*.txt"
        };
        var result = openFileDialog.ShowDialog();
        if (result == true)
        {
            return openFileDialog.FileName;
        }

        return null;
    }
}
