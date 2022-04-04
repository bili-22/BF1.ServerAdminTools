using BF1.ServerAdminTools.Wpf.Data;
using BF1.ServerAdminTools.Wpf.Utils;

namespace BF1.ServerAdminTools.Wpf.Helper;

public static class ConfigLocal
{
    public static string MyDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public static string Base { get; } = $"{MyDocument}/BF1 Server";
    public static string Cache { get; } = $"{Base}/Cache";
    public static string Log { get; } = $"{Base}/Log";

    public static string SettingFile { get; } = $"{Base}/config.json";
}

internal static class ConfigHelper
{

    /// <summary>
    /// 获取当前运行文件完整路径
    /// </summary>
    public static string Current_Path = Process.GetCurrentProcess().MainModule.FileName;

    /// <summary>
    /// 获取当前文件目录，不加文件名及后缀
    /// </summary>
    public static string CurrentDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// 我的文档完整路径
    /// </summary>
    public static string MyDocuments_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    /// <summary>
    /// 给文件名，得出当前目录完整路径，AppName带文件名后缀
    /// </summary>
    public static string GetCurrFullPath(string AppName)
    {
        return Path.Combine(CurrentDirectory_Path, AppName);
    }

    /// <summary>
    /// 文件重命名
    /// </summary>
    public static void FileReName(string OldPath, string NewPath)
    {
        FileInfo ReName = new(OldPath);
        ReName.MoveTo(NewPath);
    }

    /// <summary>
    /// 保存错误Log日志文件到本地
    /// </summary>
    /// <param name="logContent">保存内容</param>
    public static void WriteErrorLog(string logContent)
    {
        try
        {
            string path = ConfigLocal.Log + @"\ErrorLog";
            Directory.CreateDirectory(path);
            path += $@"\#ErrorLog# { DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log";
            File.WriteAllText(path, logContent);
        }
        catch (Exception) { }
    }

    public static void SaveConfig()
    {
        FileUtil.WriteFile(ConfigLocal.SettingFile, JsonUtil.JsonSeri(Globals.Config));
    }

    public static void LoadConfig()
    {
        Directory.CreateDirectory(ConfigLocal.Cache);
        Directory.CreateDirectory(ConfigLocal.Log);
        if (!File.Exists(ConfigLocal.SettingFile))
        {
            Globals.Config = new()
            {
                AudioIndex = 3
            };
            File.WriteAllText(ConfigLocal.SettingFile, JsonUtil.JsonSeri(Globals.Config));
        }
        else
        {
            Globals.Config = JsonUtil.JsonDese<ConfigObj>(File.ReadAllText(ConfigLocal.SettingFile));
        }
    }
}
