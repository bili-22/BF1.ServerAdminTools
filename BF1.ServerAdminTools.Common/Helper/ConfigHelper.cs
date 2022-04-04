using BF1.ServerAdminTools.Common.Data;
using BF1.ServerAdminTools.Common.Helper;

namespace BF1.ServerAdminTools.Common.Utils;

internal static class ConfigHelper
{
    public static string MyDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    public static string Base { get; } = $"{MyDocument}/BF1 Server";

    public static string ServerRule { get; } = $"{Base}/ServerRule";
    public static string Cache { get; } = $"{Base}/Cache";
    public static string Log { get; } = $"{Base}/Log";

    public static string SettingFile { get; } = $"{Base}/config.json";

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
            string path = Log + @"\ErrorLog";
            Directory.CreateDirectory(path);
            path += $@"\#ErrorLog# { DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log";
            File.WriteAllText(path, logContent);
        }
        catch (Exception) { }
    }

    public static void WriteFile(string file, string data)
    {
        using var stream = File.Open(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
        var temp = Encoding.UTF8.GetBytes(data);
        stream.Write(temp, 0, temp.Length);
    }

    public static void SaveAll()
    {
        SaveConfig();
        foreach (var item in Globals.Rules)
        {
            WriteFile($"{ServerRule}/{item.Key}.json", JsonUtil.JsonSeri(item.Value));
        }
    }

    public static void SaveConfig()
    {
        WriteFile(SettingFile, JsonUtil.JsonSeri(Globals.Config));
    }

    public static void LoadConfig()
    {
        Directory.CreateDirectory(ConfigHelper.ServerRule);
        Directory.CreateDirectory(ConfigHelper.Cache);
        Directory.CreateDirectory(ConfigHelper.Log);
        if (!File.Exists(SettingFile))
        {
            Globals.Config = new()
            {
                AudioIndex = 3
            };
            File.WriteAllText(SettingFile, JsonUtil.JsonSeri(Globals.Config));
        }
        else
        {
            Globals.Config = JsonUtil.JsonDese<ConfigObj>(File.ReadAllText(SettingFile));
        }

        var dir = new DirectoryInfo(ServerRule);
        foreach (var item in dir.GetFiles())
        {
            if (item.Extension is ".json")
            {
                var name = item.Name.Trim().ToLower().Replace(".json", "");
                var data = File.ReadAllText(item.FullName);
                var rule = JsonUtil.JsonDese<ServerRule>(data);

                if (rule != null)
                {
                    Globals.Rules.Add(name, rule);
                }
            }
        }

        if (!Globals.Rules.ContainsKey("default"))
        {
            var rule = new ServerRule()
            {
                Name = "Default"
            };
            Globals.Rules.Add("default", rule);
            WriteFile($"{ServerRule}/default.json", JsonUtil.JsonSeri(rule));
        }
    }

    public static void DeleteRule(string name)
    {
        File.Delete($"{ServerRule}/{name}.json");
    }

    public static void SaveRule()
    {
        WriteFile($"{ServerRule}/{Globals.NowRule.Name.Trim().ToLower()}.json", 
            JsonUtil.JsonSeri(Globals.NowRule));
    }

    public static void SaveRule(ServerRule rule)
    {
        WriteFile($"{ServerRule}/{rule.Name.Trim().ToLower()}.json",
            JsonUtil.JsonSeri(rule));
    }
}
